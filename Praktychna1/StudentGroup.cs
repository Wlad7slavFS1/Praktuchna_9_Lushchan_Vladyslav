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
        public string GroupName { get; set; }
        public string Specialization { get; set; }
        public int Course { get; set; }

        private List<Student> _students = new List<Student>();

        public int GroupSize => _students.Count;
        public double AverageGroupGrade => _students.Any() ? _students.Average(s => s.AverageGrade) : 0;

        public void AddStudent(Student s) => _students.Add(s);

        public void RemoveStudent(string recordBookNumber) =>
            _students.RemoveAll(s => s.RecordBookNumber == recordBookNumber);

        public List<Student> GetExcellentStudents() =>
            _students.Where(s => s.IsExcellent()).ToList();

        // Збереження в JSON (вимога завдання)[cite: 1]
        public void SaveToFile(string filename)
        {
            string json = JsonSerializer.Serialize(_students);
            File.WriteAllText(filename, json);
        }

        public void LoadFromFile(string filename)
        {
            if (!File.Exists(filename)) return;
            string json = File.ReadAllText(filename);
            _students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
        }

        public List<Student> GetAllStudents() => _students;
    }
}