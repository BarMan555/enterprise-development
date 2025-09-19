using Hospital.Domain.Models;
using Hospital.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Fixtures;
public class HospitalFixture
{
    public List<Patient> Patients { get; } = [];
    public List<Doctor> Doctors { get; } = [];
    public List<Appointment> Appointments{ get; } = [];

    public HospitalFixture()
    {
        Patients.AddRange([
            new Patient{
                Id = 1,
                FullName = "Иванов Иван Иванович",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1985, 3, 15),
                Address = "г. Москва, ул. Ленина, д. 25, кв. 12",
                BloodType = BloodType.A,
                RhFactor = RhFactor.Positive,
                PhoneNumber = "+7 (495) 123-45-67"
            },
            new Patient{
                Id = 2,
                FullName = "Петрова Анна Сергеевна",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(1992, 7, 22),
                Address = "г. Санкт-Петербург, Невский пр-т, д. 48, кв. 34",
                BloodType = BloodType.B,
                RhFactor = RhFactor.Negative,
                PhoneNumber = "+7 (812) 987-65-43"
            },
            new Patient{
                Id = 3,
                FullName = "Сидоров Михаил Петрович",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1978, 11, 5),
                Address = "г. Екатеринбург, ул. Мира, д. 17, кв. 8",
                BloodType = BloodType.AB,
                RhFactor = RhFactor.Positive,
                PhoneNumber = "+7 (343) 456-78-90"
            },
            new Patient{
                Id = 4,
                FullName = "Козлова Елена Викторовна",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(2001, 1, 30),
                Address = "г. Новосибирск, ул. Кирова, д. 12, кв. 56",
                BloodType = BloodType.O,
                RhFactor = RhFactor.Negative,
                PhoneNumber = "+7 (383) 234-56-78"
            },
            new Patient{
                Id = 5,
                FullName = "Николаев Дмитрий Александрович",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1965, 9, 18),
                Address = "г. Казань, ул. Баумана, д. 33, кв. 21",
                BloodType = BloodType.A,
                RhFactor = RhFactor.Negative,
                PhoneNumber = "+7 (843) 765-43-21"
            }
        ]);

        Doctors.AddRange([
            new Doctor{
                Id = 1,
                FullName = "Смирнов Александр Васильевич",
                DateOfBirth = new DateTime(1978, 5, 12),
                Specialization = Specialization.Cardiologist,
                ExperienceYears = 15
                },
            new Doctor{
                Id = 2,
                FullName = "Иванова Мария Петровна",
                DateOfBirth = new DateTime(1985, 8, 24),
                Specialization = Specialization.Pediatrician,
                ExperienceYears = 8
            },
            new Doctor{
                Id = 3,
                FullName = "Кузнецов Дмитрий Сергеевич",
                DateOfBirth = new DateTime(1970, 3, 5),
                Specialization = Specialization.Surgeon,
                ExperienceYears = 25
            },
            new Doctor{
                Id = 4,
                FullName = "Петрова Ольга Игоревна",
                DateOfBirth = new DateTime(1990, 11, 17),
                Specialization = Specialization.Neurologist,
                ExperienceYears = 5
            },
            new Doctor{
                Id = 5,
                FullName = "Васильев Игорь Николаевич",
                DateOfBirth = new DateTime(1965, 2, 28),
                Specialization = Specialization.Therapist,
                ExperienceYears = 30
            },
            new Doctor{
                Id = 6,
                FullName = "Николаева Екатерина Владимировна",
                DateOfBirth = new DateTime(1982, 7, 14),
                Specialization = Specialization.Dentist,
                ExperienceYears = 12
            },
            new Doctor{
                Id = 7,
                FullName = "Алексеев Павел Михайлович",
                DateOfBirth = new DateTime(1975, 9, 3),
                Specialization = Specialization.Ophthalmologist,
                ExperienceYears = 18
        }]);

        Appointments.AddRange([

            new Appointment{
            Id = 1,
            Patient = Patients[0], // Иванов Иван Иванович
            Doctor = Doctors[0],   // Смирнов Александр Васильевич (Кардиолог)
            AppointmentDateTime = new DateTime(2024, 1, 15, 10, 0, 0),
            RoomNumber = 101,
            IsRepeat = false
            },
            new Appointment{
                Id = 2,
                Patient = Patients[1], // Петрова Анна Сергеевна
                Doctor = Doctors[1],   // Иванова Мария Петровна (Педиатр)
                AppointmentDateTime = new DateTime(2024, 1, 15, 11, 30, 0),
                RoomNumber = 205,
                IsRepeat = true
            },
            new Appointment{
                Id = 3,
                Patient = Patients[2], // Сидоров Михаил Петрович
                Doctor = Doctors[2],   // Кузнецов Дмитрий Сергеевич (Хирург)
                AppointmentDateTime = new DateTime(2024, 1, 16, 9, 0, 0),
                RoomNumber = 312,
                IsRepeat = false
            },
            new Appointment{
                Id = 4,
                Patient = Patients[3], // Козлова Елена Викторовна
                Doctor = Doctors[3],   // Петрова Ольга Игоревна (Невролог)
                AppointmentDateTime = new DateTime(2024, 1, 16, 14, 15, 0),
                RoomNumber = 118,
                IsRepeat = false
            },
            new Appointment{
                Id = 5,
                Patient = Patients[4], // Николаев Дмитрий Александрович
                Doctor = Doctors[4],   // Васильев Игорь Николаевич (Терапевт)
                AppointmentDateTime = new DateTime(2024, 1, 17, 16, 45, 0),
                RoomNumber = 201,
                IsRepeat = true
            },
            new Appointment{
                Id = 6,
                Patient = Patients[0], // Иванов Иван Иванович
                Doctor = Doctors[6],   // Алексеев Павел Михайлович (Офтальмолог)
                AppointmentDateTime = new DateTime(2024, 1, 18, 13, 0, 0),
                RoomNumber = 404,
                IsRepeat = false
            },
            new Appointment{
                Id = 7,
                Patient = Patients[1], // Петрова Анна Сергеевна
                Doctor = Doctors[0],   // Соколова Анна Дмитриевна (Гинеколог)
                AppointmentDateTime = new DateTime(2024, 1, 19, 10, 30, 0),
                RoomNumber = 222,
                IsRepeat = true
            },
            new Appointment{
                Id = 8,
                Patient = Patients[2], // Сидоров Михаил Петрович
                Doctor = Doctors[1],   // Смирнов Александр Васильевич (Кардиолог)
                AppointmentDateTime = new DateTime(2024, 1, 19, 15, 20, 0),
                RoomNumber = 101,
                IsRepeat = false
            }

        ]);
    }
}
