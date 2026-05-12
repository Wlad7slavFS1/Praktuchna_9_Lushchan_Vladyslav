using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Praktychna1
{
    public class StudentGroup
    {
        // Поля з ПР №1
        public string GroupName { get; set; }
        public string Specialization { get; set; }
        public int Course { get; set; }

        private List<Student> _students = new List<Student>();

        // Нові компоненти ПР №2
        private PortMatrix _portMatrix = new PortMatrix(); // Двовимірний масив портів
        private PortLogger _logger = new PortLogger();    // Логер на основі StringBuilder

        public int GroupSize => _students.Count;
        public double AverageGroupGrade => _students.Any() ? _students.Average(s => s.AverageGrade) : 0;

        // --- Методи управління студентами (ПР №1) ---
        public void AddStudent(Student s) => _students.Add(s);

        public void RemoveStudent(string recordBookNumber) =>
            _students.RemoveAll(s => s.RecordBookNumber == recordBookNumber);

        public List<Student> GetAllStudents() => _students;

        // --- Інтеграція та порти (ПР №2) ---

        // Прив'язка студента до конкретного порту (робочого місця)
        public void AssignStudentToPort(string recordBook, int row, int col)
        {
            var student = _students.FirstOrDefault(s => s.RecordBookNumber == recordBook);
            if (student == null) throw new Exception("Студента не знайдено");

            // Відкриваємо порт у матриці
            _portMatrix.OpenPort(row, col);

            // Логуємо подію (StringBuilder всередині)
            _logger.Log(row * 16 + col, "Assign", $"Студент {student.FullName} зайняв робоче місце [{row},{col}]");
        }

        // Симуляція лабораторної роботи
        public void SimulateLab(string recordBook, int labNumber, byte grade)
        {
            var student = _students.FirstOrDefault(s => s.RecordBookNumber == recordBook);
            if (student != null)
            {
                // Запис в одновимірний масив оцінок студента
                student.AddLabGrade(labNumber, grade);

                // Запис у лог
                _logger.Log(-1, "LabWork", $"Студент {student.FullName} виконав лабу №{labNumber} (Оцінка: {grade})");
            }
        }

        // --- Вивід інформації (StringBuilder обов'язково) --- 

        public string GetSystemLogs() => _logger.GetFullLog();

        public string GetPortMap() => _portMatrix.GetStatusReport(); // Виклик StringBuilder з PortMatrix

        public string GetGroupStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== СТАТИСТИКА ГРУПИ ===");
            sb.AppendFormat("Група: {0} | Курс: {1}\n", GroupName, Course);
            sb.AppendFormat("Кількість студентів: {0}\n", GroupSize);
            sb.AppendFormat("Загальний сер. бал: {0:F2}\n", AverageGroupGrade);

            // Статистика за лабораторними (ПР №2)
            double labAvg = _students.Any() ? _students.Average(s => s.GetAverageLabGrade()) : 0;
            sb.AppendFormat("Сер. бал за лабораторні: {0:F2}\n", labAvg);

            return sb.ToString();
        }

        // --- Робота з JSON ---
        public void SaveToFile(string filename)
        {
            // У ПР №2 також можна зберігати стан логів або матриці за потреби
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_students, options);
            File.WriteAllText(filename, json);
        }

        public void LoadFromFile(string filename)
        {
            if (!File.Exists(filename)) return;
            string json = File.ReadAllText(filename);
            _students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
        }

        // Пошук за фрагментом імені
        public string SearchByNameFragment(string fragment)
        {
            StringBuilder sb = new StringBuilder($"Результати пошуку для '{fragment}':\n");
            var found = _students.Where(s => s.FullName.Contains(fragment, StringComparison.OrdinalIgnoreCase));
            foreach (var s in found) sb.AppendLine(s.FullName);
            return sb.ToString();
        }

        // Експорт у CSV
        public string ExportToCsv()
        {
            StringBuilder sb = new StringBuilder("FullName,RecordBookNumber,AverageGrade\n");
            foreach (var s in _students)
                sb.AppendLine($"{s.FullName},{s.RecordBookNumber},{s.AverageGrade:F2}");
            return sb.ToString();
        }

        // Імпорт з тексту
        public void ImportStudentsFromText(string rawText)
        {
            string[] lines = rawText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                try { AddStudent(new Student { FullName = line.Trim(), RecordBookNumber = "00000000" }); }
                catch { /* Пропуск некоректних */ }
            }
        }
    }
}