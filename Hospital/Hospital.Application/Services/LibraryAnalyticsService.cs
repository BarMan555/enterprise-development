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
    public async Task<List<DoctorGetDto>> GetDoctorsWithExperienceAtLeastYears(int year)
    {
        var doctors = (
            from d in await doctorRepository.ReadAll()
            where d.ExperienceYears >= year
            select d
        ).ToList();
        var doctorsDto = mapper.Map<List<DoctorGetDto>>(doctors);
        
        for (var i = 0; i < doctorsDto.Count; i++)
        {
            doctorsDto[i].IdSpecialization = doctors[i].Specialization.Id;
        }
        return doctorsDto;
    }

    /// <summary>
    /// Get patients assigned to a specific doctor
    /// </summary>
    /// <param name="doctorId">ID</param>
    /// <returns>List of patient</returns>
    public async Task<List<PatientGetDto>> GetPatientsByDoctor(int doctorId)
    {
        var patients = ((from a in await appointmentRepository.ReadAll()
            where a.Doctor.Id == doctorId
            select a.Patient).ToList());
        var patientsDto = mapper.Map<List<PatientGetDto>>(patients);
        for (var i = 0; i < patientsDto.Count; i++)
        {
            patientsDto[i].Gender = (int)patients[i].Gender;
            patientsDto[i].BloodType = (int)patients[i].BloodType;
            patientsDto[i].RhFactor = (int)patients[i].RhFactor;
        }
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
    public async Task<List<PatientGetDto>> GetPatientsOlderThanWithMultipleDoctors(int age)
    {
        var today = DateTime.Today;
        var patients = (await patientRepository.ReadAll()).Where(p => (today - p.DateOfBirth).Days / 365 >= age).ToList();
        var appointments = appointmentRepository.ReadAll();

        var patientDoctorGroups = (await appointments)
            .GroupBy(a => a.Patient.Id)
            .ToDictionary(
                g => g.Key,
                g => g.Select(a => a.Doctor.Id).Distinct().ToList()
            );

        var result = patients
            .Where(p => patientDoctorGroups.ContainsKey(p.Id) &&
                        patientDoctorGroups[p.Id].Count > 1)
            .ToList();
        
        var patientsDto = mapper.Map<List<PatientGetDto>>(result);
        
        for (var i = 0; i < patientsDto.Count; i++)
        {
            patientsDto[i].Gender = (int)result[i].Gender;
            patientsDto[i].BloodType = (int)result[i].BloodType;
            patientsDto[i].RhFactor = (int)result[i].RhFactor;
        }

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
    public async Task<List<AppointmentGetDto>> GetAppointmentsWhenInSpecificRoomInSpecificPeriod(int roomId,  DateTime start, DateTime end)
    {
         var appointments = (from a in await appointmentRepository.ReadAll()
            where a.RoomNumber == roomId
            where a.AppointmentDateTime >= start && a.AppointmentDateTime <= end
            select a).ToList();
         var appointmentsDto = mapper.Map<List<AppointmentGetDto>>(appointments);
         for(var i = 0; i < appointmentsDto.Count; i++)
         {
             appointmentsDto[i].IdDoctor = appointments[i].Doctor.Id;
             appointmentsDto[i].IdPatient = appointments[i].Patient.Id;
         }
         return appointmentsDto;
    }
}