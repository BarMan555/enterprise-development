using Hospital.Domain.Seeders;

namespace Hospital.Infrastructure.EfCore;

/// <summary>
/// Class for initializer database with start data
/// </summary>
public static class DataInitializer
{
    public static async Task SeedEnsureCreated(AppDbContext context)
    {
        // true - DB is created now, false - DB was created later  
        var created =  await context.Database.EnsureCreatedAsync();
        if (created)
        {
            await context.Patients.AddRangeAsync(DataSeeder.Patients);
            await context.Doctors.AddRangeAsync(DataSeeder.Doctors);
            await context.Appointments.AddRangeAsync(DataSeeder.Appointments);
            await context.Specializations.AddRangeAsync(DataSeeder.Specializations);
            await context.SaveChangesAsync();
        }
    }
}