using Hospital.Grpc.Server.Services;
using Hospital.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// Подключение стандартных сервисов Aspire
builder.AddServiceDefaults();

// Регистрация gRPC
builder.Services.AddGrpc();

var app = builder.Build();

app.MapDefaultEndpoints();

// Маппинг сервиса генерации
app.MapGrpcService<HospitalGeneratorService>();

app.MapGet("/", () => "Hospital Data Generator gRPC Server is running...");

app.Run();