using AutoMapper;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Dtos;
using Hospital.Domain;
using Hospital.Domain.Models;

namespace Hospital.Application.Services;

public class AppointmentService(IRepository<Appointment, int> repository, IMapper mapper) : IApplicationService<AppointmentDto, int>
{
    public int Create(AppointmentDto entity)
    {
        return repository.Create(mapper.Map<Appointment>(entity));
    }

    public List<AppointmentDto> GetAll()
    {
        return mapper.Map<List<AppointmentDto>>(repository.ReadAll());
    }

    public AppointmentDto? Get(int id)
    {
        return mapper.Map<AppointmentDto>(repository.Read(id));
    }

    public AppointmentDto? Update(int id, AppointmentDto entity)
    {
        return mapper.Map<AppointmentDto>(repository.Update(id, mapper.Map<Appointment>(entity)));
    }

    public bool Delete(int id)
    {
        return repository.Delete(id);
    }
}