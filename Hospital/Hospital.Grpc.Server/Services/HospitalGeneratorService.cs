using Bogus;
using Grpc.Core;
using Hospital.Grpc.Contracts;

namespace Hospital.Grpc.Server.Services;

public class HospitalGeneratorService(
    ILogger<HospitalGeneratorService> logger,
    IConfiguration config) 
    : HospitalGenerator.HospitalGeneratorBase
{
    // Настройка Faker для генерации русских данных
    private readonly Faker _faker = new("ru");
    
    // Задержка между отправкой данных (по умолчанию 1 секунда)
    private readonly int _delaySeconds = config.GetValue<int?>("GeneratorDelaySeconds") ?? 1;

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

                logger.LogInformation("→ Отправка пациента #{Count}: {FullName}", count, patient.FullName);

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
                    logger.LogInformation("← API подтвердил сохранение.");
                }
                else
                {
                    logger.LogWarning("← API вернул ошибку: {Error}", callback.Error);
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

    private PatientResponse GenerateRandomPatient()
    {
        return new PatientResponse
        {
            FullName = _faker.Name.FullName(),
            Gender = _faker.PickRandom(0, 1), // 0 - Male, 1 - Female
            DateOfBirth = _faker.Date.Past(50).ToString("yyyy-MM-dd"), // Строка или Timestamp в proto
            Address = _faker.Address.FullAddress(),
            BloodType = _faker.PickRandom(0, 1, 2, 3),
            RhFactor = _faker.PickRandom(0, 1),
            PhoneNumber = _faker.Phone.PhoneNumber()
        };
    }
}