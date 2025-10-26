using Hospital.Domain.Models;
using Hospital.Application.Contracts.Dtos;

namespace Hospital.Application.Contracts.Interfaces;

/// <summary>
/// Interface of Doctor Service
/// </summary>
public interface IDoctorService : IApplicationService<DoctorDto, int>
{
    /// <summary>
    /// Get appointment where is the doctor 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>list of appointments</returns>
    public List<AppointmentDto> GetAppointmentsByDoctor(int id);
}