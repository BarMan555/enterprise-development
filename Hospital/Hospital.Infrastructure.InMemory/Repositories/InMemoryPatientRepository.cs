using Hospital.Domain.Models;
using Hospital.Infrastructure.InMemory.Seeders;

namespace Hospital.Infrastructure.InMemory.Repositories;

public class InMemoryPatientRepository(InMemoryPatientRepositorySeeder seeder) : InMemoryRepository<Patient, int>(seeder)
{
    protected override int GetEntityId(Patient entity) => entity.Id;

    protected override void SetEntityId(Patient entity, int id) => entity.Id = id;

    protected override int GenerateId() => CurrentId++;
}