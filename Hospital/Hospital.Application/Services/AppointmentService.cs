using Hospital.Domain.Models;
using Hospital.Domain.Repositories;
using Hospital.Application.Dto;

namespace Hospital.Application.Services;

public class AppointmentService(IAppointmentRepository repository)
{
    private static Appointment MapDto(AppointmentDto entity)
    {
        return new Appointment
        {
            Id = -1,
            Patient = entity.Patient,
            Doctor = entity.Doctor,
            AppointmentDateTime = entity.AppointmentDateTime,
            RoomNumber = entity.RoomNumber,
            IsReturnVisit = entity.IsReturnVisit
        };
    }
    
    public int CreateAppointment(AppointmentDto entity)
    {
        return repository.Create(MapDto(entity));
    }

    public List<Appointment> ReadAppointment()
    {
        return repository.Read();
    }

    public Appointment? ReadAppointment(int id)
    {
        return repository.Read(id);
    }

    public Appointment? UpdateAppointment(int id, AppointmentDto entity)
    {
        return repository.Update(id, MapDto(entity));
    }

    public bool DeleteAppointment(int id)
    {
        return repository.Delete(id);
    }

    public List<Patient> FindPatientsByDoctor(int doctorId)
    {
        return (from a in repository.Read()
            where a.Doctor.Id == doctorId
            select a.Patient).ToList();
    }

    public int CountAppointmentWithRepeatVisitsInDate(DateTime start, DateTime end)
    {
        return (from a in repository.Read()
                where a.AppointmentDateTime >= start && a.AppointmentDateTime <= end
                select a.Id).Count();
    }

    public List<Patient> SortPatientWithMultipleDoctors(List<Patient> patients)
    {
        return (patients
            .Where(p => repository.Read().Count(a => a.Patient.Id == p.Id) > 1)
            .OrderBy(p => p.DateOfBirth)
            ).ToList();
    }

    public List<Appointment> FindAppoinmentsInSpecificRoomInDate(int roomNumber,  DateTime start, DateTime end)
    {
        return (from a in repository.Read()
                where a.RoomNumber == roomNumber
                where a.AppointmentDateTime >= start && a.AppointmentDateTime <= end
                select a).ToList();
    }
}