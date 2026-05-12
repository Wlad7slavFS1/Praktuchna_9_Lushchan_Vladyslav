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

        // 3. Нормалізація: видалення зайвих пробілів та Trim
        public string Normalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            // Split видаляє порожні елементи (зайві пробіли), Join з'єднує одним пробілом
            return string.Join(" ", text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).Trim();
        }

        // 4. Перевірка на паліндром
        public bool IsPalindrome(string text, bool ignoreCase = true, bool ignoreSpaces = true)
        {
            if (string.IsNullOrEmpty(text)) return false;

            string processed = text;
            if (ignoreSpaces) processed = processed.Replace(" ", "");
            if (ignoreCase) processed = processed.ToLower();

            string reversed = Reverse(processed);
            return processed == reversed;
        }

        // 5. Порівняння продуктивності string vs StringBuilder
        public string ComparePerformance(int iterations)
        {
            Stopwatch sw = new Stopwatch();

            // Тест String (конкатенація створює новий об'єкт щоразу)
            sw.Start();
            string s = "";
            for (int i = 0; i < iterations; i++) s += "test";
            sw.Stop();
            long stringTime = sw.ElapsedMilliseconds;

            // Тест StringBuilder (змінює існуючий буфер)
            sw.Restart();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < iterations; i++) sb.Append("test");
            sw.Stop();
            long sbTime = sw.ElapsedMilliseconds;

            return $"Ітерацій: {iterations}\nString: {stringTime} ms\nStringBuilder: {sbTime} ms";
        }
    }
}