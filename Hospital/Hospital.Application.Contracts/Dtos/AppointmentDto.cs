using Hospital.Domain.Models;

namespace Hospital.Application.Contracts.Dtos;

/// <summary>
/// DTO Represents a Appointment for registering a <see cref="Patient"/> with a <see cref="Doctor"/>
/// </summary>
public class AppointmentDto
{
    /// <summary>
    /// <see cref="Patient"/> which registering on Appointment
    /// </summary>
    public required Patient Patient { get; set; }

    /// <summary>
    /// <see cref="Doctor"/> for Appointment
    /// </summary>
    public required Doctor Doctor { get; set; }

    /// <summary>
    /// Date of the Appointment
    /// </summary>
    public required DateTime AppointmentDateTime { get; set; }

    /// <summary>
    /// Room of Number where Appointment
    /// </summary>
    public required int RoomNumber { get; set; }

    /// <summary>
    /// Appointment is repeat or not
    /// </summary>
    public required bool IsReturnVisit { get; set; }
}