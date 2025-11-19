using Hospital.Domain;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Hospital.Infrastructure.EfCore.Repositories;
public class AppointmentEfCoreRepository(AppDbContext context): IRepositoryAsync<Appointment, int>
{
    private readonly DbSet<Appointment> _appointments = context.Appointments;
    
    public async Task<Appointment> Create(Appointment entity)
    {
        var result = await _appointments.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<List<Appointment>> ReadAll()
    {
        return await _appointments.ToListAsync();
    }

    public async Task<Appointment?> Read(int id)
    {
        return await _appointments.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Appointment?> Update(int id, Appointment entity)
    {
        _appointments.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id));
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _appointments.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
            return false;
        _appointments.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}