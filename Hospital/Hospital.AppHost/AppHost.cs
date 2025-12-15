var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder
    .AddMongoDB("mongo");

// Добавляем проект генератора (gRPC сервер)
var generator = builder.AddProject<Projects.Hospital_Grpc_Server>("generator")
    .WithHttpsEndpoint(port: 7100, name: "grpc");

// Добавляем API и даем ему ссылку на генератор
builder.AddProject<Projects.Hospital_Api>("hospital")
    .WithReference(mongo)
    .WithReference(generator)
    .WaitFor(mongo)
    .WaitFor(generator);

builder.Build().Run();