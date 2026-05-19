using Praktychna1.Praktychna1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna7
{
    public static class PerformanceTest
    {
        public static void Run(int count = 100000)
        {
            Console.WriteLine($"--- Тестування продуктивності ({count} елементів) ---");

            // Тест для КЛАСІВ
            GC.Collect();
            long memoryBeforeClass = GC.GetTotalMemory(true);
            Stopwatch sw = Stopwatch.StartNew();

            var classArray = new Student[count];
            for (int i = 0; i < count; i++)
                classArray[i] = new Student($"Student {i}", DateTime.Now, $"{i:D8}", "email@test.com", 0, Student.StudentStatus.Active);

            sw.Stop();
            long memoryAfterClass = GC.GetTotalMemory(true);
            Console.WriteLine($"КЛАСИ:   Час: {sw.ElapsedMilliseconds} мс | Пам'ять: {(memoryAfterClass - memoryBeforeClass) / 1024} KB");

            // Тест для СТРУКТУР
            GC.Collect();
            long memoryBeforeStruct = GC.GetTotalMemory(true);
            sw.Restart();

            var structArray = new StudentRecord[count];
            for (int i = 0; i < count; i++)
                structArray[i] = new StudentRecord($"Student {i}", $"{i:D8}", 0);

            sw.Stop();
            long memoryAfterStruct = GC.GetTotalMemory(true);
            Console.WriteLine($"СТРУКТУРИ: Час: {sw.ElapsedMilliseconds} мс | Пам'ять: {(memoryAfterStruct - memoryBeforeStruct) / 1024} KB");
        }
    }
}
