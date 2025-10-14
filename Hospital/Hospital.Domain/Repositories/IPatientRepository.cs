using Hospital.Domain.Models;

namespace Hospital.Domain.Repositories;

public interface IPatientRepository
{
    public int Create(Patient entity);
    public List<Patient> Read();
    public Patient? Read(int id);
    public Patient? Update(int id, Patient entity);
    public bool Delete(int id);
}
