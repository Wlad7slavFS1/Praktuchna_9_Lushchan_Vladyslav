using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna6
{
    // Базовий клас працівника
    public abstract class Employee : IPrintable
    {
        public string FullName { get; set; }
        public decimal BaseSalary { get; set; }

        protected Employee(string fullName, decimal baseSalary)
        {
            FullName = fullName;
            BaseSalary = baseSalary;
        }

        // Абстрактний метод для розрахунку зарплати з урахуванням бонусів
        public abstract decimal CalculateSalary();

        public virtual string GetPrintInfo() => $"{FullName}, Базова ставка: {BaseSalary}";
    }
}
