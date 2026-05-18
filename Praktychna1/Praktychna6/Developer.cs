using Praktychna1.Praktychna6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1
{
    // Нащадок: Розробник (бонус за досвід)
    public class Developer : Employee
    {
        public string MainLanguage { get; set; }

        public Developer(string name, decimal salary, string lang) : base(name, salary) => MainLanguage = lang;

        public override decimal CalculateSalary() => BaseSalary + 5000; // Фіксований бонус розробника

        public override string GetPrintInfo() => $"[Developer] {base.GetPrintInfo()}, Мова: {MainLanguage}";
    }
}
