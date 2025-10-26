using Hospital.Application.Services;
using Hospital.Domain;
using Hospital.Domain.Models;
using Hospital.Domain.Seeders;
using Hospital.Infrastructure.InMemory.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<InMemoryPatientRepositorySeeder>();
builder.Services.AddSingleton<InMemoryDoctorRepositorySeeder>();
builder.Services.AddSingleton<InMemoryAppointmentRepositorySeeder>();

builder.Services.AddSingleton<IRepository<Patient, int>, InMemoryPatientRepository>();
builder.Services.AddSingleton<IRepository<Doctor, int>, InMemoryDoctorRepository>();
builder.Services.AddSingleton<IRepository<Appointment, int>, InMemoryAppointmentRepository>();

builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<AppointmentService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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