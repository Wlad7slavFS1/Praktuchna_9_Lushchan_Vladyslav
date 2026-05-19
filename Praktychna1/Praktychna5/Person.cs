using System;

namespace Praktychna1.Praktychna5
{
    public class Person : UniversityMember
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PersonalEmail { get; set; }
        public string Notes { get; set; }

        // Конструктор базового класу
        public Person(string fullName, DateTime dateOfBirth, string personalEmail, string notes = "")
        {
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            PersonalEmail = personalEmail;
            Notes = notes;
        }

        // Віртуальний метод для виведення інформації
        public virtual string GetInfo()
        {
            return $"Ім'я: {FullName}, Дата народження: {DateOfBirth.ToShortDateString()}, Email: {PersonalEmail}";
        }

        // Реалізуємо абстрактний метод з UniversityMember (для звичайної людини стипендія 0)
        public override decimal CalculateScholarship()
        {
            return 0;
        }
    }
}