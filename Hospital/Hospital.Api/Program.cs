using AutoMapper;
using Hospital.Application;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Services;
using Hospital.Domain;
using Hospital.Domain.Models;
using Hospital.Domain.Seeders;
using Hospital.Infrastructure.EfCore;
using Hospital.Infrastructure.EfCore.Repositories;
using Hospital.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new MappingProfile()),
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var mongoConnectionString = builder.Configuration.GetConnectionString("Mongo") 
                            ?? throw new InvalidOperationException("MongoDB connection string not found");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMongoDB(mongoConnectionString, "HospitalDb");
});

/*builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("library");
    return new MongoClient(connectionString);
});

builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var database = client.GetDatabase("library"); 
    return new MongoClient(database);
});*/


// Add services to the container.
builder.Services.AddSingleton<PatientRepositorySeeder>();
builder.Services.AddSingleton<DoctorRepositorySeeder>();
builder.Services.AddSingleton<AppointmentRepositorySeeder>();

builder.Services.AddScoped<IRepositoryAsync<Patient, int>, PatientEfCoreRepository>();
builder.Services.AddScoped<IRepositoryAsync<Doctor, int>, DoctorEfCoreRepository>();
builder.Services.AddScoped<IRepositoryAsync<Appointment, int>, AppointmentEfCoreRepository>();

builder.Services.AddScoped<ILibraryAnalyticsService, LibraryAnalyticsService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DataInitializer.SeedEnsureCreated(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();