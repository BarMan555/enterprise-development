using AutoMapper;
using Hospital.Application.Contracts.Dtos;
using Hospital.Domain.Models;

namespace Hospital.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, PatientDto>().ReverseMap();
        CreateMap<Doctor, DoctorDto>().ReverseMap();
        CreateMap<Appointment, AppointmentDto>().ReverseMap();
    }
}