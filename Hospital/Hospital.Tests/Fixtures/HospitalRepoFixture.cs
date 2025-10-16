using Hospital.Application.Services;
using Hospital.Infrastructure.InMemory.Repositories;
using Hospital.Infrastructure.InMemory.Seeders;

namespace Hospital.Tests.Fixtures;

public class HospitalRepoFixture
{
    public PatientService PatientService { get; }
    public DoctorService DoctorService { get; }
    public AppointmentService AppointmentService { get; }

    public HospitalRepoFixture()
    {
        var patientSeeder = new InMemoryPatientRepositorySeeder();
        var doctorSeeder = new InMemoryDoctorRepositorySeeder();
        var appointmentSeeder = new InMemoryAppointmentRepositorySeeder();
        
        var patientRepository = new InMemoryPatientRepository(patientSeeder); 
        var doctorRepository = new InMemoryDoctorRepository(doctorSeeder);
        var appointmentRepository = new InMemoryAppointmentRepository(appointmentSeeder);
        
        PatientService = new PatientService(patientRepository);
        DoctorService = new DoctorService(doctorRepository);
        AppointmentService = new AppointmentService(appointmentRepository);
    }
}