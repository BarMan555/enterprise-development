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
        return result.Entity;
    }

    /// <inheritdoc cref="IRepository{TEntity,TKey}"/>
    public async Task<List<Appointment>?> ReadAll()
    {
        var appointments = await context.Appointments.ToListAsync();

        var patients = await context.Patients.ToListAsync();
        var doctors = await context.Doctors.ToListAsync();

        foreach (var a in appointments)
        {
            a.Patient = patients.FirstOrDefault(p => p.Id == a.PatientId);
            a.Doctor = doctors.FirstOrDefault(d => d.Id == a.DoctorId);
        }

        return appointments;
    }

    /// <inheritdoc cref="IRepository{TEntity,TKey}"/>
    public async Task<Appointment?> Read(ObjectId id)
    {
        var appointment = await context.Appointments.FirstOrDefaultAsync(e => e.Id == id);
        appointment.Patient = context.Patients.FirstOrDefault(p => p.Id == appointment.PatientId);
        appointment.Doctor = context.Doctors.FirstOrDefault(d => d.Id == appointment.DoctorId);

        return appointment;
    }

    /// <inheritdoc cref="IRepository{TEntity,TKey}"/>
    public async Task<Appointment?> Update(ObjectId id, Appointment entity)
    {
        context.Appointments.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id));
    }

    /// <inheritdoc cref="IRepository{TEntity,TKey}"/>
    public async Task<bool> Delete(ObjectId id)
    {
        var entity = await context.Appointments.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
            return false;
        context.Appointments.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}