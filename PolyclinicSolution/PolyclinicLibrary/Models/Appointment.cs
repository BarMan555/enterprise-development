using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Models;
public class Appointment
{
    public required int Id { get; set; }
    public required Patient Patient { get; set; }
    public required Doctor Doctor { get; set; }
    public required DateTime AppointmentDateTime { get; set; }
    public required int RoomNumber { get; set; }
    public required bool IsRepeat { get; set; }
}
