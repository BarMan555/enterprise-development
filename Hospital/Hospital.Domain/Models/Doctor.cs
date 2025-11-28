using MongoDB.Bson;

namespace Hospital.Domain.Models;

/// <summary>
/// Represents a Doctor
/// </summary>
public class Doctor
{
    /// <summary>
    /// Unique identifier of the Doctor
    /// </summary>
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    /// <summary>
    /// Name of the Doctor
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Date of Birthday of the Doctor
    /// </summary>
    public required DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Reference to the specialization ID
    /// </summary>
    public required ObjectId SpecializationId { get; set; }

    /// <summary>
    /// Navigation property
    /// </summary>
    public Specialization? Specialization { get; set; }

    /// <summary>
    /// Experience of the Doctor
    /// </summary>
    public required int ExperienceYears { get; set; }

}

