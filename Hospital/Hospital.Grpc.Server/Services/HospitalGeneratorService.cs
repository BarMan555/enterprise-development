using Bogus;
using Grpc.Core;
using Hospital.Grpc.Contracts;

namespace Hospital.Grpc.Server.Services;

/// <summary>
/// gRPC сервис-генератор пациентов.
/// Генерирует случайных пациентов и отправляет их клиентам (API).
/// </summary>
public class HospitalGeneratorService(
    ILogger<HospitalGeneratorService> logger,
    IConfiguration config) 
    : HospitalGenerator.HospitalGeneratorBase
{
    private readonly Faker _faker = new("ru");
    private readonly int _delaySeconds = config.GetValue<int?>("GeneratorDelaySeconds") ?? 1;

    /// <summary>
    /// Потоковая генерация билетов.
    /// Клиент (API) подключается, генератор отправляет билеты, клиент возвращает статусы.
    /// </summary>
    public override async Task StreamPatients(
        IAsyncStreamReader<GenerationCallback> requestStream,
        IServerStreamWriter<PatientResponse> responseStream,
        ServerCallContext context)
    {
        logger.LogInformation("API подключилось к генератору пациентов.");

        // Генерация и отправка данных
        var generationTask = Task.Run(async () =>
        {
            var count = 0;
            while (!context.CancellationToken.IsCancellationRequested)
            {
                var patient = GenerateRandomPatient();
                count++;

                logger.LogInformation("Отправка пациента #{Count}: {FullName}", count, patient.FullName);

                await responseStream.WriteAsync(patient, context.CancellationToken);

                await Task.Delay(TimeSpan.FromSeconds(_delaySeconds), context.CancellationToken);
            }
        }, context.CancellationToken);

        // Чтение статусов от API (успешно/ошибка)
        var callbackTask = Task.Run(async () =>
        {
            await foreach (var callback in requestStream.ReadAllAsync(context.CancellationToken))
            {
                if (callback.Success)
                {
                    logger.LogInformation("API подтвердил сохранение.");
                }
                else
                {
                    logger.LogWarning("API вернул ошибку: {Error}", callback.Error);
                }
            }
        }, context.CancellationToken);

        try
        {
            await Task.WhenAll(generationTask, callbackTask);
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Сессия генерации завершена (клиент отключился).");
        }
    }

    /// <summary>
    /// Метод генерации пациентов.
    /// </summary>
    private PatientResponse GenerateRandomPatient()
    {
        return new PatientResponse
        {
            FullName = _faker.Name.FullName(),
            Gender = _faker.PickRandom(0, 1), 
            DateOfBirth = _faker.Date.Past(50).ToString("yyyy-MM-dd"), 
            Address = _faker.Address.FullAddress(),
            BloodType = _faker.PickRandom(0, 1, 2, 3),
            RhFactor = _faker.PickRandom(0, 1),
            PhoneNumber = _faker.Phone.PhoneNumber()
        };
    }
}