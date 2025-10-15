using Hospital.Domain.Models;

namespace Hospital.Domain.Repositories;

public interface IDoctorRepository
{
    public int Create(Doctor entity);
    public List<Doctor> Read();
    public Doctor? Read(int id);
    public Doctor? Update(int id, Doctor entity);
    public bool Delete(int id);
}