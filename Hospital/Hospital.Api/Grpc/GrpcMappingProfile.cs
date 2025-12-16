using AutoMapper;
using Hospital.Application.Contracts.Dtos;
using Hospital.Grpc.Contracts; 

namespace Hospital.Api.Grpc;

/// <summary>
/// AutoMapper configuration for mapping between Grpc responses and DTO models
/// </summary>
public class GrpcMappingProfile : Profile
{
    /// <summary>
    /// Profile for mappings between Grpc responses and DTO models
    /// </summary>
    public GrpcMappingProfile()
    {
        CreateMap<PatientResponse, PatientCreateUpdateDto>()
            .ForMember(dest => dest.DateOfBirth, 
                opt => opt.MapFrom(src => DateTime.Parse(src.DateOfBirth)));
    }
}