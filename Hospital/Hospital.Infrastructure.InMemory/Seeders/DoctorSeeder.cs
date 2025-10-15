using Hospital.Domain.Enums;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure.InMemory.Seeders;

public class InMemoryDoctorRepositorySeeder
{
    private List<Specialization> _specializations =
    [
        new Specialization() { Id = 0, Name = SpecializationName.Pediatrician },
        new Specialization() { Id = 1, Name = SpecializationName.Surgeon },
        new Specialization() { Id = 2, Name = SpecializationName.Neurologist },
        new Specialization() { Id = 3, Name = SpecializationName.Cardiologist },
        new Specialization() { Id = 4, Name = SpecializationName.Dentist },
        new Specialization() { Id = 5, Name = SpecializationName.Ophthalmologist },
        new Specialization() { Id = 6, Name = SpecializationName.Therapist },
    ];
    
    public List<Doctor> GetItems() =>
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

    public int GetCurrentId() => 8;
}