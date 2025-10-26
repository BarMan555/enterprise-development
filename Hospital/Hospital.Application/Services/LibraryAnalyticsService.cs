using AutoMapper;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Dtos;
using Hospital.Domain.Models;
using Hospital.Domain;

namespace Hospital.Application.Services;

/// <summary>
/// Service for analyze repositories 
/// </summary>
/// <param name="patientRepository">Patient repository</param>
/// <param name="doctorRepository">Doctor repository</param>
/// <param name="appointmentRepository">Appointment repository</param>
/// <param name="mapper">Mapper for DTOs</param>
public class LibraryAnalyticsService(
    IRepository<Patient, int> patientRepository,
    IRepository<Doctor, int> doctorRepository,
    IRepository<Appointment, int> appointmentRepository,
    IMapper mapper
    ) : ILibraryAnalyticsService
{
    /// <summary>
    /// Get doctors with at least some years of experience
    /// </summary>
    /// <param name="year">years</param>
    /// <returns>List of doctors</returns>
    public List<DoctorDto> GetDoctorsWithExperienceAtLeastYears(int year)
    {
        return mapper.Map<List<DoctorDto>>((
            from d in doctorRepository.ReadAll()
            where d.ExperienceYears >= year
            select d
        ).ToList());
    }

    /// <summary>
    /// Get patients assigned to a specific doctor
    /// </summary>
    /// <param name="doctorId">ID</param>
    /// <returns>List of patient</returns>
    public List<PatientDto> GetPatientsByDoctor(int doctorId)
    {
        return mapper.Map<List<PatientDto>>((from a in appointmentRepository.ReadAll()
            where a.Doctor.Id == doctorId
            select a.Patient).ToList());
    }

    /// <summary>
    /// Get counting of repeat patient appointments in specific period.
    /// </summary>
    /// <param name="start">Start period</param>
    /// <param name="end">End period</param>
    /// <returns>Counting</returns>
    public int GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod(DateTime start, DateTime end)
    {
        return (from a in appointmentRepository.ReadAll()
            where a.AppointmentDateTime >= start && a.AppointmentDateTime <= end
            select a.Id).Count();
    }

    /// <summary>
    /// Get patients over some year old who have
    /// appointments with multiple doctors
    /// </summary>
    /// <param name="age">Age of patient</param>
    /// <returns>List of patients</returns>
    public List<PatientDto> GetPatientsOlderThanWithMultipleDoctors(int age)
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

    /// <summary>
    /// Get appointments in specific period
    /// happening in a specific room. 
    /// </summary>
    /// <param name="roomId">ID of room</param>
    /// <param name="start">Start period</param>
    /// <param name="end">End period</param>
    /// <returns>List of appointments</returns>
    public List<AppointmentDto> GetAppointmentsWhenInSpecificRoomInSpecificPeriod(int roomId,  DateTime start, DateTime end)
    {
        return mapper.Map<List<AppointmentDto>>((from a in appointmentRepository.ReadAll()
            where a.RoomNumber == roomId
            where a.AppointmentDateTime >= start && a.AppointmentDateTime <= end
            select a).ToList());
    }
}