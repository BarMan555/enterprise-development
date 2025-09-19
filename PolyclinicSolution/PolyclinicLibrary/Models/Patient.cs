using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hospital.Domain.Enums;

namespace Hospital.Domain.Models;

/// <summary>
/// Represents a <see cref="Patient"/> who can book <see cref="Appointment"/>.
/// </summary>
public class Patient
{
    /// <summary>
    /// Unique identifier of the <see cref="Patient"/>
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Namec of the <see cref="Patient"/>
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gender of the <see cref="Patient"/>
    /// </summary>
    public required Gender Gender { get; set; }

    /// <summary>
    /// Date of Birthday of the <see cref="Patient"/>
    /// </summary>
    public required DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Address where lives <see cref="Patient"/>
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Blood type of the <see cref="Patient"/>
    /// </summary>
    public required BloodType BloodType { get; set; }

    /// <summary>
    /// RhFactor of the <see cref="Patient"/>
    /// </summary>
    public required RhFactor RhFactor { get; set; }

    /// <summary>
    /// Phone Number of the <see cref="Patient"/>
    /// </summary>
    public required string PhoneNumber { get; set; }
}