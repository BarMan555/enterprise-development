namespace Hospital.Application.Contracts.Dtos;

/// <summary>
/// DTO Represents a Doctor for creating and updating Doctor entities
/// </summary>
public class DoctorCreateUpdateDto
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
    /// ID of specialization of the Doctor
    /// </summary>
    public required string IdSpecialization { get; set; }

    /// <summary>
    /// Experience of the Doctor
    /// </summary>
    public required int ExperienceYears { get; set; }
}