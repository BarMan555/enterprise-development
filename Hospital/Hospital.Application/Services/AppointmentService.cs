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
public class AppointmentService(IRepository<Appointment, int> repository, IMapper mapper) : IApplicationService<AppointmentDto, int>
{
    /// <summary>
    /// Create DTO entity
    /// </summary>
    /// <param name="dto">DTO for creating</param>
    /// <returns>DTO entity</returns>
    public int Create(AppointmentDto entity)
    {
        return repository.Create(mapper.Map<Appointment>(entity));
    }

    /// <summary>
    /// Get all DTO from repository
    /// </summary>
    /// <returns>DTO</returns>
    public List<AppointmentDto> GetAll()
    {
        return mapper.Map<List<AppointmentDto>>(repository.ReadAll());
    }

    /// <summary>
    /// Get DTO from repository by ID
    /// </summary>
    /// <param name="dtoId">ID</param>
    /// <returns>DTO</returns>
    public AppointmentDto? Get(int id)
    {
        return mapper.Map<AppointmentDto>(repository.Read(id));
    }

    /// <summary>
    /// Update entity's data by new DTO 
    /// </summary>
    /// <param name="dtoId">ID old entity</param>
    /// <param name="dto">New DTO</param>
    /// <returns></returns>
    public AppointmentDto? Update(int id, AppointmentDto entity)
    {
        return mapper.Map<AppointmentDto>(repository.Update(id, mapper.Map<Appointment>(entity)));
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
}