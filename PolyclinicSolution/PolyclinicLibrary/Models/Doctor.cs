using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PolyclinicLibrary.Enums;

namespace PolyclinicLibrary.Models;
public class Doctor
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Specialization Specialization { get; set; }
    public int ExperienceYears { get; set; }

}

//Информация о враче включает номер паспорта, ФИО, год рождения, специализацию, стаж работы.
//Специализация врача является справочником.

