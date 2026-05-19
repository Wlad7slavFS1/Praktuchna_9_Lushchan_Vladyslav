using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        // Додай ці методи всередину класу FileManager:

        public void SaveToJson<T>(T data, string filePath)
        {
            // Налаштування для "красивого" вигляду JSON
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, jsonString);
        }

        public T LoadFromJson<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не знайдено");

            string jsonString = File.ReadAllText(filePath);
            // Десеріалізація об'єкта назад у тип T 
            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}