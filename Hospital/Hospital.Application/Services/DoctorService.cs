using AutoMapper;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Dtos;
using Hospital.Domain;
using Hospital.Domain.Models;

namespace Hospital.Application.Services;

public class DoctorService(IRepository<Doctor, int> repository, IMapper mapper) : IApplicationService<DoctorDto, int>
{
    public int Create(DoctorDto entity)
    {
        return repository.Create(mapper.Map<Doctor>(entity));
    }

    public List<DoctorDto> GetAll()
    {
        return mapper.Map<List<DoctorDto>>(repository.ReadAll());
    }

    public DoctorDto? Get(int id)
    {
        return mapper.Map<DoctorDto>(repository.Read(id));
    }

    public DoctorDto? Update(int id, DoctorDto entity)
    {
        return mapper.Map<DoctorDto>(repository.Update(id, mapper.Map<Doctor>(entity)));
    }

    public bool Delete(int id)
    {
        return repository.Delete(id);
    }
}