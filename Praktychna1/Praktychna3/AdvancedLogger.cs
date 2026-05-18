using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna3
{
    public class AdvancedLogger
    {
        private StringBuilder _logData = new StringBuilder();

        public void Log(string level, string message)
        {
            string logEntry = $"[{DateTime.Now:HH:mm:ss}] [{level.ToUpper()}] {message}";
            _logData.AppendLine(logEntry);
            Console.WriteLine(logEntry); // Вивід у консоль для наочності
        }

        public string GetFullLog() => _logData.ToString();

        public void SaveToFile(string path) => File.WriteAllText(path, _logData.ToString());
    }
}