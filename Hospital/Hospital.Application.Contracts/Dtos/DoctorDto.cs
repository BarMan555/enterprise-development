using Hospital.Domain.Enums;

namespace Hospital.Application.Contracts.Dtos;

/// <summary>
/// DTO Represents a Doctor
/// </summary>
public class DoctorDto
{
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