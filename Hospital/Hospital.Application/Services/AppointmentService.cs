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

        if (!ObjectId.TryParse(entity.IdDoctor, out var doctorId) || !ObjectId.TryParse(entity.IdPatient, out var patientId))
            throw new ApplicationException("An exception happened during parse doctor or patient id");
        
        newAppointment.DoctorId = doctorId; 
        newAppointment.PatientId = patientId; 
        var created = await repository.Create(newAppointment);
        return mapper.Map<AppointmentGetDto>(created);
    }

    /// <inheritdoc cref="IAppointmentService"/>
    public async Task<List<AppointmentGetDto>> GetAll()
    {
        var appointments = await repository.ReadAll();
        var appointmentsDto = mapper.Map<List<AppointmentGetDto>>(appointments);

        for (var i = 0; i < appointmentsDto.Count; i++)
        {
            appointmentsDto[i].IdDoctor = appointments[i].Doctor.Id.ToString();
            appointmentsDto[i].IdPatient = appointments[i].Patient.Id.ToString();
        }

        return appointmentsDto;
    }

    /// <inheritdoc cref="IAppointmentService"/>
    public async Task<AppointmentGetDto> Get(ObjectId id)
    {
        var appointment = await repository.Read(id);
        var appointmentDto = mapper.Map<AppointmentGetDto>(appointment);
        appointmentDto.IdDoctor = appointment.Doctor.Id.ToString();
        appointmentDto.IdPatient = appointment.Patient.Id.ToString();
        return appointmentDto;
    }

    /// <inheritdoc cref="IAppointmentService"/>
    public async Task<AppointmentGetDto> Update(ObjectId id, AppointmentCreateUpdateDto entity)
    {
        var updatedAppointment = mapper.Map<Appointment>(entity);
        
        if (!ObjectId.TryParse(entity.IdDoctor, out var doctorId) || !ObjectId.TryParse(entity.IdPatient, out var patientId))
            throw new ApplicationException("An exception happened during parse doctor or patient id");
        
        updatedAppointment.Doctor.Id = doctorId;
        updatedAppointment.Patient.Id = patientId;
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
        doctorDto.IdSpecialization = doctor.Specialization.Id;
        return doctorDto;
    }
    
    /// <inheritdoc cref="IAppointmentService"/>
    public async Task<PatientGetDto> GetParientByAppointment(ObjectId id)
    {
        var patient = (await repository.Read(id))?.Patient;
        var patientDto = mapper.Map<PatientGetDto>(patient);
        patientDto.Gender = (int)patient.Gender;
        patientDto.BloodType = (int)patient.BloodType;
        patientDto.RhFactor = (int)patient.RhFactor;
        return patientDto;
    }
}