using Hospital.Domain.Models;
using Hospital.Infrastructure.InMemory.Seeders;

namespace Hospital.Infrastructure.InMemory.Repositories;

public class InMemoryDoctorRepository(InMemoryDoctorRepositorySeeder seeder) : InMemoryRepository<Doctor, int>(seeder)
{
    protected override int GetEntityId(Doctor entity) => entity.Id;

    protected override void SetEntityId(Doctor entity, int id) => entity.Id = id;

    protected override int GenerateId() => CurrentId++;
}