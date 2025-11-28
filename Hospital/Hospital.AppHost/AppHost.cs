var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder
    .AddMongoDB("mongo");

builder.AddProject<Projects.Hospital_Api>("hospital")
    .WithReference(mongo)
    .WaitFor(mongo);

builder.Build().Run();