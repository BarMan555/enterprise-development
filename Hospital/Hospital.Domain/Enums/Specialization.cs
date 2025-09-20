namespace Hospital.Domain.Enums;

/// <summary>
/// Represent og the Specialization for <see cref="Models.Doctor"/>
/// </summary>
public class Specialization
{
    /// <summary>
    /// Unique identificator of the Specialization
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Name of the Specialization
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Description of the Specialization
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// Helper class with constants for <see cref="Specialization"/>
/// </summary>
public static class SpecializationName
{
    public const string Therapist = "THERAPIST";
    public const string Surgeon = "SURGEON";
    public const string Cardiologist = "CARDIOLOGIST";
    public const string Neurologist = "NEUROLOGIST";
    public const string Pediatrician = "PEDIATRICIAN";
    public const string Dentist = "DENTIST";
    public const string Ophthalmologist = "OPHTHALMOLOGIST";
}
