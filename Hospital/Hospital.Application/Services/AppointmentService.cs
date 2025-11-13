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
    public async Task<AppointmentGetDto> Create(AppointmentCreateUpdateDto entity)
    {
        var newAppointment = mapper.Map<Appointment>(entity);
        newAppointment.Doctor.Id = entity.IdDoctor; 
        newAppointment.Patient.Id = entity.IdPatient; 
        var created = await repository.Create(newAppointment);
        return mapper.Map<AppointmentGetDto>(created);
    }

    /// <summary>
    /// Get all DTO from repository
    /// </summary>
    /// <returns>DTO</returns>
    public async Task<List<AppointmentGetDto>> GetAll()
    {
        var appointments = await repository.ReadAll();
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
    /// <param name="id">ID</param>
    /// <returns>DTO</returns>
    public async Task<AppointmentGetDto> Get(int id)
    {
        var appointment = await repository.Read(id);
        var appointmentDto = mapper.Map<AppointmentGetDto>(appointment);
        appointmentDto.IdDoctor = appointment.Doctor.Id;
        appointmentDto.IdPatient = appointment.Patient.Id;
        return appointmentDto;
    }

    /// <summary>
    /// Update entity's data by new DTO 
    /// </summary>
    /// <param name="id">ID old entity</param>
    /// <param name="entity">New DTO</param>
    /// <returns></returns>
    public async Task<AppointmentGetDto> Update(int id, AppointmentCreateUpdateDto entity)
    {
        var updatedAppointment = mapper.Map<Appointment>(entity);
        updatedAppointment.Doctor.Id = entity.IdDoctor;
        updatedAppointment.Patient.Id = entity.IdPatient;
        return mapper.Map<AppointmentGetDto>(await repository.Update(id, updatedAppointment));
    }

    /// <summary>
    /// Delete entity from repository
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>Result of deleting</returns>
    public async Task<bool> Delete(int id)
    {
        return await repository.Delete(id);
    }

    /// <inheritdoc cref="IAppointmentService"/>
    public async Task<DoctorGetDto> GetDoctorByAppointment(int id)
    {
        var doctor = (await repository.Read(id))?.Doctor;
        var doctorDto = mapper.Map<DoctorGetDto>(doctor);
        doctorDto.IdSpecialization = doctor.Specialization.Id;
        return doctorDto;
    }
    
    /// <inheritdoc cref="IAppointmentService"/>
    public async Task<PatientGetDto> GetParientByAppointment(int id)
    {
        var patient = (await repository.Read(id))?.Patient;
        var patientDto = mapper.Map<PatientGetDto>(patient);
        patientDto.Gender = (int)patient.Gender;
        patientDto.BloodType = (int)patient.BloodType;
        patientDto.RhFactor = (int)patient.RhFactor;
        return patientDto;
    }
}