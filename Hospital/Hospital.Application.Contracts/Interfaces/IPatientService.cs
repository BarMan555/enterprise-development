using Hospital.Application.Contracts.Dtos;
using MongoDB.Bson;

namespace Hospital.Application.Contracts.Interfaces;

/// <summary>
/// Interface of Patient Service
/// </summary>
public interface IPatientService : IApplicationService<PatientGetDto, PatientCreateUpdateDto, ObjectId>
{
    /// <summary>
    /// Get appointments where is the patient
    /// </summary>
    /// <param name="id"></param>
    /// <returns>list of appointments dto</returns>
    public Task<List<AppointmentGetDto>?> GetAppointmentsByPatient(ObjectId id);
}