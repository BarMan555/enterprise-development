using Hospital.Domain;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace Hospital.Infrastructure.EfCore.Repositories;
/// <summary>
/// Repository for appointments 
/// </summary>
/// <param name="context">DataBase MongoDB context</param>
public class PatientEfCoreRepository(AppDbContext context) : IRepositoryAsync<Patient, ObjectId>
{
    /// <summary>
    /// Data of patients
    /// </summary>
    private readonly DbSet<Patient> _patients = context.Patients;
    
    /// <inheritdoc cref="IRepositoryAsync{TEntity,TKey}"/>
    public async Task<Patient> Create(Patient entity)
    {
        var result = await _patients.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc cref="IRepositoryAsync{TEntity,TKey}"/>
    public async Task<List<Patient>> ReadAll()
    {
        return await _patients.ToListAsync();
    }

    /// <inheritdoc cref="IRepositoryAsync{TEntity,TKey}"/>
    public async Task<Patient?> Read(ObjectId id)
    {
        return await _patients.FirstOrDefaultAsync(e => e.Id == id);
    }

    /// <inheritdoc cref="IRepositoryAsync{TEntity,TKey}"/>
    public async Task<Patient?> Update(ObjectId id, Patient entity)
    {
        _patients.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id));
    }

    /// <inheritdoc cref="IRepositoryAsync{TEntity,TKey}"/>
    public async Task<bool> Delete(ObjectId id)
    {
        var entity = await _patients.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
            return false;
        _patients.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}