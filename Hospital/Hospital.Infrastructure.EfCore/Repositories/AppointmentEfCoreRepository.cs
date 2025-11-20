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
    /// <summary>
    /// Data of appointments 
    /// </summary>
    private readonly DbSet<Appointment> _appointments = context.Appointments;
    /// <summary>
    /// Data of doctors
    /// </summary>
    private readonly DbSet<Doctor> _doctors = context.Doctors;
    /// <summary>
    /// Data of patients
    /// </summary>
    private readonly DbSet<Patient> _patients = context.Patients;
    
    /// <inheritdoc cref="IRepository{TEntity,TKey}"/>
    public async Task<Appointment> Create(Appointment entity)
    {
        var result = await _appointments.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc cref="IRepository{TEntity,TKey}"/>
    public async Task<List<Appointment>> ReadAll()
    {
        var appointments = await _appointments.ToListAsync();

        var patients = await _patients.ToListAsync();
        var doctors = await _doctors.ToListAsync();

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
        var appointment = await _appointments.FirstOrDefaultAsync(e => e.Id == id);
        appointment.Patient = _patients.FirstOrDefault(p => p.Id == appointment.PatientId);
        appointment.Doctor = _doctors.FirstOrDefault(d => d.Id == appointment.DoctorId);

        return appointment;
    }

    /// <inheritdoc cref="IRepository{TEntity,TKey}"/>
    public async Task<Appointment?> Update(ObjectId id, Appointment entity)
    {
        _appointments.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id));
    }

    /// <inheritdoc cref="IRepository{TEntity,TKey}"/>
    public async Task<bool> Delete(ObjectId id)
    {
        var entity = await _appointments.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
            return false;
        _appointments.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}