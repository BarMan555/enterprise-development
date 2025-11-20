using AutoMapper;
using Hospital.Application.Contracts.Dtos;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Domain;
using Hospital.Domain.Models;
using MongoDB.Bson;

namespace Hospital.Application.Services;

/// <summary>
/// Service for appointments
/// </summary>
/// <param name="repository">repository of appointments</param>
/// <param name="mapper">Mapper for DTOs</param>
public class AppointmentService(
    IRepositoryAsync<Appointment, ObjectId> repository, 
    IMapper mapper) 
    : IAppointmentService
{
    /// <inheritdoc cref="IAppointmentService"/>
    public async Task<AppointmentGetDto> Create(AppointmentCreateUpdateDto entity)
    {
        var newAppointment = mapper.Map<Appointment>(entity);
        var created = await repository.Create(newAppointment);
        return mapper.Map<AppointmentGetDto>(created);
    }

    /// <inheritdoc cref="IAppointmentService"/>
    public async Task<List<AppointmentGetDto>> GetAll()
    {
        var appointments = await repository.ReadAll();
        var appointmentsDto = mapper.Map<List<AppointmentGetDto>>(appointments);
        return appointmentsDto;
    }

    /// <inheritdoc cref="IAppointmentService"/>
    public async Task<AppointmentGetDto> Get(ObjectId id)
    {
        var appointment = await repository.Read(id);
        var appointmentDto = mapper.Map<AppointmentGetDto>(appointment);
        return appointmentDto;
    }

    /// <inheritdoc cref="IAppointmentService"/>
    public async Task<AppointmentGetDto> Update(ObjectId id, AppointmentCreateUpdateDto entity)
    {
        var updatedAppointment = mapper.Map<Appointment>(entity);
        return mapper.Map<AppointmentGetDto>(await repository.Update(id, updatedAppointment));
    }

    /// <inheritdoc cref="IAppointmentService"/>
    public async Task<bool> Delete(ObjectId id)
    {
        return await repository.Delete(id);
    }

    /// <inheritdoc cref="IAppointmentService"/>
    public async Task<DoctorGetDto> GetDoctorByAppointment(ObjectId id)
    {
        var doctor = (await repository.Read(id))?.Doctor;
        var doctorDto = mapper.Map<DoctorGetDto>(doctor);
        return doctorDto;
    }
    
    /// <inheritdoc cref="IAppointmentService"/>
    public async Task<PatientGetDto> GetParientByAppointment(ObjectId id)
    {
        var patient = (await repository.Read(id))?.Patient;
        var patientDto = mapper.Map<PatientGetDto>(patient);
        return patientDto;
    }
}