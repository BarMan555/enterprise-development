using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PolyclinicLibrary.Enums;

namespace PolyclinicLibrary.Models;
public class Patient
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public BloodType BloodType { get; set; }
    public RhFactor RhFactor { get; set; }
    public string PhoneNumber { get; set; }
}

// Пациент характеризуется номером паспорта, ФИО, полом,
// датой рождения, адресом, группой крови,
// резус фактором и контактным телефоном.