using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna5
{
    public class Assistant : Teacher
    {
        public string MentorName { get; set; } // Куратор/науковий керівник

        public Assistant(string fullName, DateTime dob, string email, string department, decimal baseSalary, string mentorName)
            : base(fullName, dob, email, department, baseSalary)
        {
            MentorName = mentorName;
            Notes = "Асистент";
        }

        public override decimal CalculateScholarship()
        {
            // Асистент отримує базову ставку викладача (наприклад, без надбавок)
            return BaseSalary;
        }

        public override string GetInfo()
        {
            return base.GetInfo().Replace("Викладач", "Асистент") + $" | Ментор: {MentorName}";
        }
    }
}
