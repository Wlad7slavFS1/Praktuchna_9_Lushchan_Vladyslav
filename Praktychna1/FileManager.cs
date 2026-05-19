using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1
{
    internal class FileManager
    {
        // Метод для збереження контенту у текстовий файл за допомогою StreamWriter
        public void SaveToText(string content, string filePath)
        {
            // Використання using гарантує закриття потоку
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(content);
            }
        }

        // Метод для зчитування з текстового файлу за допомогою StreamReader
        public string ReadFromText(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл за шляхом {filePath} не знайдено.");

            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }
    }
}