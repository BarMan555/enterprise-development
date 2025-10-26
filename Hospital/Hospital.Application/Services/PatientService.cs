using AutoMapper;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Dtos;
using Hospital.Domain;
using Hospital.Domain.Models;

namespace Hospital.Application.Services;

/// <summary>
/// Service for patients
/// </summary>
/// <param name="repository">repository of patients</param>
/// <param name="mapper">Mapper for DTOs</param>
public class PatientService(
    IRepository<Patient, int> repository, 
    IRepository<Appointment, int> appointmentRepository,
    IMapper mapper) 
    : IApplicationService<PatientDto, int>
{
    /// <summary>
    /// Create DTO entity
    /// </summary>
    /// <param name="dto">DTO for creating</param>
    /// <returns>DTO entity</returns>
    public int Create(PatientDto entity)
    {
        return repository.Create(mapper.Map<Patient>(entity));
    }

    /// <summary>
    /// Get all DTO from repository
    /// </summary>
    /// <returns>DTO</returns>
    public List<PatientDto> GetAll()
    {
        return mapper.Map<List<PatientDto>>(repository.ReadAll());
    }

    /// <summary>
    /// Get DTO from repository by ID
    /// </summary>
    /// <param name="dtoId">ID</param>
    /// <returns>DTO</returns>
    public PatientDto? Get(int id)
    {
        return mapper.Map<PatientDto>(repository.Read(id));
    }

    /// <summary>
    /// Update entity's data by new DTO 
    /// </summary>
    /// <param name="dtoId">ID old entity</param>
    /// <param name="dto">New DTO</param>
    /// <returns></returns>
    public PatientDto? Update(int id, PatientDto entity)
    {
        return mapper.Map<PatientDto>(repository.Update(id, mapper.Map<Patient>(entity)));
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
    
    /// <inheritdoc cref="IPatientService"/>
    public List<AppointmentDto> GetAppointmentsByPatient(int id)
    {
        return mapper.Map<List<AppointmentDto>>(
            from  appointment in appointmentRepository.ReadAll() 
            where appointment.Patient.Id == id 
            select appointment
        );
    }
}