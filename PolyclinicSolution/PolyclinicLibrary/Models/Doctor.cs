using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hospital.Domain.Enums;

namespace Hospital.Domain.Models;
public class Doctor
{
    public required int Id { get; set; }
    public required string FullName { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required Specialization Specialization { get; set; }
    public required int ExperienceYears { get; set; }

}

//Информация о враче включает номер паспорта, ФИО, год рождения, специализацию, стаж работы.
//Специализация врача является справочником.

