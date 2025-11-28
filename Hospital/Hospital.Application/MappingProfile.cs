using AutoMapper;
using Hospital.Application.Contracts.Dtos;
using Hospital.Domain.Enums;
using Hospital.Domain.Models;
using MongoDB.Bson;

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
        CreateMap<Patient, PatientGetDto>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender))
            .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => (int)src.BloodType))
            .ForMember(dest => dest.RhFactor, opt => opt.MapFrom(src => (int)src.RhFactor));
        
        CreateMap<PatientCreateUpdateDto, Patient>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (Gender)src.Gender))
            .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => (BloodType)src.BloodType))
            .ForMember(dest => dest.RhFactor, opt => opt.MapFrom(src => (RhFactor)src.RhFactor));

        CreateMap<Doctor, DoctorGetDto>()
            .ForMember(
                dest => dest.IdSpecialization,
                opt => opt.MapFrom(src => src.SpecializationId.ToString()));

        CreateMap<DoctorCreateUpdateDto, Doctor>()
            .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(src => ObjectId.Parse(src.IdSpecialization)));

        
        CreateMap<Appointment, AppointmentGetDto>()
            .ForMember(
                dest => dest.IdPatient,
                opt => opt.MapFrom(src => src.PatientId.ToString()))
            .ForMember(
                dest => dest.IdDoctor,
                opt => opt.MapFrom(src => src.DoctorId.ToString()));

        CreateMap<AppointmentCreateUpdateDto, Appointment>()
            .ForMember(dest => dest.PatientId,
                opt => opt.MapFrom(src => ObjectId.Parse(src.IdPatient)))
            .ForMember(dest => dest.DoctorId,
                opt => opt.MapFrom(src => ObjectId.Parse(src.IdDoctor)));
    }
}