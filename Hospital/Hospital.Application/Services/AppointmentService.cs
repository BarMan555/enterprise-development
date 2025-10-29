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
        return repository.Create(mapper.Map<Appointment>(entity));
    }

    /// <summary>
    /// Get all DTO from repository
    /// </summary>
    /// <returns>DTO</returns>
    public List<AppointmentGetDto> GetAll()
    {
        return mapper.Map<List<AppointmentGetDto>>(repository.ReadAll());
    }

    /// <summary>
    /// Get DTO from repository by ID
    /// </summary>
    /// <param name="dtoId">ID</param>
    /// <returns>DTO</returns>
    public AppointmentGetDto? Get(int id)
    {
        return mapper.Map<AppointmentGetDto>(repository.Read(id));
    }

    /// <summary>
    /// Update entity's data by new DTO 
    /// </summary>
    /// <param name="dtoId">ID old entity</param>
    /// <param name="dto">New DTO</param>
    /// <returns></returns>
    public AppointmentGetDto? Update(int id, AppointmentCreateUpdateDto entity)
    {
        return mapper.Map<AppointmentGetDto>(repository.Update(id, mapper.Map<Appointment>(entity)));
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
        return mapper.Map<DoctorGetDto>(repository.Read(id)?.Doctor);
    }
    
    /// <inheritdoc cref="IAppointmentService"/>
    public PatientGetDto GetParientByAppointment(int id)
    {
        return mapper.Map<PatientGetDto>(repository.Read(id)?.Patient);
    }
}