using Hospital.Domain;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace Hospital.Infrastructure.EfCore.Repositories;
/// <summary>
/// Repository for appointments 
/// </summary>
/// <param name="context">DataBase MongoDB context</param>
public class DoctorEfCoreRepository(AppDbContext context): IRepositoryAsync<Doctor, ObjectId>
{
    /// <summary>
    /// Data of doctors
    /// </summary>
    private readonly DbSet<Doctor> _doctors = context.Doctors;
    
    /// <inheritdoc cref="IRepositoryAsync{TEntity,TKey}"/>
    public async Task<Doctor> Create(Doctor entity)
    {
        var result = await _doctors.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc cref="IRepositoryAsync{TEntity,TKey}"/>
    public async Task<List<Doctor>> ReadAll()
    {
        return await _doctors.ToListAsync();
    }

    /// <inheritdoc cref="IRepositoryAsync{TEntity,TKey}"/>
    public async Task<Doctor?> Read(ObjectId id)
    {
        return await _doctors.FirstOrDefaultAsync(e => e.Id == id);
    }

    /// <inheritdoc cref="IRepositoryAsync{TEntity,TKey}"/>
    public async Task<Doctor?> Update(ObjectId id, Doctor entity)
    {
        _doctors.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id));
    }

    /// <inheritdoc cref="IRepositoryAsync{TEntity,TKey}"/>
    public async Task<bool> Delete(ObjectId id)
    {
        var entity = await _doctors.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
            return false;
        _doctors.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}