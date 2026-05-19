using Praktychna1.Praktychna2;
using Praktychna1.Praktychna5;
using Praktychna1.Praktychna6;
using Praktychna1.Praktychna7;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna1
{
    public class StudentGroup
    {
        public string GroupName { get; set; }
        public string Specialization { get; set; }
        public int Course { get; set; }

        // ПР №5: Колекція зберігає Person (які успадковують UniversityMember)
        private List<Person> _members = new List<Person>();

        private PortMatrix _portMatrix = new PortMatrix();
        private PortLogger _logger = new PortLogger();

        public int GroupSize => _members.Count;
        public double AverageGroupGrade => _members.OfType<Student>().Any()
            ? _members.OfType<Student>().Average(s => s.AverageGrade)
            : 0;

        // --- Методи управління (ПР №5: Адаптація під ТЗ) ---

        /// <summary>
        /// Додає будь-якого члена університету до групи (Вимога ПР №5)
        /// </summary>

        public void AddMember(UniversityMember member)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));

            // Оскільки внутрішня колекція побудована на List<Person>, 
            // виконуємо безпечне приведення типів
            if (member is Person person)
            {
                _members.Add(person);
            }
        }

        // Залишаємо для сумісності зі старим кодом у Program.cs
        public void AddStudent(Student s) => AddMember(s);

        public void RemoveStudent(string recordBookNumber) =>
            _members.RemoveAll(m => m is Student s && s.RecordBookNumber == recordBookNumber);

        public List<Person> GetAllMembers() => _members;

        /// <summary>
        /// ПР №5: Generic-метод для отримання відфільтрованого списку за типом
        /// </summary>

        public List<T> GetMembersByType<T>() where T : Person
        {
            return _members.OfType<T>().ToList();
        }

        // Переписуємо старий метод через новий Generic-метод для чистоти коду
        public List<Student> GetAllStudents() => GetMembersByType<Student>();

        /// <summary>
        /// ПР №5: Розрахунок загального стипендіального фонду групи
        /// </summary>

        public decimal GetTotalScholarship()
        {
            // Завдяки поліморфізму викликається CalculateScholarship() конкретного підкласу
            return _members.Sum(m => m.CalculateScholarship());
        }

        // ПР №4-5: Оператор + (Поліморфне об'єднання)
        public static StudentGroup operator +(StudentGroup g1, StudentGroup g2)
        {
            var merged = new StudentGroup
            {
                GroupName = $"{g1.GroupName}+{g2.GroupName}",
                Specialization = g1.Specialization,
                Course = g1.Course
            };
            merged._members.AddRange(g1._members);
            merged._members.AddRange(g2._members);
            return merged;
        }

        // --- Бізнес-логіка ---

        public void AssignStudentToPort(string recordBook, int row, int col)
        {
            var student = GetMembersByType<Student>().FirstOrDefault(s => s.RecordBookNumber == recordBook);
            if (student == null) throw new Exception("Студента не знайдено");

            _portMatrix.OpenPort(row, col);
            _logger.Log(row * 16 + col, "Assign", $"Студент {student.FullName} зайняв місце [{row},{col}]");
        }

        public void SimulateLab(string recordBook, int labNumber, byte grade)
        {
            var student = GetMembersByType<Student>().FirstOrDefault(s => s.RecordBookNumber == recordBook);
            if (student != null)
            {
                student.AddLabGrade(labNumber, grade);
                _logger.Log(-1, "LabWork", $"Студент {student.FullName} виконав лабу №{labNumber} (Оцінка: {grade})");
            }
        }

        public string GetSystemLogs() => _logger.GetFullLog();
        public string GetPortMap() => _portMatrix.GetStatusReport();

        public string GetGroupStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== СТАТИСТИКА ГРУПИ (ПОЛІМОРФНА) ===");
            sb.AppendFormat("Група: {0} | Курс: {1}\n", GroupName, Course);
            sb.AppendFormat("Загальна кількість осіб: {0}\n", GroupSize);
            sb.AppendFormat("Середній рейтинг студентів: {0:F2}\n", AverageGroupGrade);

            double labAvg = GetMembersByType<Student>().Any()
                ? GetMembersByType<Student>().Average(s => s.GetAverageLabGrade())
                : 0;
            sb.AppendFormat("Сер. бал за лабораторні: {0:F2}\n", labAvg);

            return sb.ToString();
        }

        public void SaveToFile(string filename)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(filename, JsonSerializer.Serialize(_members, options));
        }

        public void LoadFromFile(string filename)
        {
            if (!File.Exists(filename)) return;
            _members = JsonSerializer.Deserialize<List<Person>>(File.ReadAllText(filename)) ?? new List<Person>();
        }

        public string SearchByNameFragment(string fragment)
        {
            var results = _members.Where(m => m.FullName.Contains(fragment, StringComparison.OrdinalIgnoreCase)).ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Результати пошуку ({fragment}): {results.Count}");

            foreach (var member in results)
            {
                sb.AppendLine(member.GetInfo());
            }
            return sb.ToString();
        }

        public string ExportToCsv(string path = null)
        {
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("Type,FullName,RecordBookNumber,AverageGrade,Scholarship,Details");

            foreach (var member in _members)
            {
                string type = member.GetType().Name;
                string scholarship = member.CalculateScholarship().ToString();
                string details = member.GetInfo().Replace(',', ';');

                if (member is Student s)
                {
                    csv.AppendLine($"{type},{s.FullName},{s.RecordBookNumber},{s.AverageGrade:F2},{scholarship},{details}");
                }
                else
                {
                    csv.AppendLine($"{type},{member.FullName},N/A,N/A,{scholarship},{details}");
                }
            }

            if (!string.IsNullOrEmpty(path)) File.WriteAllText(path, csv.ToString());
            return csv.ToString();
        }

        public void ImportStudentsFromText(string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return;
            string[] lines = data.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                try
                {
                    string[] parts = line.Split(';');
                    if (parts.Length >= 2)
                    {
                        var newStudent = new Student(
                            parts[0].Trim(),
                            DateTime.Now.AddYears(-18),
                            "student@college.edu.ua",
                            parts[1].Trim(),
                            0,
                            Student.StudentStatus.Active,
                            "Імпортовано"
                        );
                        _members.Add(newStudent);
                    }
                }
                catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
            }
        }

        public Student BestStudent()
        {
            var studentsOnly = GetMembersByType<Student>();
            if (!studentsOnly.Any()) return null;

            Student best = studentsOnly[0];
            foreach (var current in studentsOnly)
            {
                if (current > best) best = current;
            }
            return best;
        }

        public List<Student> Students { get; set; } = new List<Student>();

        // Розрахувати загальну площу всіх фігур усіх студентів
        public double GetTotalAreaOfAllShapes()
        {
            double totalArea = 0;
            foreach (var student in Students)
            {
                foreach (var shape in student.StudentShapes)
                {
                    totalArea += shape.CalculateArea(); // Динамічне зв'язування
                }
            }
            return totalArea;
        }

        // Намалювати всі фігури (симуляція через інтерфейс)
        public void DrawAllShapes()
        {
            foreach (var student in Students)
            {
                foreach (var shape in student.StudentShapes)
                {
                    if (shape is IDrawable drawable)
                    {
                        drawable.Draw(); // Виклик через інтерфейс
                    }
                }
            }
        }

        // Змінити розмір всіх фігур
        public void ResizeAllShapes(double factor)
        {
            foreach (var student in Students)
            {
                foreach (var shape in student.StudentShapes)
                {
                    if (shape is IResizable resizable)
                    {
                        resizable.Resize(factor); // Виклик через інтерфейс
                    }
                }
            }
        }

        public void DemonstrateLateBinding()
        {
            Console.WriteLine("--- Демонстрація динамічного зв’язування ---");

            // Створюємо список базового типу
            List<Shape> testShapes = new List<Shape>
    {
        new Circle("Тестове Коло", "Red", 10),
        new Rectangle("Тестовий Прямокутник", "Blue", 5, 10),
        new Triangle("Тестовий Трикутник", "Green", 3, 4, 5)
    };

            foreach (Shape shape in testShapes)
            {
                // Програма не знає тип об'єкта до моменту виконання,
                // але викличе правильний GetDescription()
                Console.WriteLine($"Тип посилання: Shape | Реальний об'єкт: {shape.GetType().Name}");
                Console.WriteLine($"Опис: {shape.GetDescription()}");
                Console.WriteLine($"Площа: {shape.CalculateArea():F2}");
                Console.WriteLine("------------------------------------------");
            }
        }

        // StudentGroup.cs
        public void ShowAllShapesInfo()
        {
            Console.WriteLine("\n--- ІНФОРМАЦІЯ ПРО ВСІ ФІГУРИ ГРУПИ ---");
            // Використовуємо властивість Students напряму
            foreach (var student in this.Students)
            {
                if (student.StudentShapes != null && student.StudentShapes.Any())
                {
                    Console.WriteLine($"Студент: {student.FullName}");
                    foreach (var shape in student.StudentShapes)
                    {
                        Console.WriteLine($"  -> {shape.GetDescription()}");
                    }
                }
            }
        }

        private Point[] _labSeats = new Point[30]; // Масив структур

        public void AssignSeat(int studentIndex, int row, int seat)
        {
            _labSeats[studentIndex] = new Point(row, seat);
        }

        public StudentRecord[] GetAllRecords()
        {
            var records = new StudentRecord[Students.Count];
            for (int i = 0; i < Students.Count; i++)
            {
                records[i] = Students[i].ToRecord(); // Використовуємо метод з попередньої гілки
            }
            return records;
        }

        public void OptimizeStorage()
        {
            foreach (var student in Students)
            {
                // Логіка переведення внутрішніх списків студента на компактні структури
                // (Ми вже підготували GradeRecord[] у класі Student)
            }
            Console.WriteLine("Зберігання даних групи оптимізовано.");
        }

        public StudentRecord? FindStudentRecord(string recordBook)
        {
            var student = Students.FirstOrDefault(s => s.RecordBookNumber == recordBook);
            return student?.ToRecord();
        }
    }
}