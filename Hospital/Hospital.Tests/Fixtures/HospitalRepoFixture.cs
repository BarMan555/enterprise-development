using AutoMapper;
using Hospital.Application.Services;
using Hospital.Application;
using Hospital.Domain.Models;
using Hospital.Infrastructure.InMemory.Repositories;
using Hospital.Infrastructure.InMemory.Seeders;

namespace Hospital.Tests.Fixtures;

/// <summary>
/// Fixture data for Unit-Tests
/// </summary>
public class HospitalRepoFixture
{
    /// <summary>
    /// Repository of patients
    /// </summary>
    public InMemoryPatientRepository PatientRepository { get; }
    
    /// <summary>
    /// Repository of doctors
    /// </summary>
    public InMemoryDoctorRepository DoctorRepository { get; }
    
    /// <summary>
    /// Repository of appointments
    /// </summary>
    public InMemoryAppointmentRepository AppointmentRepository { get; }

    /// <summary>
    /// Make data for all repositories
    /// </summary>
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