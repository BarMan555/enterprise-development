using MongoDB.Bson;

namespace Hospital.Application.Contracts.Dtos;

/// <summary>
/// DTO Represents a Doctor for receiving Doctor entitites
/// </summary>
public class DoctorGetDto
{
    /// <summary>
    /// Unique identificator of the Appointment
    /// </summary>
    public required string Id { get; set; }
    
    /// <summary>
    /// Name of the Doctor
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Date of Birthday of the Doctor
    /// </summary>
    public required DateTime DateOfBirth { get; set; }

    /// <summary>
    /// ID of specialization of the Doctor
    /// </summary>
    public required int IdSpecialization { get; set; }

    /// <summary>
    /// Experience of the Doctor
    /// </summary>
    public required int ExperienceYears { get; set; }
}