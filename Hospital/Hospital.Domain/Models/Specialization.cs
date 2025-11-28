using MongoDB.Bson;

namespace Hospital.Domain.Models;

/// <summary>
/// Represents a medical specialization
/// </summary>
public class Specialization
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public ObjectId Id { get; set; } =  ObjectId.GenerateNewId();

    /// <summary>
    /// Name of the specialization 
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Is the specialization currently active/available
    /// </summary>
    public bool IsActive { get; set; } = true;
}