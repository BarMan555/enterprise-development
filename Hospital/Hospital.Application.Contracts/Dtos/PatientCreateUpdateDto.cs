namespace Hospital.Application.Contracts.Dtos;

/// <summary>
/// DTO Represents a Patient for creating and updating Patient entities
/// </summary>
public class PatientCreateUpdateDto
{ 
    /// <summary>
    /// Name of the Patient
    /// </summary>
    public required string FullName { get; set; }
    
    /// <summary>
    /// Gender of the Patient
    /// </summary>
    public required int Gender { get; set; }

    /// <summary>
    /// Date of Birthday of the Patient
    /// </summary>
    public required DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Address where lives Patient
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Blood type of the Patient
    /// </summary>
    public required int BloodType { get; set; }

    /// <summary>
    /// RhFactor of the Patient
    /// </summary>
    public required int RhFactor { get; set; }

    /// <summary>
    /// Phone Number of the Patient
    /// </summary>
    public required string PhoneNumber { get; set; }
}