using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna2
{
    public class PortMatrix
    {
        // Двовимірний масив об'єктів Port розміром 16x16
        private Port[,] _matrix = new Port[16, 16];

        public PortMatrix()
        {
            int counter = 0;
            // Ініціалізація матриці (вкладені цикли для двовимірного масиву)
            for (int r = 0; r < 16; r++)
            {
                for (int c = 0; c < 16; c++)
                {
                    _matrix[r, c] = new Port(counter++, $"Dev-{r}-{c}");
                }
            }
        }

        // Метод для відкриття порту за координатами з обробкою винятків
        public void OpenPort(int row, int col)
        {
            if (row < 0 || row >= 16 || col < 0 || col >= 16)
                throw new IndexOutOfRangeException("Координати за межами матриці 16x16");

            _matrix[row, col].Open();
        }

        // Метод запису даних у буфер порту
        public void WriteToPort(int row, int col, byte[] data)
        {
            if (data.Length > 64)
                throw new ArgumentException("Дані перевищують розмір буфера порту (64 байти)");

            Array.Copy(data, _matrix[row, col].DataBuffer, data.Length);
        }

        // Перейменовано для успішної інтеграції з StudentGroup
        public string GetStatusReport()
        {
            // Обов'язкове використання StringBuilder замість конкатенації
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== Поточний стан апаратних портів (16x16) ===");

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    // [O] - порт відкритий, [.] - закритий
                    sb.Append(_matrix[i, j].IsOpen ? "[O]" : "[.]");
                }
                sb.AppendLine(); // Перехід на новий рядок через StringBuilder 
            }
            return sb.ToString();
        }

        // Пошук у двовимірній матриці для додаткових балів
        public List<Port> FindOpenPorts()
        {
            List<Port> openPorts = new List<Port>();
            foreach (var port in _matrix)
            {
                if (port.IsOpen) openPorts.Add(port);
            }
            return openPorts;
        }
    }
}
