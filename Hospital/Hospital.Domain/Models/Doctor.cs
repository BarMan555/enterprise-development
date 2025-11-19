using Hospital.Domain.Enums;
using MongoDB.Bson;

namespace Hospital.Domain.Models;

/// <summary>
/// Represents an Doctor
/// </summary>
public class Doctor
{
    /// <summary>
    /// Unique identifier of the Doctor
    /// </summary>
    public required ObjectId Id { get; set; }

    /// <summary>
    /// Name of the Doctor
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Date of Birthday of the Doctor
    /// </summary>
    public required DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Specialization of the Doctor
    /// </summary>
    public required Specialization Specialization { get; set; }

    /// <summary>
    /// Experience of the Doctor
    /// </summary>
    public required int ExperienceYears { get; set; }

}

