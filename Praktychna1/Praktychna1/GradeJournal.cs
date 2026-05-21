using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna1
{
    public class GradeJournal
    {
        // Зберігає оцінки студента по предметах (Назва предмета -> Оцінка)
        private Dictionary<string, double> _subjectGrades = new();

        // Публічний доступ до оцінок (тільки для читання зовні)
        public IReadOnlyDictionary<string, double> SubjectGrades => _subjectGrades;

        // Додати або оновити оцінку за предмет
        public void SetGrade(string subject, double grade)
        {
            if (grade < 0 || grade > 100)
                throw new ArgumentOutOfRangeException(nameof(grade), "Оцінка має бути від 0 до 100");

            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("Назва предмета не може бути порожньою");

            _subjectGrades[subject] = grade;
        }

        // Метод перерахунку середнього балу
        public double CalculateAverage()
        {
            if (_subjectGrades.Count == 0) return 0;
            return Math.Round(_subjectGrades.Values.Average(), 2);
        }

        // Видалити предмет з журналу
        public bool RemoveSubject(string subject) => _subjectGrades.Remove(subject);

        // Отримати список усіх предметів та оцінок текстом
        public string GetGradesSummary()
        {
            if (_subjectGrades.Count == 0) return "Оцінок ще немає.";
            return string.Join(", ", _subjectGrades.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
        }
    }
}
