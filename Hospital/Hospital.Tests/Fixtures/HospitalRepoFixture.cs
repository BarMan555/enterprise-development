using AutoMapper;
using Hospital.Application.Services;
using Hospital.Application;
using Hospital.Domain.Models;
using Hospital.Infrastructure.InMemory.Repositories;
using Hospital.Infrastructure.InMemory.Seeders;

namespace Hospital.Tests.Fixtures;

public class HospitalRepoFixture
{
    public InMemoryPatientRepository PatientRepository { get; }
    public InMemoryDoctorRepository DoctorRepository { get; }
    public InMemoryAppointmentRepository AppointmentRepository { get; }

    public HospitalRepoFixture()
    {
        var patientSeeder = new InMemoryPatientRepositorySeeder();
        var doctorSeeder = new InMemoryDoctorRepositorySeeder();
        var appointmentSeeder = new InMemoryAppointmentRepositorySeeder();
        
        PatientRepository = new InMemoryPatientRepository(patientSeeder); 
        DoctorRepository = new InMemoryDoctorRepository(doctorSeeder);
        AppointmentRepository = new InMemoryAppointmentRepository(appointmentSeeder);
    }
}