using Hospital.Application.Contracts.Dtos;
using MongoDB.Bson;

namespace Hospital.Application.Contracts.Interfaces;

/// <summary>
/// Interface of Doctor Service
/// </summary>
public interface IDoctorService : IApplicationService<DoctorGetDto, DoctorCreateUpdateDto, ObjectId>
{
    /// <summary>
    /// Get appointment where is the doctor 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>list of appointments dto</returns>
    public Task<List<AppointmentGetDto>?> GetAppointmentsByDoctor(ObjectId id);
}