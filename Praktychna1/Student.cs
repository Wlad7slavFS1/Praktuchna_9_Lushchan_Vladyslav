using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Praktychna1
{
    public enum StudentStatus { Active, AcademicLeave, Expelled, Graduated }

    public class Student : ICloneable
    {
        private string _fullName;
        private string _recordBookNumber;

        // Масив оцінок за лабораторні (з ПР №2)
        public byte[] LabGrades { get; set; } = new byte[10];

        // 1. Посилена валідація ПІБ (Вимога ПР №3)
        public string FullName
        {
            get => _fullName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ПІБ не може бути порожнім.");

                // Розбиваємо рядок на слова, видаляючи зайві пробіли
                string[] words = value.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (words.Length < 3)
                    throw new ArgumentException("ПІБ має містити щонайменше три слова: Прізвище, Ім'я та По батькові.");

                _fullName = string.Join(" ", words); // Зберігаємо в нормалізованому вигляді
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

        public GradeJournal Journal { get; } = new GradeJournal();
        public double AverageGrade => Journal.CalculateAverage();
        public StudentStatus Status { get; set; }
        public DateTime EnrollmentDate { get; init; } = DateTime.Now;

        // Поля для роботи з текстом (Вимога ПР №3)
        public string PersonalEmail { get; set; }
        public string Notes { get; set; } = ""; // Нотатки для аналізу настрою (Варіант 2)

        // --- Методи для роботи з оцінками ---

        public void AddLabGrade(int labNumber, byte grade)
        {
            if (labNumber < 0 || labNumber >= LabGrades.Length)
                throw new IndexOutOfRangeException("Номер лабораторної має бути від 0 до 9");
            LabGrades[labNumber] = grade;
        }

        public double GetAverageLabGrade()
        {
            return LabGrades.Length == 0 ? 0 : LabGrades.Select(x => (int)x).Average();
        }

        // --- Нові методи ПР №3 ---

        // 2. Форматування інформації через StringBuilder (Вимога ПР №3)
        public string GetFormattedInfo(bool detailed = false)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("╔══════════════════════════════════════════════╗");
            sb.AppendFormat("║ Студент: {0,-35} ║\n", FullName);
            sb.AppendFormat("║ Заліковка: {0,-33} ║\n", RecordBookNumber);
            sb.AppendFormat("║ Статус: {0,-36} ║\n", Status);

            if (detailed)
            {
                sb.AppendLine("╟──────────────────────────────────────────────╢");
                sb.AppendFormat("║ Вік: {0,-39} ║\n", Age);
                sb.AppendFormat("║ Сер. бал лаб: {0,-30:F2} ║\n", GetAverageLabGrade());
                sb.AppendFormat("║ Нотатки: {0,-35} ║\n",
                    Notes.Length > 35 ? Notes.Substring(0, 32) + "..." : Notes);
            }
            sb.AppendLine("╚══════════════════════════════════════════════╝");

            return sb.ToString();
        }

        // 3. Пошук за ключовим словом (Вимога ПР №3)
        public bool ContainsKeyword(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return false;

            return FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                   Notes.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                   RecordBookNumber.Contains(keyword);
        }

        // --- Старі методи та клонування ---

        public bool IsExcellent() => AverageGrade >= 90;
        public bool IsFailing() => AverageGrade < 60;

        public void ShowDetailedInfo()
        {
            Console.WriteLine(GetFormattedInfo(true));
        }

        public object Clone()
        {
            var clone = (Student)this.MemberwiseClone();
            clone.LabGrades = (byte[])this.LabGrades.Clone();
            return clone;
        }
    }
}