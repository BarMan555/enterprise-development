using Hospital.Domain;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Hospital.Infrastructure.EfCore.Repositories;
public class PatientEfCoreRepository(AppDbContext context) : IRepositoryAsync<Patient, int>
{
    private readonly DbSet<Patient> _patients = context.Patients;
    
    public async Task<Patient> Create(Patient entity)
    {
        var result = await _patients.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<List<Patient>> ReadAll()
    {
        return await _patients.ToListAsync();
    }

    public async Task<Patient?> Read(int id)
    {
        return await _patients.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Patient?> Update(int id, Patient entity)
    {
        _patients.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id));
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _patients.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
            return false;
        _patients.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}