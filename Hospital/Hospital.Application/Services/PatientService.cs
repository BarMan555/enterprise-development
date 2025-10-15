using Hospital.Application.Dto;
using Hospital.Domain.Models;
using Hospital.Domain.Enums;
using Hospital.Domain.Repositories;

namespace Hospital.Application.Services;

public class PatientService(IPatientRepository repository)
{
    private static Patient MapDto(PatientDto entity)
    {
        return new Patient
        {
            Id = -1,
            FullName = entity.FullName,
            Gender = entity.Gender,
            DateOfBirth = entity.DateOfBirth,
            Address = entity.Address,
            BloodType = entity.BloodType,
            RhFactor = entity.RhFactor,
            PhoneNumber = entity.PhoneNumber,
        };
    }

    public int CreatePatient(PatientDto entity)
    {
        return repository.Create(MapDto(entity));
    }

    public List<Patient> ReadPatients()
    {
        return repository.Read();
    }

    public Patient? ReadPatient(int id)
    {
        return repository.Read(id);
    }

    public Patient? UpdatePatient(int id, PatientDto entity)
    {
        return repository.Update(id, MapDto(entity));
    }

    public bool DeletePatient(int id)
    {
        return repository.Delete(id);
    }
}