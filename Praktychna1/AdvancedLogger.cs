using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1
{
    internal class AdvancedLogger
    {
        private StringBuilder _logBuffer = new StringBuilder();

        public void Log(string level, string message) =>
            _logBuffer.AppendLine($"[{DateTime.Now:T}] [{level.ToUpper()}] {message}");

        public string GetLogsByLevel(string level) =>
            string.Join("\n", _logBuffer.ToString().Split('\n').Where(l => l.Contains($"[{level.ToUpper()}]")));

        public void SaveToFile(string path) => File.WriteAllText(path, _logBuffer.ToString());

        public void Clear() => _logBuffer.Clear();

        public string GetFullLog() => _logBuffer.ToString();
    }
}