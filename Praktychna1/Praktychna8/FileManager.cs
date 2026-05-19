using Praktychna1.Praktychna1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna8
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
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(content);
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("Помилка: Відсутні права доступу для запису файлу.");
            }
            catch (IOException ex)
            {
                throw new Exception($"Помилка введення-виведення при записі: {ex.Message}");
            }
        }

        public string ReadFromText(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Файл {filePath} не знайдено.");

                using (StreamReader reader = new StreamReader(filePath))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (IOException ex)
            {
                throw new Exception($"Не вдалося прочитати файл: {ex.Message}");
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
                throw new InvalidFileFormatException($"Помилка серіалізації: {ex.Message}");
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("Відмовлено у доступі. Можливо, файл відкритий в іншій програмі.");
            }
            catch (IOException ex)
            {
                throw new Exception($"Помилка дискової системи: {ex.Message}");
            }
        }

        public T LoadFromJson<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Файл за шляхом {filePath} не існує.");

                string jsonString = File.ReadAllText(filePath);

                // Перевірка на порожній файл перед десеріалізацією
                if (string.IsNullOrWhiteSpace(jsonString))
                    throw new InvalidFileFormatException("Файл порожній. Завантаження неможливе.");

                return JsonSerializer.Deserialize<T>(jsonString, _options);
            }
            catch (JsonException ex)
            {
                // Використовуємо твій власний виняток для помилок формату 
                throw new InvalidFileFormatException($"Некоректний формат JSON: {ex.Message}");
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("Немає дозволу на читання цього файлу.");
            }
        }

        public void ExportToCsv(IEnumerable<string[]> rows, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    foreach (var row in rows)
                    {
                        writer.WriteLine(string.Join(";", row));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Експорт у CSV провалено: {ex.Message}");
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

        private void ValidatePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Шлях до файлу не може бути порожнім.");

            if (filePath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                throw new InvalidFileFormatException("Шлях містить недопустимі символи.");
        }

        public void TestExceptionHandling()
        {
            Console.WriteLine("Тестування обробки винятків...");
            try
            {
                LoadFromJson<StudentGroup>("non_existent_file.json");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Успішно перехоплено: {ex.Message}");
            }
        }

        public void LogAction(string message)
        {
            string logDir = "Logs";
            string logPath = Path.Combine(logDir, "user_actions.log");

            if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);

            // Ротація: якщо файл > 100 КБ, архівуємо його
            FileInfo logFile = new FileInfo(logPath);
            if (logFile.Exists && logFile.Length > 100 * 1024)
            {
                string archivePath = Path.Combine(logDir, $"log_archive_{DateTime.Now:yyyyMMdd_HHmmss}.log");
                File.Move(logPath, archivePath);
            }

            using (StreamWriter sw = File.AppendText(logPath))
            {
                sw.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
            }
        }
    }
}