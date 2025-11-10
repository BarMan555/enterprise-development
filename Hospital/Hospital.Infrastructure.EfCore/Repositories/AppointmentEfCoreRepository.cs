using Hospital.Domain.Models;
using MongoDB.Driver;

namespace Hospital.Infrastructure.EfCore.Repositories;
public class AppointmentEfCoreRepository: MongoClient<Appointment, int>
{
    public AppointmentEfCoreRepository(IMongoDatabase database) : base(database, "Appointments"){}
}