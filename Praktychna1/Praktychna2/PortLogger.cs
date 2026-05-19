using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna2
{
    public class PortLogger
    {
        // Поле для накопичення логів
        private readonly StringBuilder _logHistory = new StringBuilder();

        // Метод для запису операції
        public void Log(int portNumber, string operation, string status)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Використовуємо AppendFormat замість конкатенації (+)
            _logHistory.AppendFormat("[{0}] Порт №{1}: {2} | Статус: {3}\n",
                timestamp, portNumber, operation, status);
        }

        // Метод для отримання всього логу
        public string GetFullLog()
        {
            if (_logHistory.Length == 0)
                return "Історія операцій порожня.";

            return _logHistory.ToString();
        }

        // Очищення логів
        public void Clear() => _logHistory.Clear();
    }
}
