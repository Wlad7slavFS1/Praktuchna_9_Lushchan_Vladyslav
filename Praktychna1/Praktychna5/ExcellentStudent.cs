using Praktychna1.Praktychna1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna5
{
    public class ExcellentStudent : Student
    {
        public decimal PresidentialScholarshipBonus { get; set; } // Власна властивість

        public ExcellentStudent(string fullName, DateTime dateOfBirth, string personalEmail,
                                string recordBookNumber, double averageGrade, StudentStatus status, decimal bonus)
            : base(fullName, dateOfBirth, personalEmail, recordBookNumber, averageGrade, status, "Відмінник навчання")
        {
            PresidentialScholarshipBonus = bonus;
        }

        public override decimal CalculateScholarship() // override
        {
            return base.CalculateScholarship() + PresidentialScholarshipBonus;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", Президентська надбавка: +{PresidentialScholarshipBonus} грн";
        }

        public override void Enroll()
        {
            base.Enroll(); // Виклик логіки базового класу Student
            Console.WriteLine("Статус: Зараховано з відзнакою (призначено підвищену стипендію).");
        }
    }
}
