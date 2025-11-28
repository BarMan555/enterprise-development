using Hospital.Domain.Enums;
using Hospital.Domain.Models;
using MongoDB.Bson;

namespace Hospital.Domain.Seeders;
/// <summary>
/// Seeder for Database
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// IDs of specializations
    /// </summary>
    private static readonly ObjectId _cardiologyId = ObjectId.GenerateNewId();
    private static readonly ObjectId _neurologyId = ObjectId.GenerateNewId();
    private static readonly ObjectId _orthopedicsId = ObjectId.GenerateNewId();
    private static readonly ObjectId _pediatricsId = ObjectId.GenerateNewId();
    private static readonly ObjectId _dermatologyId = ObjectId.GenerateNewId();

    /// <summary>
    /// List of Specialization
    /// </summary>
    public static readonly List<Specialization> Specializations =
    [

        new() { Id = _cardiologyId, Name = "Кардиолог", IsActive = true },
        new() { Id = _neurologyId, Name = "Нейролог", IsActive = true },
        new() { Id = _orthopedicsId, Name = "Ортопед", IsActive = true },
        new() { Id = _pediatricsId, Name = "Педиатр", IsActive = true },
        new() { Id = _dermatologyId, Name = "Дерматолог", IsActive = true }
    ];
    
    /// <summary>
    /// List of Patients
    /// </summary>
    public static readonly List<Patient> Patients =
    [
        new Patient
        {
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
    /// List of Doctors
    /// </summary>
    public static readonly List<Doctor> Doctors =
    [
        new Doctor
        {
            FullName = "Смирнов Александр Васильевич",
            DateOfBirth = new DateTime(1978, 5, 12),
            SpecializationId = _cardiologyId,
            ExperienceYears = 15
        },
        new Doctor
        {
            FullName = "Иванова Мария Петровна",
            DateOfBirth = new DateTime(1985, 8, 24),
            SpecializationId = _dermatologyId,
            ExperienceYears = 8
        },
        new Doctor
        {
            FullName = "Кузнецов Дмитрий Сергеевич",
            DateOfBirth = new DateTime(1970, 3, 5),
            SpecializationId = _neurologyId,
            ExperienceYears = 25
        },
        new Doctor
        {
            FullName = "Петрова Ольга Игоревна",
            DateOfBirth = new DateTime(1990, 11, 17),
            SpecializationId = _orthopedicsId,
            ExperienceYears = 5
        },
        new Doctor
        {
            FullName = "Васильев Игорь Николаевич",
            DateOfBirth = new DateTime(1965, 2, 28),
            SpecializationId = _pediatricsId,
            ExperienceYears = 30
        },
        new Doctor
        {
            FullName = "Николаева Екатерина Владимировна",
            DateOfBirth = new DateTime(1982, 7, 14),
            SpecializationId = _cardiologyId,
            ExperienceYears = 12
        },
        new Doctor
        {
            FullName = "Алексеев Павел Михайлович",
            DateOfBirth = new DateTime(1975, 9, 3),
            SpecializationId = _dermatologyId,
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
            PatientId = Patients[0].Id, // Иванов Иван Иванович
            DoctorId = Doctors[0].Id, // Смирнов Александр Васильевич (Кардиолог)
            AppointmentDateTime = new DateTime(2025, 8, 15, 10, 0, 0),
            RoomNumber = 101,
            IsReturnVisit = false
        },
        new Appointment
        {
            PatientId = Patients[1].Id, // Петрова Анна Сергеевна
            DoctorId = Doctors[1].Id, // Иванова Мария Петровна (Педиатр)
            AppointmentDateTime = new DateTime(2025, 8, 15, 11, 30, 0),
            RoomNumber = 205,
            IsReturnVisit = true
        },
        new Appointment
        {
            PatientId = Patients[2].Id, // Сидоров Михаил Петрович
            DoctorId = Doctors[2].Id, // Кузнецов Дмитрий Сергеевич (Хирург)
            AppointmentDateTime = new DateTime(2025, 9, 16, 9, 0, 0),
            RoomNumber = 312,
            IsReturnVisit = false
        },
        new Appointment
        {
            PatientId = Patients[3].Id, // Козлова Елена Викторовна
            DoctorId = Doctors[3].Id, // Петрова Ольга Игоревна (Невролог)
            AppointmentDateTime = new DateTime(2025, 8, 16, 14, 15, 0),
            RoomNumber = 118,
            IsReturnVisit = false
        },
        new Appointment
        {
            PatientId = Patients[4].Id, // Николаев Дмитрий Александрович
            DoctorId = Doctors[4].Id, // Васильев Игорь Николаевич (Терапевт)
            AppointmentDateTime = new DateTime(2025, 8, 17, 16, 45, 0),
            RoomNumber = 201,
            IsReturnVisit = true
        },
        new Appointment
        {
            PatientId = Patients[0].Id, // Иванов Иван Иванович
            DoctorId = Doctors[6].Id, // Алексеев Павел Михайлович (Офтальмолог)
            AppointmentDateTime = new DateTime(2025, 8, 18, 13, 0, 0),
            RoomNumber = 404,
            IsReturnVisit = false
        },
        new Appointment
        {
            PatientId = Patients[1].Id, // Петрова Анна Сергеевна
            DoctorId = Doctors[0].Id, // Соколова Анна Дмитриевна (Гинеколог)
            AppointmentDateTime = new DateTime(2025, 8, 19, 10, 30, 0),
            RoomNumber = 222,
            IsReturnVisit = true
        },
        new Appointment
        {
            PatientId = Patients[2].Id, // Сидоров Михаил Петрович
            DoctorId = Doctors[1].Id, // Смирнов Александр Васильевич (Кардиолог)
            AppointmentDateTime = new DateTime(2025, 8, 19, 15, 20, 0),
            RoomNumber = 101,
            IsReturnVisit = false
        }
    ];
}