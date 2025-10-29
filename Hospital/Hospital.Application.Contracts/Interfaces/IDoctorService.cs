using Hospital.Application.Contracts.Dtos;

namespace Hospital.Application.Contracts.Interfaces;

/// <summary>
/// Interface of Doctor Service
/// </summary>
public interface IDoctorService : IApplicationService<DoctorGetDto, DoctorCreateUpdateDto, int>
{
    /// <summary>
    /// Get appointment where is the doctor 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>list of appointments dto</returns>
    public List<AppointmentGetDto> GetAppointmentsByDoctor(int id);
}