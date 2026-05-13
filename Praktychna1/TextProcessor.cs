using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1
{
    internal class TextProcessor
    {
        public string Reverse(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            char[] array = input.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        public int CountWords(string text) =>
            string.IsNullOrWhiteSpace(text) ? 0 : text.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

        public string Normalize(string text) =>
            string.Join(" ", text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).Trim();

        public bool IsPalindrome(string text, bool ignoreCase = true, bool ignoreSpaces = true)
        {
            if (string.IsNullOrEmpty(text)) return false;
            string processed = text;
            if (ignoreSpaces) processed = processed.Replace(" ", "");
            if (ignoreCase) processed = processed.ToLower();
            string reversed = new string(processed.Reverse().ToArray());
            return processed == reversed;
        }

        // ВІДКОРИГОВАНИЙ ЗВІТ: тепер він бачить предмет та оцінку
        public string BuildGroupReport(StudentGroup group)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\n" + new string('=', 70));
            sb.AppendFormat("║ {0,-25} | {1,-15} | {2,-10} | {3,-8} ║\n", "ПІБ Студента", "Заліковка", "Предмет", "Оцінка");
            sb.AppendLine(new string('=', 70));

            var students = group.GetAllStudents();
            foreach (var student in students)
            {
                // Отримуємо оцінки з Journal
                var grades = student.Journal.SubjectGrades;
                var firstEntry = grades.FirstOrDefault();

                string subject = string.IsNullOrEmpty(firstEntry.Key) ? "---" : firstEntry.Key;
                string score = firstEntry.Value == 0 ? "0" : firstEntry.Value.ToString();

                sb.AppendFormat("║ {0,-25} | {1,-15} | {2,-10} | {3,-8} ║\n",
                    student.FullName.Length > 25 ? student.FullName.Substring(0, 22) + "..." : student.FullName,
                    student.RecordBookNumber,
                    subject,
                    score);
            }

            sb.AppendLine(new string('=', 70));
            sb.AppendLine($"Всього студентів: {students.Count}");
            return sb.ToString();
        }

        public string ComparePerformance(int iterations)
        {
            if (iterations > 100000) iterations = 100000;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string s = "";
            for (int i = 0; i < iterations; i++) s += "test";
            sw.Stop();
            long t1 = sw.ElapsedMilliseconds;

            sw.Restart();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < iterations; i++) sb.Append("test");
            sw.Stop();
            return $"String: {t1} ms | StringBuilder: {sw.ElapsedMilliseconds} ms";
        }

        public string AnalyzeSentiment(StudentGroup group)
        {
            int pos = 0, neg = 0;
            string[] posW = { "відмінно", "успіх", "добре", "супер" };
            string[] negW = { "погано", "проблема", "важко", "борг" };

            foreach (var s in group.GetAllStudents())
            {
                if (string.IsNullOrEmpty(s.Notes)) continue;
                string n = s.Notes.ToLower();
                pos += posW.Count(w => n.Contains(w));
                neg += negW.Count(w => n.Contains(w));
            }
            return $"\n>>> АНАЛІЗ НАСТРОЮ <<<\nПозитив: {pos} | Негатив: {neg}\nВисновок: {(pos >= neg ? "Все супер! 😊" : "Потрібна допомога! 😟")}";
        }
    }
}