using Praktychna1.Praktychna1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna5
{
    public sealed class GraduateStudent : Student // sealed — забороняє подальше наслідування
    {
        public string ThesisTitle { get; set; }

        public GraduateStudent(string fullName, DateTime dob, string email,
                                   string recordBook, double grade, StudentStatus status, string thesisTitle)
                : base(fullName, dob, email, recordBook, grade, status, "Випускник")
        {
            ThesisTitle = thesisTitle;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", Тема диплому: \"{ThesisTitle}\"";
        }

        public override void Enroll()
        {
            base.Enroll();
            Console.WriteLine($"Статус випускника: Затверджено тему дипломної роботи \"{ThesisTitle}\".");
        }
    }
}
