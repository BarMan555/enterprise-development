using AutoMapper;
using Hospital.Application.Contracts.Dtos;
using Hospital.Domain.Models;

namespace Hospital.Application;

/// <summary>
/// Global Mapster configuration for mapping between Domain and DTO models
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Profile for mappings between domain models and DTOs
    /// </summary>
    public MappingProfile()
    {
        CreateMap<Patient, PatientGetDto>();
        CreateMap<PatientCreateUpdateDto, Patient>();

        CreateMap<Doctor, DoctorGetDto>();
        CreateMap<DoctorCreateUpdateDto, Doctor>();
        
        CreateMap<Appointment, AppointmentGetDto>();
        CreateMap<AppointmentCreateUpdateDto, Appointment>();
    }
}