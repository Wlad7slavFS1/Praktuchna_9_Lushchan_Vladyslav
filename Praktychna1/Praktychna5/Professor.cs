using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna5
{
    // Запечатаний клас, бо професор — це найвище звання, далі наслідувати немає сенсу
    public sealed class Professor : Teacher
    {
        public string ScienceDegree { get; set; } // Вчене звання (напр. Доктор наук)
        public decimal AcademicBonus { get; set; } // Надбавка за звання

        public Professor(string fullName, DateTime dob, string email, string department, decimal baseSalary, string degree, decimal bonus)
            : base(fullName, dob, email, department, baseSalary)
        {
            ScienceDegree = degree;
            AcademicBonus = bonus;
            Notes = "Професор";
        }

        public override decimal CalculateScholarship()
        {
            // Професор отримує ставку + надбавку за звання
            return BaseSalary + AcademicBonus;
        }

        public override string GetInfo()
        {
            return base.GetInfo().Replace("Викладач", $"Професор ({ScienceDegree})") + $" | Надбавка: +{AcademicBonus} грн.";
        }
    }
}
