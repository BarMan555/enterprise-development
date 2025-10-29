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
/// <param name="mapper">Mapper for DTOs</param>
public class DoctorService(
    IRepository<Doctor, int> repository, 
    IRepository<Appointment, int> appointmentRepository,
    IMapper mapper) 
    : IDoctorService
{
    /// <summary>
    /// Create DTO entity
    /// </summary>
    /// <param name="dto">DTO for creating</param>
    /// <returns>DTO entity</returns>
    public int Create(DoctorCreateUpdateDto entity)
    {
        return repository.Create(mapper.Map<Doctor>(entity));
    }

    /// <summary>
    /// Get all DTO from repository
    /// </summary>
    /// <returns>DTO</returns>
    public List<DoctorGetDto> GetAll()
    {
        return mapper.Map<List<DoctorGetDto>>(repository.ReadAll());
    }

    /// <summary>
    /// Get DTO from repository by ID
    /// </summary>
    /// <param name="dtoId">ID</param>
    /// <returns>DTO</returns>
    public DoctorGetDto? Get(int id)
    {
        return mapper.Map<DoctorGetDto>(repository.Read(id));
    }

    /// <summary>
    /// Update entity's data by new DTO 
    /// </summary>
    /// <param name="dtoId">ID old entity</param>
    /// <param name="dto">New DTO</param>
    /// <returns></returns>
    public DoctorGetDto? Update(int id, DoctorCreateUpdateDto entity)
    {
        return mapper.Map<DoctorGetDto>(repository.Update(id, mapper.Map<Doctor>(entity)));
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
    
    /// <inheritdoc cref="IDoctorService"/>
    public List<AppointmentGetDto> GetAppointmentsByDoctor(int id)
    {
        return mapper.Map<List<AppointmentGetDto>>(
            from  appointment in appointmentRepository.ReadAll() 
            where appointment.Doctor.Id == id 
            select appointment
        );
    }
}