using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Hospital.Infrastructure.EfCore;

/// <summary>
/// Class for modeling database structure 
/// </summary>
/// <param name="options"></param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

        // Patient
        modelBuilder.Entity<Patient>(builder =>
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasElementName("_id").IsRequired();
            builder.Property(p => p.FullName).HasElementName("full_name").IsRequired();
            builder.Property(p => p.DateOfBirth).HasElementName("date_of_birth").IsRequired();
            builder.Property(p => p.Gender).HasElementName("gender").IsRequired();
            builder.Property(p => p.Address).HasElementName("address").IsRequired();
            builder.Property(p => p.BloodType).HasElementName("blood_type").IsRequired();
            builder.Property(p => p.RhFactor).HasElementName("rh_factor").IsRequired();
            builder.Property(p => p.PhoneNumber).HasElementName("phone_number").IsRequired();
            builder.ToCollection("patients");
        });

        // Doctor
        modelBuilder.Entity<Doctor>(builder =>
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasElementName("_id").IsRequired();
            builder.Property(d => d.FullName).HasElementName("full_name").IsRequired();
            builder.Property(d => d.DateOfBirth).HasElementName("date_of_birth").IsRequired();
            builder.Property(d => d.ExperienceYears).HasElementName("experience_years").IsRequired();
            builder.OwnsOne(d => d.Specialization);
            builder.ToCollection("doctors");
        });

        // Appointment
        modelBuilder.Entity<Appointment>(builder =>
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasElementName("_id").IsRequired();
            builder.Property(a => a.PatientId).HasElementName("patient_id").IsRequired();
            builder.Property(a => a.DoctorId).HasElementName("doctor_id").IsRequired();
            builder.Property(a => a.RoomNumber).HasElementName("room_number").IsRequired();
            builder.Property(a => a.IsReturnVisit).HasElementName("is_return_visit").IsRequired();
            builder.Property(a => a.AppointmentDateTime).HasElementName("appointment_date_time").IsRequired();
            builder.ToCollection("appointments");
        });
    }
}
