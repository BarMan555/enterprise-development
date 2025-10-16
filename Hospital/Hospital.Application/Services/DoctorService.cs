using Hospital.Application.Dto;
using Hospital.Domain.Models;
using Hospital.Domain.Repositories;

namespace Hospital.Application.Services;

public class DoctorService(IDoctorRepository repository)
{
    private static Doctor MapDto(DoctorDto entity)
    {
        return new Doctor
        {
            Id = -1,
            FullName = entity.FullName,
            DateOfBirth = entity.DateOfBirth,
            Specialization = entity.Specialization,
            ExperienceYears = entity.ExperienceYears
        };
    }
    
    public int CreateDoctor(DoctorDto entity)
    {
        return repository.Create(MapDto(entity));
    }

    public List<Doctor> ReadDoctors()
    {
        return repository.Read();
    }

    public Doctor? ReadDoctor(int id)
    {
        return repository.Read(id);
    }

    public Doctor? UpdateDoctor(int id, DoctorDto entity)
    {
        return repository.Update(id, MapDto(entity));
    }

    public bool DeleteDoctor(int id)
    {
        return repository.Delete(id);
    }
}