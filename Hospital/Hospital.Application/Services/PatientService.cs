using AutoMapper;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Dtos;
using Hospital.Domain;
using Hospital.Domain.Models;
using Hospital.Domain.Enums;

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
    : IPatientService
{
    /// <summary>
    /// Create DTO entity
    /// </summary>
    /// <param name="dto">DTO for creating</param>
    /// <returns>DTO entity</returns>
    public int Create(PatientCreateUpdateDto entity)
    { 
        var newPatient = mapper.Map<Patient>(entity);
        newPatient.Gender = (Gender)entity.Gender; 
        newPatient.BloodType = (BloodType)entity.BloodType; 
        newPatient.RhFactor = (RhFactor)entity.RhFactor; 
        return repository.Create(newPatient);
    }

    /// <summary>
    /// Get all DTO from repository
    /// </summary>
    /// <returns>DTO</returns>
    public List<PatientGetDto> GetAll()
    {
        var patients = repository.ReadAll();
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
    /// Get DTO from repository by ID
    /// </summary>
    /// <param name="dtoId">ID</param>
    /// <returns>DTO</returns>
    public PatientGetDto? Get(int id)
    {
        var patient = repository.Read(id);
        var patientDto = mapper.Map<PatientGetDto>(patient);
        patientDto.Gender = (int)patient.Gender;
        patientDto.BloodType = (int)patient.BloodType;
        patientDto.RhFactor = (int)patient.RhFactor;
        return patientDto;
    }

    /// <summary>
    /// Update entity's data by new DTO 
    /// </summary>
    /// <param name="dtoId">ID old entity</param>
    /// <param name="dto">New DTO</param>
    /// <returns></returns>
    public PatientGetDto? Update(int id, PatientCreateUpdateDto entity)
    {
        var updatedPatient = mapper.Map<Patient>(entity);
        updatedPatient.Gender = (Gender)entity.Gender;
        updatedPatient.BloodType = (BloodType)entity.BloodType;
        updatedPatient.RhFactor = (RhFactor)entity.RhFactor;
        return mapper.Map<PatientGetDto>(repository.Update(id, updatedPatient));
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
    public List<AppointmentGetDto> GetAppointmentsByPatient(int id)
    {
        var appointmets = (
            from appointment in appointmentRepository.ReadAll()
            where appointment.Patient.Id == id
            select appointment
        ).ToList();
        var appointmnetsDto =  mapper.Map<List<AppointmentGetDto>>(appointmets);
        
        for (var i = 0; i < appointmnetsDto.Count; i++)
            appointmnetsDto[i].IdDoctor = appointmets[i].Doctor.Id;
        
        for (var i = 0; i < appointmnetsDto.Count; i++)
            appointmnetsDto[i].IdPatient = id;
        
        return appointmnetsDto;
    }
}