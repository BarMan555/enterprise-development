using Hospital.Domain.Models;
using Hospital.Application.Contracts.Dtos;

namespace Hospital.Application.Contracts.Interfaces;

/// <summary>
/// Interface of Appointment Service
/// </summary>
public interface IAppointmentService : IApplicationService<AppointmentGetDto, AppointmentCreateUpdateDto, int>
{
    /// <summary>
    /// Get Doctor of the appointment
    /// </summary>
    /// <param name="id"></param>
    /// <returns>doctor dto</returns>
    public DoctorGetDto GetDoctorByAppointment(int id);

    /// <summary>
    /// Get patient of the appointment
    /// </summary>
    /// <param name="id"></param>
    /// <returns>patient dto</returns>
    public PatientGetDto GetParientByAppointment(int id);
}