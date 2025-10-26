using Hospital.Domain.Models;
using Hospital.Application.Contracts.Dtos;

namespace Hospital.Application.Contracts.Interfaces;

/// <summary>
/// Interface of Patient Service
/// </summary>
public interface IPatientService : IApplicationService<PatientDto, int>
{
    /// <summary>
    /// Get appointments where is the patient
    /// </summary>
    /// <param name="id"></param>
    /// <returns>list of appointments</returns>
    public List<AppointmentDto> GetAppointmentsByPatient(int id);
}