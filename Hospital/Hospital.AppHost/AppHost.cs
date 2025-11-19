var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder
    .AddMongoDB("mongo")
    .AddDatabase("hospital");

builder.AddProject<Projects.Hospital_Api>("api")
    .WithReference(mongo)
    .WaitFor(mongo);

builder.Build().Run();