using AutoMapper;
using Hospital.Application.Contracts.Dtos;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Domain;
using Hospital.Domain.Models;
using MongoDB.Bson;

namespace Hospital.Application.Services;

/// <summary>
/// Service for analyze repositories 
/// </summary>
/// <param name="patientRepository">Patient repository</param>
/// <param name="doctorRepository">Doctor repository</param>
/// <param name="appointmentRepository">Appointment repository</param>
/// <param name="mapper">Mapper for DTOs</param>
public class LibraryAnalyticsService(
    IRepository<Patient, ObjectId> patientRepository,
    IRepository<Doctor, ObjectId> doctorRepository,
    IRepository<Appointment, ObjectId> appointmentRepository,
    IMapper mapper
    ) : ILibraryAnalyticsService
{
    /// <summary>
    /// Get doctors with at least some years of experience
    /// </summary>
    /// <param name="year">years</param>
    /// <returns>List of doctors</returns>
    public async Task<List<DoctorGetDto>?> GetDoctorsWithExperienceAtLeastYears(int year)
    {
        var doctors = (
            from d in await doctorRepository.ReadAll()
            where d.ExperienceYears >= year
            select d
        ).ToList();
        
        if (doctors.Count == 0)
            return null;
        
        var doctorsDto = mapper.Map<List<DoctorGetDto>>(doctors);
        return doctorsDto;
    }

    /// <summary>
    /// Get patients assigned to a specific doctor
    /// </summary>
    /// <param name="doctorId">ID</param>
    /// <returns>List of patient</returns>
    public async Task<List<PatientGetDto>?> GetPatientsByDoctor(ObjectId doctorId)
    {
        var patients = ((from a in await appointmentRepository.ReadAll()
            where a.DoctorId == doctorId
            select a.Patient).ToList());
        
        if (patients.Count == 0) 
            return null;
        
        var patientsDto = mapper.Map<List<PatientGetDto>>(patients);
        return patientsDto;
    }

    /// <summary>
    /// Get counting of repeat patient appointments in specific period.
    /// </summary>
    /// <param name="start">Start period</param>
    /// <param name="end">End period</param>
    /// <returns>Counting</returns>
    public async Task<int> GetCountAppointmentsWhenRepeatVisitsInSpecificPeriod(DateTime start, DateTime end)
    {
        return (from a in await appointmentRepository.ReadAll()
            where a.AppointmentDateTime >= start && a.AppointmentDateTime <= end
            select a.Id).Count();
    }

    /// <summary>
    /// Get patients over some year old who have
    /// appointments with multiple doctors
    /// </summary>
    /// <param name="age">Age of patient</param>
    /// <returns>List of patients</returns>
    public async Task<List<PatientGetDto>?> GetPatientsOlderThanWithMultipleDoctors(int age)
    {
        var today = DateTime.Today;
        var patients = (await patientRepository.ReadAll())?.Where(p => (today - p.DateOfBirth).Days / 365 >= age).ToList();
        var appointments = appointmentRepository.ReadAll();
        
        if (patients == null || patients.Count == 0)
            return null;

        var patientDoctorGroups = (await appointments)?
            .GroupBy(a => a.PatientId)
            .ToDictionary(
                g => g.Key,
                g => g.Select(a => a.DoctorId).Distinct().ToList()
            );
        
        if (patientDoctorGroups == null || patientDoctorGroups.Count == 0)
            return null;

        var result = patients
            .Where(p => patientDoctorGroups.ContainsKey(p.Id) &&
                        patientDoctorGroups[p.Id].Count > 1)
            .ToList();
        
        var patientsDto = mapper.Map<List<PatientGetDto>>(result);
        return patientsDto;
    }

    /// <summary>
    /// Get appointments in specific period
    /// happening in a specific room. 
    /// </summary>
    /// <param name="roomId">ID of room</param>
    /// <param name="start">Start period</param>
    /// <param name="end">End period</param>
    /// <returns>List of appointments</returns>
    public async Task<List<AppointmentGetDto>?> GetAppointmentsWhenInSpecificRoomInSpecificPeriod(int roomId,  DateTime start, DateTime end)
    {
         var appointments = (from a in await appointmentRepository.ReadAll()
            where a.RoomNumber == roomId
            where a.AppointmentDateTime >= start && a.AppointmentDateTime <= end
            select a).ToList();
         
         if (appointments.Count == 0) 
             return null;
         
         var appointmentsDto = mapper.Map<List<AppointmentGetDto>>(appointments);
         return appointmentsDto;
    }
}