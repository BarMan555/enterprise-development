using Hospital.Domain.Enums;
using Hospital.Domain.Models;

namespace Hospital.Application.Contracts.Dtos;

/// <summary>
/// DTO Represents a Patient who can book <see cref="Appointment"/>.
/// </summary>
public class PatientDto
{
    /// <summary>
    /// Unique identifier of the Patient
    /// </summary>
    public required int Id { get; set; }
    
    /// <summary>
    /// Namec of the Patient
    /// </summary>
    public required string FullName { get; set; }
    
    /// <summary>
    /// Gender of the Patient
    /// </summary>
    public required Gender Gender { get; set; }

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
    public required BloodType BloodType { get; set; }

    /// <summary>
    /// RhFactor of the Patient
    /// </summary>
    public required RhFactor RhFactor { get; set; }

    /// <summary>
    /// Phone Number of the Patient
    /// </summary>
    public required string PhoneNumber { get; set; }
}