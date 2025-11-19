using AutoMapper;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Dtos;
using Hospital.Domain;
using Hospital.Domain.Models;
using Hospital.Domain.Enums;
using MongoDB.Bson;

namespace Hospital.Application.Services;

/// <summary>
/// Service for doctors
/// </summary>
/// <param name="repository">repository of doctors</param>
/// <param name="appointmentRepository">repository od appointments</param>
/// <param name="mapper">Mapper for DTOs</param>
public class DoctorService(
    IRepositoryAsync<Doctor, ObjectId> repository, 
    IRepositoryAsync<Appointment, ObjectId> appointmentRepository,
    IMapper mapper) 
    : IDoctorService
{
    /// <inheritdoc cref="IDoctorService"/>
    public async Task<DoctorGetDto> Create(DoctorCreateUpdateDto entity)
    {
        var newDoctor = mapper.Map<Doctor>(entity);
        
        newDoctor.Id = ObjectId.GenerateNewId();
        newDoctor.Specialization ??= new Specialization{Id=0, Name=null};
        newDoctor.Specialization.Id = entity.IdSpecialization; 
        
        var created = await repository.Create(newDoctor);
        return mapper.Map<DoctorGetDto>(created);
    }

    /// <inheritdoc cref="IDoctorService"/>
    public async Task<List<DoctorGetDto>> GetAll()
    {
        var doctors = await repository.ReadAll();
        var doctorsDto = mapper.Map<List<DoctorGetDto>>(doctors);
        
        for (var i = 0; i < doctorsDto.Count; i++)
            doctorsDto[i].IdSpecialization = doctors[i].Specialization.Id;
        
        return doctorsDto;
    }

    /// <inheritdoc cref="IDoctorService"/>
    public async Task<DoctorGetDto> Get(ObjectId id)
    {
        var doctor = await repository.Read(id);
        var doctorDto = mapper.Map<DoctorGetDto>(doctor);
        doctorDto.IdSpecialization = doctor.Specialization.Id;
        return doctorDto;
    }

    /// <inheritdoc cref="IDoctorService"/>
    public async Task<DoctorGetDto> Update(ObjectId id, DoctorCreateUpdateDto entity)
    {
        var updatedDoctor = mapper.Map<Doctor>(entity);
        
        updatedDoctor.Specialization ??= new Specialization{Id=0, Name=null};
        updatedDoctor.Specialization.Id = entity.IdSpecialization;
        
        return mapper.Map<DoctorGetDto>(await repository.Update(id, updatedDoctor));
    }

    /// <inheritdoc cref="IDoctorService"/>
    public async Task<bool> Delete(ObjectId id)
    {
        return await repository.Delete(id);
    }
    
    /// <inheritdoc cref="IDoctorService"/>
    public async Task<List<AppointmentGetDto>?> GetAppointmentsByDoctor(ObjectId id)
    {
        var appointments = (
            from appointment in await appointmentRepository.ReadAll()
            where appointment.Doctor.Id == id
            select appointment
        ).ToList();
        if (!appointments.Any()) 
            return null;
        
        var appointmentsDto =  mapper.Map<List<AppointmentGetDto>>(appointments);
        
        for (var i = 0; i < appointmentsDto.Count; i++)
            appointmentsDto[i].IdDoctor = id.ToString();
        
        for (var i = 0; i < appointmentsDto.Count; i++)
            appointmentsDto[i].IdPatient = appointments[i].Patient.Id.ToString();
        
        return appointmentsDto;
    }
}