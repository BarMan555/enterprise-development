using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicLibrary.Models;
public class Appointment
{
    public string PatientPassportNumber { get; set; }
    public string DoctorPassportNumber { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public int RoomNumber { get; set; }
    public bool IsRepeat { get; set; }
}
