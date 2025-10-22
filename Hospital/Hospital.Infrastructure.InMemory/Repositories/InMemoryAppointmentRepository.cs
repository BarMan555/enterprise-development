using Hospital.Domain.Models;
using Hospital.Infrastructure.InMemory.Seeders;

namespace Hospital.Infrastructure.InMemory.Repositories;

/// <summary>
/// In memory repository for appointments
/// </summary>
/// <param name="seeder">Data for repository</param>
public class InMemoryAppointmentRepository(InMemoryAppointmentRepositorySeeder seeder) :  InMemoryRepository<Appointment, int>(seeder)
{
    /// <summary>
    /// Get ID of input entity
    /// </summary>
    /// <param name="entity">entity</param>
    /// <returns>ID of entity</returns>
    protected override int GetEntityId(Appointment entity) => entity.Id;

    /// <summary>
    /// Set ID to entity
    /// </summary>
    /// <param name="entity">entity</param>
    /// <param name="id">ID to entity</param>
    protected override void SetEntityId(Appointment entity, int id) => entity.Id = id;

    /// <summary>
    /// Generate new ID
    /// </summary>
    /// <returns>New ID</returns>
    protected override int GenerateId() => CurrentId++;
}