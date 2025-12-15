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
        
        if (entity.Doctor != null)
        {
            await context.Entry(entity.Doctor).Reference(d => d.Specialization).LoadAsync();
        }
        
        return result.Entity;
    }

    /// <inheritdoc cref="IRepository{TEntity,TKey}"/>
    public async Task<List<Appointment>?> ReadAll()
    {
        var appointments = await context.Appointments.ToListAsync();

        if (appointments.Count == 0)
            return appointments;

        var patientIds = appointments.Select(a => a.PatientId).Distinct().ToList();
        var doctorIds = appointments.Select(a => a.DoctorId).Distinct().ToList();

        var patients = await context.Patients
            .Where(p => patientIds.Contains(p.Id))
            .ToListAsync();

        var doctors = await context.Doctors
            .Where(d => doctorIds.Contains(d.Id))
            .ToListAsync();

        var specializationIds = doctors.Select(d => d.SpecializationId).Distinct().ToList();
        var specializations = await context.Specializations
            .Where(s => specializationIds.Contains(s.Id))
            .ToListAsync();

        foreach (var doctor in doctors)
        {
            doctor.Specialization = specializations.FirstOrDefault(s => s.Id == doctor.SpecializationId);
        }

        foreach (var appointment in appointments)
        {
            appointment.Patient = patients.FirstOrDefault(p => p.Id == appointment.PatientId);
            appointment.Doctor = doctors.FirstOrDefault(d => d.Id == appointment.DoctorId);
        }

        return appointments;
    }

    /// <inheritdoc cref="IRepository{TEntity,TKey}"/>
    public async Task<Appointment?> Read(ObjectId id)
    {
        var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

        if (appointment == null) 
            return null;

        await context.Entry(appointment).Reference(a => a.Patient).LoadAsync();

        await context.Entry(appointment).Reference(a => a.Doctor).LoadAsync();

        if (appointment.Doctor != null)
        {
            await context.Entry(appointment.Doctor).Reference(d => d.Specialization).LoadAsync();
        }
        
        return appointment;
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