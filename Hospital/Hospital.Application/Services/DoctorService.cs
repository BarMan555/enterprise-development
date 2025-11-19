using AutoMapper;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Dtos;
using Hospital.Domain;
using Hospital.Domain.Models;

namespace Hospital.Application.Services;

/// <summary>
/// Service for doctors
/// </summary>
/// <param name="repository">repository of doctors</param>
/// <param name="appointmentRepository">repository od appointments</param>
/// <param name="mapper">Mapper for DTOs</param>
public class DoctorService(
    IRepositoryAsync<Doctor, int> repository, 
    IRepositoryAsync<Appointment, int> appointmentRepository,
    IMapper mapper) 
    : IDoctorService
{
    /// <summary>
    /// Create DTO entity
    /// </summary>
    /// <param name="entity">DTO for creating</param>
    /// <returns>DTO entity</returns>
    public async Task<DoctorGetDto> Create(DoctorCreateUpdateDto entity)
    {
        var newDoctor = mapper.Map<Doctor>(entity);
        newDoctor.Specialization.Id = entity.IdSpecialization; 
        var created = await repository.Create(newDoctor);
        return mapper.Map<DoctorGetDto>(created);
    }

    /// <summary>
    /// Get all DTO from repository
    /// </summary>
    /// <returns>DTO</returns>
    public async Task<List<DoctorGetDto>> GetAll()
    {
        var doctors = await repository.ReadAll();
        var doctorsDto = mapper.Map<List<DoctorGetDto>>(doctors);
        
        for (var i = 0; i < doctorsDto.Count; i++)
            doctorsDto[i].IdSpecialization = doctors[i].Specialization.Id;
        
        return doctorsDto;
    }

    /// <summary>
    /// Get DTO from repository by ID
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>DTO</returns>
    public async Task<DoctorGetDto> Get(int id)
    {
        var doctor = await repository.Read(id);
        var doctorDto = mapper.Map<DoctorGetDto>(doctor);
        doctorDto.IdSpecialization = doctor.Specialization.Id;
        return doctorDto;
    }

    /// <summary>
    /// Update entity's data by new DTO 
    /// </summary>
    /// <param name="id">ID old entity</param>
    /// <param name="entity">New DTO</param>
    /// <returns></returns>
    public async Task<DoctorGetDto> Update(int id, DoctorCreateUpdateDto entity)
    {
        var updatedDoctor = mapper.Map<Doctor>(entity);
        updatedDoctor.Specialization.Id = entity.IdSpecialization;
        return mapper.Map<DoctorGetDto>(await repository.Update(id, updatedDoctor));
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
    
    /// <inheritdoc cref="IDoctorService"/>
    public async Task<List<AppointmentGetDto>> GetAppointmentsByDoctor(int id)
    {
        var appointmets = (
            from appointment in await appointmentRepository.ReadAll()
            where appointment.Doctor.Id == id
            select appointment
        ).ToList();
        var appointmnetsDto =  mapper.Map<List<AppointmentGetDto>>(appointmets);
        
        for (var i = 0; i < appointmnetsDto.Count; i++)
            appointmnetsDto[i].IdDoctor = id;
        
        for (var i = 0; i < appointmnetsDto.Count; i++)
            appointmnetsDto[i].IdPatient = appointmets[i].Patient.Id;
        
        return appointmnetsDto;
    }
}