using Hospital.Domain.Models;
using Hospital.Domain.Repositories;
using Hospital.Infrastructure.InMemory.Seeders;

namespace Hospital.Infrastructure.InMemory.Repositories;

public class InMemoryDoctorRepository : IDoctorRepository
{
    private readonly List<Doctor> _items = [];
    
    private int _currentId = 1;

    public InMemoryDoctorRepository(InMemoryDoctorRepositorySeeder? seeder)
    {
        if(seeder == null) return;

        _items = seeder.GetItems();
        _currentId = seeder.GetCurrentId();
    }

    private int GenerateId()
    {
        return _currentId++;
    }

    public int Create(Doctor entity)
    {
        entity.Id = GenerateId();
        _items.Add(entity);
        return entity.Id;
    }

    public List<Doctor> Read()
    {
        return _items;
    }

    public Doctor? Read(int id)
    {
        return _items.FirstOrDefault(x => x.Id == id);
    }

    public Doctor? Update(int id, Doctor entity)
    {
        var existingDoctor = Read(id);
        if (existingDoctor == null) return null;
        
        existingDoctor.FullName = entity.FullName;
        existingDoctor.DateOfBirth = entity.DateOfBirth;
        existingDoctor.Specialization = entity.Specialization;
        existingDoctor.ExperienceYears = entity.ExperienceYears;
        
        return existingDoctor;
    }

    public bool Delete(int id)
    {
        var existingDoctor = Read(id);
        if (existingDoctor == null) return false;
        
        _items.Remove(existingDoctor);
        return true;
    }
}