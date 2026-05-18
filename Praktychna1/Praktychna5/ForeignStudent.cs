using Praktychna1.Praktychna1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna5
{
    public class ForeignStudent : Student
    {
        public string CountryOfOrigin { get; set; }
        public DateTime VisaExpirationDate { get; set; }

        public ForeignStudent(string fullName, DateTime dob, string email,
                              string recordBook, double grade, StudentStatus status, string country, DateTime visaExp)
            : base(fullName, dob, email, recordBook, grade, status, "Іноземний студент")
        {
            CountryOfOrigin = country;
            VisaExpirationDate = visaExp;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", Країна: {CountryOfOrigin}, Віза до: {VisaExpirationDate.ToShortDateString()}";
        }

        public override void Enroll()
        {
            base.Enroll();
            Console.WriteLine($"Міграційний статус: Громадянин країни {CountryOfOrigin}. Віза дійсна до {VisaExpirationDate.ToShortDateString()}.");
        }
    }
}
