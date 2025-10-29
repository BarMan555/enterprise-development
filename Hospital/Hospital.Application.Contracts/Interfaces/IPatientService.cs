using Hospital.Application.Contracts.Dtos;

namespace Hospital.Application.Contracts.Interfaces;

/// <summary>
/// Interface of Patient Service
/// </summary>
public interface IPatientService : IApplicationService<PatientGetDto, PatientCreateUpdateDto, int>
{
    /// <summary>
    /// Get appointments where is the patient
    /// </summary>
    /// <param name="id"></param>
    /// <returns>list of appointments dto</returns>
    public List<AppointmentGetDto> GetAppointmentsByPatient(int id);
}