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
        var newDoctor = mapper.Map<Doctor>(entity);
        newDoctor.Specialization.Id = entity.IdSpecialization; 
        return repository.Create(newDoctor);
    }

    /// <summary>
    /// Get all DTO from repository
    /// </summary>
    /// <returns>DTO</returns>
    public List<DoctorGetDto> GetAll()
    {
        var doctors = repository.ReadAll();
        var doctorsDto = mapper.Map<List<DoctorGetDto>>(doctors); 
        foreach (var doctorDto in doctorsDto)
            foreach (var doctor in doctors)
                doctorDto.Specialization = doctor.Specialization.Id;
        return doctorsDto;
    }

    /// <summary>
    /// Get DTO from repository by ID
    /// </summary>
    /// <param name="dtoId">ID</param>
    /// <returns>DTO</returns>
    public DoctorGetDto? Get(int id)
    {
        var doctor = repository.Read(id);
        var doctorDto = mapper.Map<DoctorGetDto>(doctor);
        doctorDto.Specialization = doctor.Specialization.Id;
        return doctorDto;
    }

    /// <summary>
    /// Update entity's data by new DTO 
    /// </summary>
    /// <param name="dtoId">ID old entity</param>
    /// <param name="dto">New DTO</param>
    /// <returns></returns>
    public DoctorGetDto? Update(int id, DoctorCreateUpdateDto entity)
    {
        var updatedDoctor = mapper.Map<Doctor>(entity);
        updatedDoctor.Specialization.Id = entity.IdSpecialization;
        return mapper.Map<DoctorGetDto>(repository.Update(id, updatedDoctor));
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
        var appointmets = (
            from appointment in appointmentRepository.ReadAll()
            where appointment.Doctor.Id == id
            select appointment
        );
        var appointmnetsDto =  mapper.Map<List<AppointmentGetDto>>(appointmets);
        
        foreach (var appointmentDto in appointmnetsDto)
            appointmentDto.IdDoctor = id;
        
        foreach (var appointmentDto in appointmnetsDto)
            foreach (var appointment in appointmets)
                appointmentDto.IdPatient = appointment.Patient.Id;
        
        return appointmnetsDto;
    }
}