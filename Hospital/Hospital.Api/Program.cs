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
using MongoDB.Bson;
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

// Add services to the container
builder.Services.AddScoped<IRepositoryAsync<Patient, ObjectId>, PatientEfCoreRepository>();
builder.Services.AddScoped<IRepositoryAsync<Doctor, ObjectId>, DoctorEfCoreRepository>();
builder.Services.AddScoped<IRepositoryAsync<Appointment, ObjectId>, AppointmentEfCoreRepository>();

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