using Hospital.Domain.Models;

namespace Hospital.Domain.Repositories;

public interface IAppointmentRepository
{
    public int Create(Appointment entity);
    public List<Appointment> Read();
    public Appointment? Read(int id);
    public Appointment? Update(int id, Appointment entity);
    public bool Delete(int id);
}