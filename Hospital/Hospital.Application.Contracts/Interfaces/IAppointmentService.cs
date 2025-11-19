using Hospital.Application.Contracts.Dtos;
using MongoDB.Bson;

namespace Hospital.Application.Contracts.Interfaces;

/// <summary>
/// Interface of Appointment Service
/// </summary>
public interface IAppointmentService : IApplicationService<AppointmentGetDto, AppointmentCreateUpdateDto, ObjectId>
{
    /// <summary>
    /// Get Doctor of the appointment
    /// </summary>
    /// <param name="id"></param>
    /// <returns>doctor dto</returns>
    public Task<DoctorGetDto> GetDoctorByAppointment(ObjectId id);

    /// <summary>
    /// Get patient of the appointment
    /// </summary>
    /// <param name="id"></param>
    /// <returns>patient dto</returns>
    public Task<PatientGetDto> GetParientByAppointment(ObjectId id);
}