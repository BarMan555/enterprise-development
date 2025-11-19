using MongoDB.Bson;

namespace Hospital.Application.Contracts.Dtos;

/// <summary>
/// DTO Represents an Appointment
/// </summary>
public class AppointmentCreateUpdateDto
{
    /// <summary>
    /// ID Patient which registering on Appointment
    /// </summary>
    public required string IdPatient { get; set; }

    /// <summary>
    /// ID Doctor for Appointment
    /// </summary>
    public required string IdDoctor { get; set; }

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