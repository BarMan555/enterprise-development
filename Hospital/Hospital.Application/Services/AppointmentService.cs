using AutoMapper;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Dtos;
using Hospital.Domain;
using Hospital.Domain.Models;

namespace Hospital.Application.Services;

/// <summary>
/// Service for appointments
/// </summary>
/// <param name="repository">repository of appointments</param>
/// <param name="mapper">Mapper for DTOs</param>
public class AppointmentService(
    IRepository<Appointment, int> repository, 
    IMapper mapper) 
    : IAppointmentService
{
    /// <summary>
    /// Create DTO entity
    /// </summary>
    /// <param name="dto">DTO for creating</param>
    /// <returns>DTO entity</returns>
    public int Create(AppointmentCreateUpdateDto entity)
    {
        var newAppointment = mapper.Map<Appointment>(entity);
        newAppointment.Doctor.Id = entity.IdDoctor; 
        newAppointment.Patient.Id = entity.IdPatient; 
        return repository.Create(newAppointment);
    }

    /// <summary>
    /// Get all DTO from repository
    /// </summary>
    /// <returns>DTO</returns>
    public List<AppointmentGetDto> GetAll()
    {
        var appointments = repository.ReadAll();
        var appointmentsDto = mapper.Map<List<AppointmentGetDto>>(appointments);

        for (var i = 0; i < appointmentsDto.Count; i++)
        {
            appointmentsDto[i].IdDoctor = appointments[i].Doctor.Id;
            appointmentsDto[i].IdPatient = appointments[i].Patient.Id;
        }

        return appointmentsDto;
    }

    /// <summary>
    /// Get DTO from repository by ID
    /// </summary>
    /// <param name="dtoId">ID</param>
    /// <returns>DTO</returns>
    public AppointmentGetDto? Get(int id)
    {
        var appointment = repository.Read(id);
        var appointmentDto = mapper.Map<AppointmentGetDto>(appointment);
        appointmentDto.IdDoctor = appointment.Doctor.Id;
        appointmentDto.IdPatient = appointment.Patient.Id;
        return appointmentDto;
    }

    /// <summary>
    /// Update entity's data by new DTO 
    /// </summary>
    /// <param name="dtoId">ID old entity</param>
    /// <param name="dto">New DTO</param>
    /// <returns></returns>
    public AppointmentGetDto? Update(int id, AppointmentCreateUpdateDto entity)
    {
        var updatedAppointment = mapper.Map<Appointment>(entity);
        updatedAppointment.Doctor.Id = entity.IdDoctor;
        updatedAppointment.Patient.Id = entity.IdPatient;
        return mapper.Map<AppointmentGetDto>(repository.Update(id, updatedAppointment));
    }

    /// <summary>
    /// Delete entity from repository
    /// </summary>
    /// <param name="dtoId">Entity ID</param>
    /// <returns>Result of deleting</returns>
    public bool Delete(int id)
    {
        return repository.Delete(id);
    }

    /// <inheritdoc cref="IAppointmentService"/>
    public DoctorGetDto GetDoctorByAppointment(int id)
    {
        var doctor = repository.Read(id)?.Doctor;
        var doctorDto = mapper.Map<DoctorGetDto>(doctor);
        doctorDto.IdSpecialization = doctor.Specialization.Id;
        return doctorDto;
    }
    
    /// <inheritdoc cref="IAppointmentService"/>
    public PatientGetDto GetParientByAppointment(int id)
    {
        var patient = repository.Read(id)?.Patient;
        var patientDto = mapper.Map<PatientGetDto>(patient);
        patientDto.Gender = (int)patient.Gender;
        patientDto.BloodType = (int)patient.BloodType;
        patientDto.RhFactor = (int)patient.RhFactor;
        return patientDto;
    }
}