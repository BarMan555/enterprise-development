using Hospital.Domain.Models;
using MongoDB.Driver;

namespace Hospital.Infrastructure.EfCore.Repositories;
public class DoctorEfCoreRepository: MongoClient<Patient, int>
{
    public DoctorEfCoreRepository(IMongoDatabase database) : base(database, "Doctors"){}
}