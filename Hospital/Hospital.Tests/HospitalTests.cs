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
    public void GetDoctorsWithExperienceAtLeast10Years()
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
    public void PatientsByDoctorOrderedByName()
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
    public void RepeatAppointmentsCountLastMonth()
    {
        var lastMonthStart = new DateTime(2024, 1, 1);
        var lastMonthEnd = new DateTime(2024, 1, 31);
        var expectedCount = 3;

        var result = hospital.Appointments
            .Count(a => a.IsRepeat &&
                       a.AppointmentDateTime >= lastMonthStart &&
                       a.AppointmentDateTime <= lastMonthEnd);

        Assert.Equal(expectedCount, result);
    }

    /// <summary>
    /// Test to verify retrieval of patients over 30 years old who have
    /// appointments with multiple doctors, ordered by birth date.
    /// </summary>
    [Fact]
    public void PatientsOver30WithMultipleDoctorsOrderedByBirthDate()
    {
        var currentDate = new DateTime(2024, 1, 1);
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
    public void AppointmentsInRoomCurrentMonth()
    {
        var roomNumber = 101;
        var currentMonthStart = new DateTime(2024, 1, 1);
        var currentMonthEnd = new DateTime(2024, 1, 31);

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