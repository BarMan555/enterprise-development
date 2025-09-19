using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Models;

/// <summary>
/// Represents a <see cref="Appointment"/> for registering a <see cref="Patient"/> with a <see cref="Doctor"/>
/// </summary>
public class Appointment
{
    /// <summary>
    /// Unique identificator of the <see cref="Appointment"/>
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// <see cref="Patient"/> which registering on <see cref="Appointment"/>
    /// </summary>
    public required Patient Patient { get; set; }

    /// <summary>
    /// <see cref="Doctor"/> for <see cref="Appointment"/>
    /// </summary>
    public required Doctor Doctor { get; set; }

    /// <summary>
    /// Date of the <see cref="Appointment"/>
    /// </summary>
    public required DateTime AppointmentDateTime { get; set; }

    /// <summary>
    /// Room of Number where <see cref="Appointment"/>
    /// </summary>
    public required int RoomNumber { get; set; }

    /// <summary>
    /// <see cref="Appointment"/> is repeat or not
    /// </summary>
    public required bool IsRepeat { get; set; }
}
