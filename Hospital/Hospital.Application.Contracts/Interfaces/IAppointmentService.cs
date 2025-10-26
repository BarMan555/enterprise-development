using Hospital.Domain.Models;
using Hospital.Application.Contracts.Dtos;

namespace Hospital.Application.Contracts.Interfaces;

/// <summary>
/// Interface of Appointment Service
/// </summary>
public interface IAppointmentService : IApplicationService<AppointmentDto, int>
{
    /// <summary>
    /// Get Doctor of the appointment
    /// </summary>
    /// <param name="id"></param>
    /// <returns>doctor</returns>
    public DoctorDto GetDoctorByAppointment(int id);

    /// <summary>
    /// Get patient of the appointment
    /// </summary>
    /// <param name="id"></param>
    /// <returns>patient</returns>
    public PatientDto GetParientByAppointment(int id);
}