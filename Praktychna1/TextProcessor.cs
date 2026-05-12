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
        // 1. Реверс рядка
        public string Reverse(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            char[] array = input.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        // 2. Підрахунок слів
        public int CountWords(string text) =>
            string.IsNullOrWhiteSpace(text) ? 0 : text.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

        // 3. Підрахунок символів (Вимога №2.3)
        public int CountCharacters(string text, bool ignoreWhitespace = true)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            return ignoreWhitespace ? text.Replace(" ", "").Length : text.Length;
        }

        // 4. Нормалізація: видалення зайвих пробілів та Trim
        public string Normalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            return string.Join(" ", text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).Trim();
        }

        // 5. Перевірка на паліндром
        public bool IsPalindrome(string text, bool ignoreCase = true, bool ignoreSpaces = true)
        {
            if (string.IsNullOrEmpty(text)) return false;

            string processed = text;
            if (ignoreSpaces) processed = processed.Replace(" ", "");
            if (ignoreCase) processed = processed.ToLower();

            string reversed = Reverse(processed);
            return processed == reversed;
        }

        // 6. Множинна заміна (Вимога №2.6)
        public string ReplaceMultiple(string text, Dictionary<string, string> replacements)
        {
            if (string.IsNullOrEmpty(text) || replacements == null) return text;
            StringBuilder sb = new StringBuilder(text);
            foreach (var replacement in replacements)
            {
                sb.Replace(replacement.Key, replacement.Value);
            }
            return sb.ToString();
        }

        // 7. Розбиття на речення (Вимога №2.7)
        public string[] SplitIntoSentences(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return Array.Empty<string>();
            return text.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(s => s.Trim())
                       .ToArray();
        }

        // 8. ГЕНЕРАЦІЯ ПОВНОГО ЗВІТУ ГРУПИ (Вимога №2.8)
        public string BuildGroupReport(StudentGroup group)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("==================================================");
            sb.AppendLine($"ЗВІТ ГРУПИ: {group.GroupName}");
            sb.AppendLine($"Дата: {DateTime.Now:G}");
            sb.AppendLine("==================================================");

            var students = group.GetAllStudents();
            if (students.Count == 0)
            {
                sb.AppendLine("Група порожня.");
            }
            else
            {
                foreach (var student in students)
                {
                    // Використовуємо StringBuilder-метод студента
                    sb.AppendLine(student.GetFormattedInfo(true));
                }
            }

            sb.AppendLine("==================================================");
            sb.AppendLine($"Всього студентів: {students.Count}");
            sb.AppendLine($"Середній бал групи: {group.AverageGroupGrade:F2}");
            return sb.ToString();
        }

        // 9. Порівняння продуктивності string vs StringBuilder
        public string ComparePerformance(int iterations)
        {
            Stopwatch sw = new Stopwatch();

            // Тест String
            sw.Start();
            string s = "";
            for (int i = 0; i < iterations; i++) s += "test";
            sw.Stop();
            long stringTime = sw.ElapsedMilliseconds;

            // Тест StringBuilder
            sw.Restart();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < iterations; i++) sb.Append("test");
            sw.Stop();
            long sbTime = sw.ElapsedMilliseconds;

            return $"Ітерацій: {iterations}\nString: {stringTime} ms\nStringBuilder: {sbTime} ms";
        }

        // 10. АНАЛІЗ НАСТРОЮ (Варіант 2)
        public string AnalyzeSentiment(StudentGroup group)
        {
            int positive = 0, negative = 0;
            string[] posWords = { "відмінно", "успіх", "задоволений", "добре" };
            string[] negWords = { "погано", "проблема", "заборгованість", "важко" };

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== РЕЗУЛЬТАТИ АНАЛІЗУ НАСТРОЮ ===");

            foreach (var student in group.GetAllStudents())
            {
                string notes = student.Notes.ToLower();
                positive += posWords.Count(w => notes.Contains(w));
                negative += negWords.Count(w => notes.Contains(w));
            }

            sb.AppendLine($"Позитивні маркери: {positive}");
            sb.AppendLine($"Негативні маркери: {negative}");
            sb.AppendLine(positive >= negative ? "Стан групи: Позитивний" : "Стан групи: Потребує уваги");

            return sb.ToString();
        }
    }
}