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
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,                         // Читабельний вигляд
            PropertyNameCaseInsensitive = true,           // Ігнорування регістру при читанні
            AllowTrailingCommas = true                    // Дозвіл коми в кінці списку
        };

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

        // 2. Оновлений метод SaveToJson з обробкою винятків
        public void SaveToJson<T>(T data, string filePath)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(data, _options);
                File.WriteAllText(filePath, jsonString);
            }
            catch (JsonException ex)
            {
                // Тут ми "перехоплюємо" помилку JSON і кидаємо зрозуміле повідомлення
                throw new Exception($"Помилка серіалізації у JSON: {ex.Message}");
            }
            catch (IOException ex)
            {
                throw new Exception($"Помилка доступу до файлу: {ex.Message}");
            }
        }

        // 3. Оновлений метод LoadFromJson з обробкою винятків
        public T LoadFromJson<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл {filePath} не знайдено");

            try
            {
                string jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(jsonString, _options);
            }
            catch (JsonException ex)
            {
                // Якщо файл JSON "битий" або порожній, спрацює цей блок
                throw new Exception($"Помилка формату JSON у файлі {filePath}: {ex.Message}");
            }
        }

        public void ExportToCsv(IEnumerable<string[]> rows, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (var row in rows)
                {
                    // Об'єднуємо елементи рядка через крапку з комою (стандарт для Excel в Україні)
                    writer.WriteLine(string.Join(";", row));
                }
            }
        }

        public void CreateBackup(string sourcePath)
        {
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("Початковий файл для бекапу не знайдено.");

            if (!Directory.Exists("Backups"))
                Directory.CreateDirectory("Backups");

            string fileName = Path.GetFileNameWithoutExtension(sourcePath);
            string extension = Path.GetExtension(sourcePath);
            // Додаємо дату і час, щоб копії мали унікальні імена
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string destPath = Path.Combine("Backups", $"{fileName}_{timestamp}{extension}");

            File.Copy(sourcePath, destPath);
            Console.WriteLine($"Резервну копію створено: {destPath}");
        }

        public void CleanOldBackups(int daysOld)
        {
            if (!Directory.Exists("Backups")) return;

            DirectoryInfo dir = new DirectoryInfo("Backups");
            // Отримуємо всі файли в папці бекапів
            foreach (FileInfo file in dir.GetFiles())
            {
                // Якщо файл створено раніше, ніж daysOld днів тому
                if (file.CreationTime < DateTime.Now.AddDays(-daysOld))
                {
                    file.Delete();
                    Console.WriteLine($"Видалено старий бекап: {file.Name}");
                }
            }
        }

        public void ShowBackupList()
        {
            if (!Directory.Exists("Backups")) return;

            string[] files = Directory.GetFiles("Backups");
            Console.WriteLine("Список доступних резервних копій:");
            foreach (var file in files)
            {
                Console.WriteLine($"- {Path.GetFileName(file)}");
            }
        }
    }
}