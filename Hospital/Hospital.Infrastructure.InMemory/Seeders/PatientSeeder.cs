using Hospital.Domain.Enums;
using Hospital.Domain.Models;

namespace Hospital.Infrastructure.InMemory.Seeders;

public class InMemoryPatientRepositorySeeder : ISeeder<Patient, int>
{
    public List<Patient> GetItems() =>
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

    public int GetCurrentId() => 6;
}