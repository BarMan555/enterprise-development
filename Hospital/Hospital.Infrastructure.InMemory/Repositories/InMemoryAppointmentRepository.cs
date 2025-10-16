using Hospital.Domain.Models;
using Hospital.Domain.Repositories;
using Hospital.Infrastructure.InMemory.Seeders;

namespace Hospital.Infrastructure.InMemory.Repositories;

public class InMemoryAppointmentRepository :  IAppointmentRepository
{
    private readonly List<Appointment> _items = [];
    
    private int _currentId = 1;

    public InMemoryAppointmentRepository(InMemoryAppointmentRepositorySeeder? seeder)
    {
        if(seeder == null) return;

        _items = seeder.GetItems();
        _currentId = seeder.GetCurrentId();
    }

    private int GenerateId()
    {
        return _currentId++;
    }

    public int Create(Appointment entity)
    {
        entity.Id = GenerateId();
        _items.Add(entity);
        return entity.Id;
    }

    public List<Appointment> Read()
    {
        return _items;
    }

    public Appointment? Read(int id)
    {
        return _items.FirstOrDefault(x => x.Id == id);
    }

    public Appointment? Update(int id, Appointment entity)
    {
        var existingDoctor = Read(id);
        if (existingDoctor == null) return null;
        
        existingDoctor.Patient = entity.Patient;
        existingDoctor.Doctor = entity.Doctor;
        existingDoctor.AppointmentDateTime = entity.AppointmentDateTime;
        existingDoctor.RoomNumber = entity.RoomNumber;
        existingDoctor.IsReturnVisit = entity.IsReturnVisit;
        
        return existingDoctor;
    }

    public bool Delete(int id)
    {
        var existingAppointment = Read(id);
        if (existingAppointment == null) return false;
        
        _items.Remove(existingAppointment);
        return true;
    }
}