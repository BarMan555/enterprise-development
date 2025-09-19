using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hospital.Domain.Enums;

namespace Hospital.Domain.Models;

/// <summary>
/// Represents an <see cref="Doctor"> 
/// </summary>
public class Doctor
{
    /// <summary>
    /// Unique identifier of the <see cref="Doctor"/>
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Name of the <see cref="Doctor"/>
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Date of Birthday of the <see cref="Doctor"/>
    /// </summary>
    public required DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Specialization of the <see cref="Doctor"/>
    /// </summary>
    public required Specialization Specialization { get; set; }

    /// <summary>
    /// Experience of the <see cref="Doctor"/>
    /// </summary>
    public required int ExperienceYears { get; set; }

}

