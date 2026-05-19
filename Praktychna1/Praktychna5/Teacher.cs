using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna5
{
    public class Teacher : Person
    {
        public string Department { get; set; } // Кафедра
        public decimal BaseSalary { get; set; } // Базова ставка

        public Teacher(string fullName, DateTime dob, string email, string department, decimal baseSalary)
            : base(fullName, dob, email, "Викладач")
        {
            Department = department;
            BaseSalary = baseSalary;
        }

        // Перевизначаємо метод розрахунку виплат (для викладачів це зарплата)
        public override decimal CalculateScholarship()
        {
            return BaseSalary; // Базова виплата викладача
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" | Посада: Викладач | Кафедра: {Department} | Оклад: {BaseSalary} грн.";
        }

        public override void Enroll()
        {
            Console.WriteLine($"Викладача {FullName} успішно прийнято на кафедру {Department}.");
        }
    }
}
