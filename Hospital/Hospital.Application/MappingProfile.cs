using AutoMapper;
using Hospital.Application.Contracts.Dtos;
using Hospital.Domain.Models;

namespace Hospital.Application;

public class MappingProfile : Profile
{
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