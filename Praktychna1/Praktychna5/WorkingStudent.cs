using Praktychna1.Praktychna1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna5
{
    public class WorkingStudent : Student
    {
        public string JobTitle { get; set; }

        public WorkingStudent(string fullName, DateTime dob, string email,
                              string recordBook, double grade, StudentStatus status, string jobTitle)
            : base(fullName, dob, email, recordBook, grade, status, "Працює")
        {
            JobTitle = jobTitle;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", Посада: {JobTitle}";
        }

        public override void Enroll()
        {
            base.Enroll();
            Console.WriteLine($"Особливі умови: Індивідуальний графік навчання у зв'язку з роботою на посаді: {JobTitle}.");
        }
    }
}
