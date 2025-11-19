using MongoDB.Bson;

namespace Hospital.Domain.Models;

/// <summary>
/// Represents a Appointment for registering a <see cref="Patient"/> with a <see cref="Doctor"/>
/// </summary>
public class Appointment
{
    /// <summary>
    /// Unique identificator of the Appointment
    /// </summary>
    public required ObjectId Id { get; set; }
    
    /// <summary>
    /// ID of <see cref="Patient"/> which registering on Appointment
    /// </summary>
    public required ObjectId PatientId { get; set; }

    /// <summary>
    /// <see cref="Patient"/> which registering on Appointment
    /// </summary>
    public Patient? Patient { get; set; }

    /// <summary>
    /// ID of <see cref="Doctor"/> for Appointment
    /// </summary>
    public required ObjectId DoctorId { get; set; }
    
    /// <summary>
    /// <see cref="Doctor"/> for Appointment
    /// </summary>
    public Doctor? Doctor { get; set; }

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
