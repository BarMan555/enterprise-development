using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure.EfCore;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor);

        Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
        modelBuilder.Entity<Patient>().ToCollection("patients");
        modelBuilder.Entity<Doctor>().ToCollection("doctors");
        modelBuilder.Entity<Appointment>().ToCollection("appointments");
    }
}