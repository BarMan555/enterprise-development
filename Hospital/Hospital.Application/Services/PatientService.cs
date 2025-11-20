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
    IRepositoryAsync<Patient, ObjectId> repository, 
    IRepositoryAsync<Appointment, ObjectId> appointmentRepository,
    IMapper mapper) 
    : IPatientService
{
    /// <inheritdoc cref="IPatientService"/>
    public async Task<PatientGetDto> Create(PatientCreateUpdateDto entity)
    { 
        var newPatient = mapper.Map<Patient>(entity);
        newPatient.Gender = (Gender)entity.Gender; 
        newPatient.BloodType = (BloodType)entity.BloodType; 
        newPatient.RhFactor = (RhFactor)entity.RhFactor; 
        var created = await repository.Create(newPatient);
        return mapper.Map<PatientGetDto>(created);
    }

    /// <inheritdoc cref="IPatientService"/>
    public async Task<List<PatientGetDto>> GetAll()
    {
        var patients = await repository.ReadAll();
        var patientsDto = mapper.Map<List<PatientGetDto>>(patients);

        for (var i = 0; i < patientsDto.Count; i++)
        {
            patientsDto[i].Gender = (int)patients[i].Gender;
            patientsDto[i].BloodType = (int)patients[i].BloodType;
            patientsDto[i].RhFactor = (int)patients[i].RhFactor;
        }

        return patientsDto;
    }

    /// <inheritdoc cref="IPatientService"/>
    public async Task<PatientGetDto> Get(ObjectId id)
    {
        var patient = await repository.Read(id);
        var patientDto = mapper.Map<PatientGetDto>(patient);
        patientDto.Gender = (int)patient.Gender;
        patientDto.BloodType = (int)patient.BloodType;
        patientDto.RhFactor = (int)patient.RhFactor;
        return patientDto;
    }

    /// <inheritdoc cref="IPatientService"/>
    public async Task<PatientGetDto> Update(ObjectId id, PatientCreateUpdateDto entity)
    {
        var updatedPatient = mapper.Map<Patient>(entity);
        updatedPatient.Gender = (Gender)entity.Gender;
        updatedPatient.BloodType = (BloodType)entity.BloodType;
        updatedPatient.RhFactor = (RhFactor)entity.RhFactor;
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
        
        for (var i = 0; i < appointmnetsDto.Count; i++)
            appointmnetsDto[i].IdDoctor = appointments[i].Doctor.Id.ToString();
        
        for (var i = 0; i < appointmnetsDto.Count; i++)
            appointmnetsDto[i].IdPatient = id.ToString();
        
        return appointmnetsDto;
    }
}