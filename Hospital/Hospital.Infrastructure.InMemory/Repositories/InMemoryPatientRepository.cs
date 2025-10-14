using Hospital.Domain;
using Hospital.Domain.Models;
using Hospital.Domain.Repositories;
using Hospital.Infrastructure.InMemory.Seeders;

namespace Hospital.Infrastructure.InMemory.Repositories;

public class InMemoryPatientRepository : IPatientRepository
{
    private readonly List<Patient> _items = [];
    
    private int _currentId = 1;

    private int GenerateId()
    {
        return _currentId + 1;
    }

    public int Create(Patient entity)
    {
        entity.Id = GenerateId();
        _items.Add(entity);
        return entity.Id;
    }

    public List<Patient> Read()
    {
        return _items;
    }

    public Patient? Read(int id)
    {
        return _items.FirstOrDefault(x => x.Id == id);
    }

    public Patient? Update(int id, Patient entity)
    {
        var existingPatient = Read(id);
        if (existingPatient == null) return null;
        
        existingPatient.FullName = entity.FullName;
        existingPatient.Gender = entity.Gender;
        existingPatient.DateOfBirth = entity.DateOfBirth;
        existingPatient.Address = entity.Address;
        existingPatient.BloodType = entity.BloodType;
        existingPatient.RhFactor =  entity.RhFactor;
        existingPatient.PhoneNumber = entity.PhoneNumber;
        
        return existingPatient;
    }

    public bool Delete(int id)
    {
        var existingPatient = Read(id);
        if (existingPatient == null) return false;
        
        _items.Remove(existingPatient);
        return true;
    }
}