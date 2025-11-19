using Hospital.Domain;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Hospital.Infrastructure.EfCore.Repositories;
public class DoctorEfCoreRepository(AppDbContext context): IRepositoryAsync<Doctor, int>
{
    private readonly DbSet<Doctor> _doctors = context.Doctors;
    
    public async Task<Doctor> Create(Doctor entity)
    {
        var result = await _doctors.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<List<Doctor>> ReadAll()
    {
        return await _doctors.ToListAsync();
    }

    public async Task<Doctor?> Read(int id)
    {
        return await _doctors.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Doctor?> Update(int id, Doctor entity)
    {
        _doctors.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id));
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _doctors.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
            return false;
        _doctors.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}