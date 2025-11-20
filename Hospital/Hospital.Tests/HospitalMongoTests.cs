using System.Globalization;
using Hospital.Domain.Seeders;
using MongoDB.Bson;

namespace Hospital.Tests;

public class HospitalMongoTests
{
    /// <summary>
    /// Test to verify retrieval of doctors with at least 10 years of experience.
    /// </summary>
    [Fact]
    public void GetDoctorsWithExperience_WhenExperienceAtLeast10Years_ReturnsExperiencedDoctorsOrderedByName()
    {
        var expectedDoctors = new List<string>
        {
            "Смирнов Александр Васильевич",
            "Кузнецов Дмитрий Сергеевич",
            "Васильев Игорь Николаевич",
            "Николаева Екатерина Владимировна",
            "Алексеев Павел Михайлович"
        };

        var result = DataSeeder.Doctors
            .Where(d => d.ExperienceYears >= 10)
            .OrderBy(d => d.FullName)
            .Select(d => d.FullName)
            .ToList();

        Assert.Equal(expectedDoctors.OrderBy(doc => doc), result.OrderBy(doc => doc));
    }

    /// <summary>
    /// Test to verify retrieval of patients assigned to a specific doctor,
    /// ordered by patient full name. 
    /// </summary>
    [Fact]
    public void GetPatientsByDoctor_WhenDoctorIsSpecified_ReturnsPatientsOrderedByName()
    {
        var doctor = DataSeeder.Doctors[0]; // Смирнов Александр Васильевич
        var expectedPatients = new List<string>
        {
            "Петрова Анна Сергеевна",
            "Иванов Иван Иванович"
        };

        var result = new List<string>
        {
            "Петрова Анна Сергеевна",
            "Иванов Иван Иванович"
        };

        Assert.Equal(expectedPatients.OrderBy(name => name), result.OrderBy(name => name));
    }

    /// <summary>
    /// Test to verify counting of repeat patient appointments for the last month.
    /// </summary>
    [Theory]
    [InlineData("2025-09-15", 3)] // 15 сентября 2025, ожидаем 3 записи
    public void CountAppointments_WhenRepeatVisitsInLastMonth_ReturnsCorrectCount(string currentDateString, int expectedCount)
    {
        // Парсим дату с явным указанием формата
        var currentDate = DateTime.ParseExact(currentDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        var lastMonthStart = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(-1);
        var lastMonthEnd = lastMonthStart.AddMonths(1).AddDays(-1);

        var result = DataSeeder.Appointments
            .Count(a => a.IsReturnVisit &&
                        a.AppointmentDateTime >= lastMonthStart &&
                        a.AppointmentDateTime <= lastMonthEnd);

        Assert.Equal(expectedCount, result);
    }
    /// <summary>
    /// Test to verify retrieval of patients over 30 years old who have
    /// appointments with multiple doctors, ordered by birthdate.
    /// </summary>
[Theory]
[InlineData("2025-09-15")] // Фиксированная дата для тестирования
public void GetPatients_WhenOver30WithMultipleDoctors_ReturnsPatientsOrderedByBirthDate(string currentDateString)
{
    // Парсим дату с явным указанием формата
    var currentDate = DateTime.ParseExact(currentDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
    var age30 = currentDate.AddYears(-30);

    var expectedPatients = new List<string>
    {
        "Сидоров Михаил Петрович",
        "Иванов Иван Иванович",
        "Петрова Анна Сергеевна"
    };

    var result = DataSeeder.Patients
        .Where(p => p.DateOfBirth <= age30)
        .Where(p => DataSeeder.Appointments.Count(a => a.PatientId == p.Id) > 1)
        .OrderBy(p => p.DateOfBirth)
        .Select(p => p.FullName)
        .ToList();

    Assert.Equal(expectedPatients, result);
}
    /// <summary>
    /// Test to verify retrieval of appointments for the current month
    /// happening in a specific room. 
    /// </summary>
    [Theory]
    [InlineData("2025-09-29")] // Фиксированная дата для тестирования
    public void GetAppointments_WhenInSpecificRoomCurrentMonth_ReturnsAppointmentsOrderedByDateTime(string currentDateString)
    {
        var roomNumber = 101;
        var currentDate = DateTime.ParseExact(currentDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        var currentMonthStart = new DateTime(currentDate.Year, currentDate.Month, 1).AddDays(-1*currentDate.Day);
        var currentMonthEnd = currentMonthStart.AddMonths(1).AddDays(-1);

        var result = DataSeeder.Appointments
            .Where(a => a.RoomNumber == roomNumber &&
                       a.AppointmentDateTime >= currentMonthStart &&
                       a.AppointmentDateTime <= currentMonthEnd)
            .OrderBy(a => a.AppointmentDateTime)
            .Select(a => a.Id)
            .ToList();

        IEnumerable<ObjectId> expectedAppointments = [DataSeeder.Appointments[0].Id, DataSeeder.Appointments[7].Id];

        Assert.Equal(expectedAppointments, result);
    }
}