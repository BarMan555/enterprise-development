using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using Hospital.Application.Contracts.Dtos;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Grpc.Contracts;

namespace Hospital.Api.Grpc;

/// <summary>
/// Фоновый сервис-потребитель пациентов, работающий как gRPC клиент.
/// Подключается к удалённому генератору билетов, получает поток данных
/// и сохраняет их в базу данных с отправкой статусов обработки.
/// </summary>
public class PatientConsumerService(
    IServiceScopeFactory scopeFactory,
    ILogger<PatientConsumerService> logger,
    IConfiguration config,
    IMapper mapper) : BackgroundService
{
    private readonly string _generatorUrl = config["services:generator:grpc:0"] 
                                            ?? throw new KeyNotFoundException("URL генератора не найден в конфигурации");
    
    /// <summary>
    /// Основной метод выполнения фонового сервиса.
    /// Устанавливает соединение с генератором, получает поток пациентов
    /// и обрабатывает каждого пациента с сохранением в БД.
    /// </summary>
    /// <param name="stoppingToken">Токен отмены для корректного завершения работы сервиса.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(5000, stoppingToken);

        logger.LogInformation("Подключение к генератору данных по адресу: {Url}", _generatorUrl);
        
        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback = 
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        using var channel = GrpcChannel.ForAddress(_generatorUrl, new GrpcChannelOptions { 
            HttpHandler = httpHandler 
        });
        
        var client = new HospitalGenerator.HospitalGeneratorClient(channel);

        try
        {
            using var call = client.StreamPatients(cancellationToken: stoppingToken);
            
            await foreach (var response in call.ResponseStream.ReadAllAsync(stoppingToken))
            {
                logger.LogInformation("Получен пациент: {Name}", response.FullName);

                var success = false;
                var error = string.Empty;

                try
                {
                    using var scope = scopeFactory.CreateScope();
                    var patientService = scope.ServiceProvider.GetRequiredService<IPatientService>();
                    
                    var createDto = mapper.Map<PatientCreateUpdateDto>(response);

                    await patientService.Create(createDto);
                    success = true;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Ошибка при сохранении пациента");
                    error = ex.Message;
                }

                // Отправляем ответ генератору (Callback)
                await call.RequestStream.WriteAsync(new GenerationCallback
                {
                    Success = success,
                    Error = error ?? ""
                }, stoppingToken);
            }
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
        {
            logger.LogInformation("Стрим был отменен.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Критическая ошибка в потребителе данных");
        }
    }
}