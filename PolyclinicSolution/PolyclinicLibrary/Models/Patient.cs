using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hospital.Domain.Enums;

namespace Hospital.Domain.Models;
public class Patient
{
    public required int Id { get; set; }
    public required string FullName { get; set; }
    public required Gender Gender { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string Address { get; set; }
    public required BloodType BloodType { get; set; }
    public required RhFactor RhFactor { get; set; }
    public required string PhoneNumber { get; set; }
}

// Пациент характеризуется номером паспорта, ФИО, полом,
// датой рождения, адресом, группой крови,
// резус фактором и контактным телефоном.