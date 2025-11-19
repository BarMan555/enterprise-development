using Hospital.Domain.Models;
using Hospital.Domain.Enums;

namespace Hospital.Domain.Seeders;
/// <summary>
/// Seeder for Database
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// List of Patients
    /// </summary>
    public static readonly List<Patient> Patients =
    [
        new Patient
        {
            Id = 1,
            FullName = "Иванов Иван Иванович",
            Gender = Gender.Male,
            DateOfBirth = new DateTime(1985, 3, 15),
            Address = "г. Москва, ул. Ленина, д. 25, кв. 12",
            BloodType = BloodType.A,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "+7 (495) 123-45-67"
        },
        new Patient
        {
            Id = 2,
            FullName = "Петрова Анна Сергеевна",
            Gender = Gender.Female,
            DateOfBirth = new DateTime(1992, 7, 22),
            Address = "г. Санкт-Петербург, Невский пр-т, д. 48, кв. 34",
            BloodType = BloodType.B,
            RhFactor = RhFactor.Negative,
            PhoneNumber = "+7 (812) 987-65-43"
        },
        new Patient
        {
            Id = 3,
            FullName = "Сидоров Михаил Петрович",
            Gender = Gender.Male,
            DateOfBirth = new DateTime(1978, 11, 5),
            Address = "г. Екатеринбург, ул. Мира, д. 17, кв. 8",
            BloodType = BloodType.Ab,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "+7 (343) 456-78-90"
        },
        new Patient
        {
            Id = 4,
            FullName = "Козлова Елена Викторовна",
            Gender = Gender.Female,
            DateOfBirth = new DateTime(2001, 1, 30),
            Address = "г. Новосибирск, ул. Кирова, д. 12, кв. 56",
            BloodType = BloodType.O,
            RhFactor = RhFactor.Negative,
            PhoneNumber = "+7 (383) 234-56-78"
        },
        new Patient
        {
            Id = 5,
            FullName = "Николаев Дмитрий Александрович",
            Gender = Gender.Male,
            DateOfBirth = new DateTime(1965, 9, 18),
            Address = "г. Казань, ул. Баумана, д. 33, кв. 21",
            BloodType = BloodType.A,
            RhFactor = RhFactor.Negative,
            PhoneNumber = "+7 (843) 765-43-21"
        }
    ];

    /// <summary>
    /// List of doctor's specialization
    /// </summary>
    private static readonly List<Specialization> _specializations =
    [
        new Specialization() { Id = 0, Name = SpecializationName.Pediatrician },
        new Specialization() { Id = 1, Name = SpecializationName.Surgeon },
        new Specialization() { Id = 2, Name = SpecializationName.Neurologist },
        new Specialization() { Id = 3, Name = SpecializationName.Cardiologist },
        new Specialization() { Id = 4, Name = SpecializationName.Dentist },
        new Specialization() { Id = 5, Name = SpecializationName.Ophthalmologist },
        new Specialization() { Id = 6, Name = SpecializationName.Therapist },
    ];
    
    /// <summary>
    /// List of Doctors
    /// </summary>
    public static readonly List<Doctor> Doctors =
    [
        new Doctor
        {
            Id = 1,
            FullName = "Смирнов Александр Васильевич",
            DateOfBirth = new DateTime(1978, 5, 12),
            Specialization = _specializations[0],
            ExperienceYears = 15
        },
        new Doctor
        {
            Id = 2,
            FullName = "Иванова Мария Петровна",
            DateOfBirth = new DateTime(1985, 8, 24),
            Specialization = _specializations[1],
            ExperienceYears = 8
        },
        new Doctor
        {
            Id = 3,
            FullName = "Кузнецов Дмитрий Сергеевич",
            DateOfBirth = new DateTime(1970, 3, 5),
            Specialization = _specializations[2],
            ExperienceYears = 25
        },
        new Doctor
        {
            Id = 4,
            FullName = "Петрова Ольга Игоревна",
            DateOfBirth = new DateTime(1990, 11, 17),
            Specialization = _specializations[3],
            ExperienceYears = 5
        },
        new Doctor
        {
            Id = 5,
            FullName = "Васильев Игорь Николаевич",
            DateOfBirth = new DateTime(1965, 2, 28),
            Specialization = _specializations[4],
            ExperienceYears = 30
        },
        new Doctor
        {
            Id = 6,
            FullName = "Николаева Екатерина Владимировна",
            DateOfBirth = new DateTime(1982, 7, 14),
            Specialization = _specializations[5],
            ExperienceYears = 12
        },
        new Doctor
        {
            Id = 7,
            FullName = "Алексеев Павел Михайлович",
            DateOfBirth = new DateTime(1975, 9, 3),
            Specialization = _specializations[6],
            ExperienceYears = 18
        }
    ];

    /// <summary>
    /// List of Appointments
    /// </summary>
    public static readonly List<Appointment> Appointments =
    [
        new Appointment
        {
            Id = 1,
            Patient = Patients[0], // Иванов Иван Иванович
            Doctor = Doctors[0], // Смирнов Александр Васильевич (Кардиолог)
            AppointmentDateTime = new DateTime(2025, 8, 15, 10, 0, 0),
            RoomNumber = 101,
            IsReturnVisit = false
        },
        new Appointment
        {
            Id = 2,
            Patient = Patients[1], // Петрова Анна Сергеевна
            Doctor = Doctors[1], // Иванова Мария Петровна (Педиатр)
            AppointmentDateTime = new DateTime(2025, 8, 15, 11, 30, 0),
            RoomNumber = 205,
            IsReturnVisit = true
        },
        new Appointment
        {
            Id = 3,
            Patient = Patients[2], // Сидоров Михаил Петрович
            Doctor = Doctors[2], // Кузнецов Дмитрий Сергеевич (Хирург)
            AppointmentDateTime = new DateTime(2025, 9, 16, 9, 0, 0),
            RoomNumber = 312,
            IsReturnVisit = false
        },
        new Appointment
        {
            Id = 4,
            Patient = Patients[3], // Козлова Елена Викторовна
            Doctor = Doctors[3], // Петрова Ольга Игоревна (Невролог)
            AppointmentDateTime = new DateTime(2025, 8, 16, 14, 15, 0),
            RoomNumber = 118,
            IsReturnVisit = false
        },
        new Appointment
        {
            Id = 5,
            Patient = Patients[4], // Николаев Дмитрий Александрович
            Doctor = Doctors[4], // Васильев Игорь Николаевич (Терапевт)
            AppointmentDateTime = new DateTime(2025, 8, 17, 16, 45, 0),
            RoomNumber = 201,
            IsReturnVisit = true
        },
        new Appointment
        {
            Id = 6,
            Patient = Patients[0], // Иванов Иван Иванович
            Doctor = Doctors[6], // Алексеев Павел Михайлович (Офтальмолог)
            AppointmentDateTime = new DateTime(2025, 8, 18, 13, 0, 0),
            RoomNumber = 404,
            IsReturnVisit = false
        },
        new Appointment
        {
            Id = 7,
            Patient = Patients[1], // Петрова Анна Сергеевна
            Doctor = Doctors[0], // Соколова Анна Дмитриевна (Гинеколог)
            AppointmentDateTime = new DateTime(2025, 8, 19, 10, 30, 0),
            RoomNumber = 222,
            IsReturnVisit = true
        },
        new Appointment
        {
            Id = 8,
            Patient = Patients[2], // Сидоров Михаил Петрович
            Doctor = Doctors[1], // Смирнов Александр Васильевич (Кардиолог)
            AppointmentDateTime = new DateTime(2025, 8, 19, 15, 20, 0),
            RoomNumber = 101,
            IsReturnVisit = false
        }
    ];
}