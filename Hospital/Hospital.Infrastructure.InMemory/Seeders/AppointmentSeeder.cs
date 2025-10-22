using Hospital.Domain.Enums;
using Hospital.Domain.Models;


namespace Hospital.Infrastructure.InMemory.Seeders;

/// <summary>
/// Seeder for appointments repository
/// </summary>
public class InMemoryAppointmentRepositorySeeder :  ISeeder<Appointment, int>
{
    /// <summary>
    /// List of patients
    /// </summary>
    private List<Patient> _patients = (new InMemoryPatientRepositorySeeder()).GetItems();
    
    /// <summary>
    /// List of doctors
    /// </summary>
    private List<Doctor> _doctors = (new InMemoryDoctorRepositorySeeder()).GetItems();
    
    /// <summary>
    /// Get list of data
    /// </summary>
    /// <returns>List of data</returns>
    public List<Appointment> GetItems() =>
    [

        new Appointment
        {
            Id = 1,
            Patient = _patients[0], // Иванов Иван Иванович
            Doctor = _doctors[0], // Смирнов Александр Васильевич (Кардиолог)
            AppointmentDateTime = new DateTime(2025, 8, 15, 10, 0, 0),
            RoomNumber = 101,
            IsReturnVisit = false
        },
        new Appointment
        {
            Id = 2,
            Patient = _patients[1], // Петрова Анна Сергеевна
            Doctor = _doctors[1], // Иванова Мария Петровна (Педиатр)
            AppointmentDateTime = new DateTime(2025, 8, 15, 11, 30, 0),
            RoomNumber = 205,
            IsReturnVisit = true
        },
        new Appointment
        {
            Id = 3,
            Patient = _patients[2], // Сидоров Михаил Петрович
            Doctor = _doctors[2], // Кузнецов Дмитрий Сергеевич (Хирург)
            AppointmentDateTime = new DateTime(2025, 9, 16, 9, 0, 0),
            RoomNumber = 312,
            IsReturnVisit = false
        },
        new Appointment
        {
            Id = 4,
            Patient = _patients[3], // Козлова Елена Викторовна
            Doctor = _doctors[3], // Петрова Ольга Игоревна (Невролог)
            AppointmentDateTime = new DateTime(2025, 8, 16, 14, 15, 0),
            RoomNumber = 118,
            IsReturnVisit = false
        },
        new Appointment
        {
            Id = 5,
            Patient = _patients[4], // Николаев Дмитрий Александрович
            Doctor = _doctors[4], // Васильев Игорь Николаевич (Терапевт)
            AppointmentDateTime = new DateTime(2025, 8, 17, 16, 45, 0),
            RoomNumber = 201,
            IsReturnVisit = true
        },
        new Appointment
        {
            Id = 6,
            Patient = _patients[0], // Иванов Иван Иванович
            Doctor = _doctors[6], // Алексеев Павел Михайлович (Офтальмолог)
            AppointmentDateTime = new DateTime(2025, 8, 18, 13, 0, 0),
            RoomNumber = 404,
            IsReturnVisit = false
        },
        new Appointment
        {
            Id = 7,
            Patient = _patients[1], // Петрова Анна Сергеевна
            Doctor = _doctors[0], // Соколова Анна Дмитриевна (Гинеколог)
            AppointmentDateTime = new DateTime(2025, 8, 19, 10, 30, 0),
            RoomNumber = 222,
            IsReturnVisit = true
        },
        new Appointment
        {
            Id = 8,
            Patient = _patients[2], // Сидоров Михаил Петрович
            Doctor = _doctors[1], // Смирнов Александр Васильевич (Кардиолог)
            AppointmentDateTime = new DateTime(2025, 8, 19, 15, 20, 0),
            RoomNumber = 101,
            IsReturnVisit = false
        }
    ];
    
    /// <summary>
    /// Get ID to start counting
    /// </summary>
    /// <returns>ID</returns>
    public int GetCurrentId() => 9;
}