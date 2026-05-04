using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1
{
    public enum StudentStatus { Active, AcademicLeave, Expelled, Graduated }

    public class Student
    {
        private string _fullName;
        private string _recordBookNumber;

        public string FullName
        {
            get => _fullName;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                    throw new ArgumentException("ПІБ має містити мінімум 5 символів");
                _fullName = value;
            }
        }

        public DateTime DateOfBirth { get; init; }
        public int Age => DateTime.Now.Year - DateOfBirth.Year;

        public required string RecordBookNumber
        {
            get => _recordBookNumber;
            set
            {
                if (value?.Length != 8 || !long.TryParse(value, out _))
                    throw new ArgumentException("Номер заліковки має містити рівно 8 цифр");
                _recordBookNumber = value;
            }
        }

        // Журнал оцінок (інтеграція)
        public GradeJournal Journal { get; } = new GradeJournal();

        // Тепер AverageGrade вираховується автоматично через журнал
        public double AverageGrade => Journal.CalculateAverage();

        public StudentStatus Status { get; set; }
        public DateTime EnrollmentDate { get; init; } = DateTime.Now;
        public string PersonalEmail { get; set; }
        public string Notes { get; set; }

        public bool IsExcellent() => AverageGrade >= 90;
        public bool IsFailing() => AverageGrade < 60;
        public int GetYearsToGraduation() => 4 - (DateTime.Now.Year - EnrollmentDate.Year);

        public void ShowDetailedInfo()
        {
            Console.WriteLine($"[Студент] {FullName} | Квиток: {RecordBookNumber} | Вік: {Age} | Сер. бал: {AverageGrade}");
            Console.WriteLine($"   Оцінки: {Journal.GetGradesSummary()}");
        }
    }
}