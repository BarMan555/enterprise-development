using Hospital.Domain.Models;
using Hospital.Domain.Seeders;
using MongoDB.Bson;

namespace Hospital.Infrastructure.InMemory.Repositories;

/// <summary>
/// In memory repository for patients
/// </summary>
/// <param name="seeder">Data for repository</param>
public class InMemoryPatientRepository(PatientRepositorySeeder seeder) : InMemoryRepository<Patient, ObjectId>(seeder)
{
    /// <summary>
    /// Get ID of input entity
    /// </summary>
    /// <param name="entity">entity</param>
    /// <returns>ID of entity</returns>
    protected override ObjectId GetEntityId(Patient entity) => entity.Id;

    /// <summary>
    /// Set ID to entity
    /// </summary>
    /// <param name="entity">entity</param>
    /// <param name="id">ID to entity</param>
    protected override void SetEntityId(Patient entity, ObjectId id) => entity.Id = id;

    /// <summary>
    /// Generate new ID
    /// </summary>
    /// <returns>New ID</returns>
    protected override ObjectId GenerateId() => ObjectId.GenerateNewId();
}