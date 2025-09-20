using Hospital.Domain.Fixtures;

namespace Hospital.Tests;

/// <summary>
/// Unit Tests of Domain classes
/// </summary>
/// <param name="hospital"></param>
public class HospitalTests(HospitalFixture hospital) : IClassFixture<HospitalFixture>
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

        var result = hospital.Doctors
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
        var doctor = hospital.Doctors[0]; // Смирнов Александр Васильевич
        var expectedPatients = new List<string>
        {
            "Петрова Анна Сергеевна",
            "Иванов Иван Иванович"
        };

        var result = hospital.Appointments
            .Where(a => a.Doctor.Id == doctor.Id)
            .Select(a => a.Patient.FullName)
            .OrderBy(name => name)
            .ToList();

        Assert.Equal(expectedPatients.OrderBy(name => name), result.OrderBy(name => name));
    }

    /// <summary>
    /// Test to verify counting of repeat patient appointments for the last month.
    /// </summary>
    [Fact]
    public void CountAppointments_WhenRepeatVisitsInLastMonth_ReturnsCorrectCount()
    {
        var currentDate = DateTime.Now;
        var lastMonthStart = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(-1);
        var lastMonthEnd = lastMonthStart.AddMonths(1).AddDays(-1);
        var expectedCount = 3;

        var result = hospital.Appointments
            .Count(a => a.IsReturnVisit &&
                       a.AppointmentDateTime >= lastMonthStart &&
                       a.AppointmentDateTime <= lastMonthEnd);

        Assert.Equal(expectedCount, result);
    }

    /// <summary>
    /// Test to verify retrieval of patients over 30 years old who have
    /// appointments with multiple doctors, ordered by birth date.
    /// </summary>
    [Fact]
    public void GetPatients_WhenOver30WithMultipleDoctors_ReturnsPatientsOrderedByBirthDate()
    {
        var currentDate = DateTime.Now;
        var age30 = currentDate.AddYears(-30);

        var expectedPatients = new List<string>
        {
            "Сидоров Михаил Петрович",
            "Иванов Иван Иванович",
            "Петрова Анна Сергеевна"
        };

        var result = hospital.Patients
            .Where(p => p.DateOfBirth <= age30)
            .Where(p => hospital.Appointments.Count(a => a.Patient.Id == p.Id) > 1)
            .OrderBy(p => p.DateOfBirth)
            .Select(p => p.FullName)
            .ToList();

        Assert.Equal(expectedPatients, result);
    }

    /// <summary>
    /// Test to verify retrieval of appointments for the current month
    /// happening in a specific room. 
    /// </summary>
    [Fact]
    public void GetAppointments_WhenInSpecificRoomCurrentMonth_ReturnsAppointmentsOrderedByDateTime()
    {
        var roomNumber = 101;
        var currentDate = DateTime.Now;
        var currentMonthStart = new DateTime(currentDate.Year, currentDate.Month, 1).AddDays(-1*currentDate.Day);
        var currentMonthEnd = currentMonthStart.AddMonths(1).AddDays(-1);

        var expectedAppointmentIds = new List<int> { 1, 8 };

        var result = hospital.Appointments
            .Where(a => a.RoomNumber == roomNumber &&
                       a.AppointmentDateTime >= currentMonthStart &&
                       a.AppointmentDateTime <= currentMonthEnd)
            .OrderBy(a => a.AppointmentDateTime)
            .Select(a => a.Id)
            .ToList();

        Assert.Equal(expectedAppointmentIds, result);
    }
}