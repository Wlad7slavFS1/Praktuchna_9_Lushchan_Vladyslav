using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna6
{
    // Нащадок: Викладач (бонус за кількість годин)
    public class Teacher : Employee
    {
        public int HoursCount { get; set; }

        public Teacher(string name, decimal salary, int hours) : base(name, salary) => HoursCount = hours;

        public override decimal CalculateSalary() => BaseSalary + HoursCount * 200;

        public override string GetPrintInfo() => $"[Teacher] {base.GetPrintInfo()}, Годин: {HoursCount}";
    }
}
