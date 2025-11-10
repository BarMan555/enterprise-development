using Hospital.Domain.Models;
using MongoDB.Driver;

namespace Hospital.Infrastructure.EfCore.Repositories;
public class PatientEfCoreRepository : MongoClient<Patient, int>
{
    public PatientEfCoreRepository(IMongoDatabase database) : base(database, "Patients"){}
}