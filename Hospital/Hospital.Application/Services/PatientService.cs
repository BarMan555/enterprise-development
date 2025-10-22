using AutoMapper;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Dtos;
using Hospital.Domain;
using Hospital.Domain.Models;

namespace Hospital.Application.Services;

public class PatientService(IRepository<Patient, int> repository, IMapper mapper) 
    : IApplicationService<PatientDto, int>
{
    public int Create(PatientDto entity)
    {
        return repository.Create(mapper.Map<Patient>(entity));
    }

    public List<PatientDto> GetAll()
    {
        return mapper.Map<List<PatientDto>>(repository.ReadAll());
    }

    public PatientDto? Get(int id)
    {
        return mapper.Map<PatientDto>(repository.Read(id));
    }

    public PatientDto? Update(int id, PatientDto entity)
    {
        return mapper.Map<PatientDto>(repository.Update(id, mapper.Map<Patient>(entity)));
    }

    public bool Delete(int id)
    {
        return repository.Delete(id);
    }
}