using AutoMapper;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Dtos;
using Hospital.Domain.Models;
using Hospital.Domain;

namespace Hospital.Application.Services;

public class LibraryAnalyticsService(
    IRepository<Patient, int> patientRepository,
    IRepository<Doctor, int> doctorRepository,
    IRepository<Appointment, int> appointmentRepository,
    IMapper mapper
    ) : ILibraryAnalyticsService
{
    
    public List<DoctorDto> GetDoctorsWithExperienceAtLeastYears(int year)
    {
        return mapper.Map<List<DoctorDto>>((
            from d in doctorRepository.ReadAll()
            where d.ExperienceYears >= year
            select d
        ).ToList());
    }

    public List<PatientDto> GetPatientsByDoctor(int doctorId)
    {
        return mapper.Map<List<PatientDto>>((from a in appointmentRepository.ReadAll()
            where a.Doctor.Id == doctorId
            select a.Patient).ToList());
    }

    public int GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod(DateTime start, DateTime end)
    {
        return (from a in appointmentRepository.ReadAll()
            where a.AppointmentDateTime >= start && a.AppointmentDateTime <= end
            select a.Id).Count();
    }

    public List<PatientDto> GetPatientsOlderThaneWithMultipleDoctors(int age)
    {
        var today = DateTime.Today;
        var patients = patientRepository.ReadAll().Where(p => (today - p.DateOfBirth).Days / 365 >= age).ToList();
        var appointments = appointmentRepository.ReadAll();

        var patientDoctorGroups = appointments
            .GroupBy(a => a.Patient.Id)
            .ToDictionary(
                g => g.Key,
                g => g.Select(a => a.Doctor.Id).Distinct().ToList()
            );

        var result = patients
            .Where(p => patientDoctorGroups.ContainsKey(p.Id) &&
                        patientDoctorGroups[p.Id].Count > 1)
            .ToList();

        return mapper.Map<List<PatientDto>>(result);
    }

    public List<AppointmentDto> GetAppointmentsWhenInSpecificRoomInSpecificPeriod(int roomId,  DateTime start, DateTime end)
    {
        return mapper.Map<List<AppointmentDto>>((from a in appointmentRepository.ReadAll()
            where a.RoomNumber == roomId
            where a.AppointmentDateTime >= start && a.AppointmentDateTime <= end
            select a).ToList());
    }
}