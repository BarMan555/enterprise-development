using AutoMapper;
using Hospital.Application.Contracts.Dtos;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Domain;
using Hospital.Domain.Enums;
using Hospital.Domain.Models;
using MongoDB.Bson;

namespace Hospital.Application.Services;

/// <summary>
/// Service for patients
/// </summary>
/// <param name="repository">repository of patients</param>
/// <param name="appointmentRepository">repository od appointments</param>
/// <param name="mapper">Mapper for DTOs</param>
public class PatientService(
    IRepository<Patient, ObjectId> repository, 
    IRepository<Appointment, ObjectId> appointmentRepository,
    IMapper mapper) 
    : IPatientService
{
    /// <inheritdoc cref="IPatientService"/>
    public async Task<PatientGetDto> Create(PatientCreateUpdateDto entity)
    { 
        var newPatient = mapper.Map<Patient>(entity);
        var created = await repository.Create(newPatient);
        return mapper.Map<PatientGetDto>(created);
    }

    /// <inheritdoc cref="IPatientService"/>
    public async Task<List<PatientGetDto>> GetAll()
    {
        var patients = await repository.ReadAll();
        var patientsDto = mapper.Map<List<PatientGetDto>>(patients);
        return patientsDto;
    }

    /// <inheritdoc cref="IPatientService"/>
    public async Task<PatientGetDto> Get(ObjectId id)
    {
        var patient = await repository.Read(id);
        var patientDto = mapper.Map<PatientGetDto>(patient);
        return patientDto;
    }

    /// <inheritdoc cref="IPatientService"/>
    public async Task<PatientGetDto> Update(ObjectId id, PatientCreateUpdateDto entity)
    {
        var updatedPatient = mapper.Map<Patient>(entity);
        return mapper.Map<PatientGetDto>(await repository.Update(id, updatedPatient));
    }

    /// <inheritdoc cref="IPatientService"/>
    public async Task<bool> Delete(ObjectId id)
    {
        return await repository.Delete(id);
    }
    
    /// <inheritdoc cref="IPatientService"/>
    public async Task<List<AppointmentGetDto>?> GetAppointmentsByPatient(ObjectId id)
    {
        var appointments = (
            from appointment in (await appointmentRepository.ReadAll())
            where appointment.Patient.Id == id
            select appointment
        ).ToList();
        if (appointments.Count == 0) 
            return null;
        
        var appointmnetsDto =  mapper.Map<List<AppointmentGetDto>>(appointments);
        return appointmnetsDto;
    }
}