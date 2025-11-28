using Hospital.Domain;
using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace Hospital.Infrastructure.EfCore.Repositories;
/// <summary>
/// Repository for appointments 
/// </summary>
/// <param name="context">DataBase MongoDB context</param>
public class AppointmentEfCoreRepository(AppDbContext context): IRepository<Appointment, ObjectId>
{
    /// <inheritdoc cref="IRepository{TEntity,TKey}"/>
    public async Task<Appointment> Create(Appointment entity)
    {
        var result = await context.Appointments.AddAsync(entity);
        await context.SaveChangesAsync();
        
        await context.Entry(entity).Reference(a => a.Patient).LoadAsync();
        await context.Entry(entity).Reference(a => a.Doctor).LoadAsync();
        
        return result.Entity;
    }

    /// <inheritdoc cref="IRepository{TEntity,TKey}"/>
    public async Task<List<Appointment>?> ReadAll()
    {
        return await context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .ThenInclude(d => d.Specialization)
            .ToListAsync();
    }

    /// <inheritdoc cref="IRepository{TEntity,TKey}"/>
    public async Task<Appointment?> Read(ObjectId id)
    {
        return await context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .ThenInclude(d => d.Specialization)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    /// <inheritdoc cref="IRepository{TEntity,TKey}"/>
    public async Task<Appointment?> Update(ObjectId id, Appointment entity)
    {
        var existingAppointment = await context.Appointments.FindAsync(id);
        if (existingAppointment == null)
        {
            throw new KeyNotFoundException($"Appointment with id {id} not found");
        }

        existingAppointment.AppointmentDateTime = entity.AppointmentDateTime;
        existingAppointment.RoomNumber = entity.RoomNumber;
        existingAppointment.IsReturnVisit = entity.IsReturnVisit;
        existingAppointment.PatientId = entity.PatientId;
        existingAppointment.DoctorId = entity.DoctorId;

        context.Appointments.Update(existingAppointment);
        await context.SaveChangesAsync();

        return await Read(id) ?? existingAppointment;
    }

    /// <inheritdoc cref="IRepository{TEntity,TKey}"/>
    public async Task<bool> Delete(ObjectId id)
    {
        var appointment = await context.Appointments.FindAsync(id);
        if (appointment == null)
            return false;
        context.Appointments.Remove(appointment);
        await context.SaveChangesAsync();
        return true;
    }
}