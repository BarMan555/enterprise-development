using Hospital.Domain.Seeders;

namespace Hospital.Infrastructure.EfCore;

public static class DataInitializer
{
    public static async Task SeedEnsureCreated(AppDbContext context)
    {
        var created =  await context.Database.EnsureCreatedAsync();
        if (created)
        {
            await context.Patients.AddRangeAsync(DataSeeder.Patients);
            await context.Doctors.AddRangeAsync(DataSeeder.Doctors);
            await context.Appointments.AddRangeAsync(DataSeeder.Appointments);
            await context.SaveChangesAsync();
        }
    }
}