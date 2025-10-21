using Hospital.Domain.Models;
using Hospital.Infrastructure.InMemory.Seeders;

namespace Hospital.Infrastructure.InMemory.Repositories;

public class InMemoryAppointmentRepository(InMemoryAppointmentRepositorySeeder seeder) :  InMemoryRepository<Appointment, int>(seeder)
{
    protected override int GetEntityId(Appointment entity) => entity.Id;

    protected override void SetEntityId(Appointment entity, int id) => entity.Id = id;

    protected override int GenerateId() => CurrentId++;
}