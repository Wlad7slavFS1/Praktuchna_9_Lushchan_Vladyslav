using Praktychna1;
using System;
using System.Text;
using System.Linq;
using System.IO;

class Program
{
    static StudentGroup myGroup = new StudentGroup
    {
        GroupName = "RPZ-21",
        Specialization = "Software Engineering",
        Course = 3
    };

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // Ініціалізація інструментів ПР №3
        TextProcessor tp = new TextProcessor();
        AdvancedLogger advLogger = new AdvancedLogger();

        advLogger.Log("Info", "Програма запущена.");

        while (true)
        {
            StringBuilder menuBuilder = new StringBuilder();
            menuBuilder.AppendLine("\n--- РОБОТА З ТЕКСТОМ ТА ЗВІТАМИ (ПР №3) ---");
            menuBuilder.AppendLine("1.  Додати студента");
            menuBuilder.AppendLine("2.  Видалити студента");
            menuBuilder.AppendLine("3.  Вивести всіх студентів");
            menuBuilder.AppendLine("4.  Пошук студента");
            menuBuilder.AppendLine("5.  Редагування даних студента");
            menuBuilder.AppendLine("6.  Відмінники / ті, хто має < 6 балів");
            menuBuilder.AppendLine("7.  Статистика групи");
            menuBuilder.AppendLine("8.  Зберегти дані (JSON)");
            menuBuilder.AppendLine("9.  Завантажити дані (JSON)");
            menuBuilder.AppendLine("10. Пошук за фрагментом ПІБ");
            menuBuilder.AppendLine("11. Згенерувати повний звіт групи (StringBuilder)");
            menuBuilder.AppendLine("12. Нормалізувати нотатки всіх студентів");
            menuBuilder.AppendLine("13. Перевірити паліндроми в нотатках");
            menuBuilder.AppendLine("14. Експорт групи у CSV");
            menuBuilder.AppendLine("15. Імпорт студентів з текстового блоку");
            menuBuilder.AppendLine("16. Переглянути логи системи");
            menuBuilder.AppendLine("17. Порівняти продуктивність string vs StringBuilder");
            menuBuilder.AppendLine("18. Обробка тексту (реверс, підрахунок тощо)");
            menuBuilder.AppendLine("19. Аналіз настрою групи (Варіант 2)");
            menuBuilder.AppendLine("0.  Вийти");
            menuBuilder.Append("Виберіть дію: ");

            Console.Write(menuBuilder.ToString());
            string choice = Console.ReadLine();
            if (choice == "0") break;

            switch (choice)
            {
                case "1": AddStudent(advLogger); break;
                case "2": RemoveStudent(advLogger); break;
                case "3": ShowAllStudents(); break;
                case "4": SearchStudent(); break;
                case "5": EditStudent(advLogger); break;
                case "6": ShowPerformanceCategories(); break;
                case "7": Console.WriteLine(myGroup.GetGroupStatistics()); break;
                case "8": myGroup.SaveToFile("students.json"); advLogger.Log("Info", "Дані збережено."); break;
                case "9": myGroup.LoadFromFile("students.json"); advLogger.Log("Info", "Дані завантажено."); break;
                case "10":
                    Console.Write("Введіть фрагмент імені: ");
                    Console.WriteLine(myGroup.SearchByNameFragment(Console.ReadLine()));
                    break;
                case "11": Console.WriteLine(tp.BuildGroupReport(myGroup)); break;
                case "12":
                    foreach (var s in myGroup.GetAllStudents()) s.Notes = tp.Normalize(s.Notes);
                    Console.WriteLine("Всі нотатки нормалізовано.");
                    break;
                case "13":
                    foreach (var s in myGroup.GetAllStudents())
                        if (tp.IsPalindrome(s.Notes)) Console.WriteLine($"Паліндром у {s.FullName}: {s.Notes}");
                    break;
                case "14":
                    File.WriteAllText("export.csv", myGroup.ExportToCsv());
                    Console.WriteLine("Дані експортовано в export.csv");
                    break;
                case "15":
                    Console.WriteLine("Введіть імена (завершіть порожнім рядком):");
                    StringBuilder importSb = new StringBuilder();
                    string line;
                    while (!string.IsNullOrWhiteSpace(line = Console.ReadLine())) importSb.AppendLine(line);
                    myGroup.ImportStudentsFromText(importSb.ToString());
                    break;
                case "16": Console.WriteLine(advLogger.GetFullLog()); break;
                case "17":
                    Console.Write("Кількість ітерацій: ");
                    if (int.TryParse(Console.ReadLine(), out int iter)) Console.WriteLine(tp.ComparePerformance(iter));
                    break;
                case "18":
                    Console.Write("Введіть текст: ");
                    string txt = Console.ReadLine();
                    Console.WriteLine($"Реверс: {tp.Reverse(txt)}, Слів: {tp.CountWords(txt)}");
                    break;
                case "19": Console.WriteLine(tp.AnalyzeSentiment(myGroup)); break;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }
    }

    static void AddStudent(AdvancedLogger logger)
    {
        try
        {
            Console.Write("ПІБ (Прізвище Ім'я По батькові): "); string name = Console.ReadLine();
            Console.Write("№ заліковки (8 цифр): "); string id = Console.ReadLine();
            Console.Write("Нотатки: "); string n = Console.ReadLine();
            myGroup.AddStudent(new Student { FullName = name, RecordBookNumber = id, Notes = n, DateOfBirth = DateTime.Now.AddYears(-18) });
            logger.Log("Info", $"Додано студента: {name}");
        }
        catch (Exception e) { Console.WriteLine($"Помилка: {e.Message}"); }
    }

    static void RemoveStudent(AdvancedLogger logger)
    {
        Console.Write("№ заліковки: "); string id = Console.ReadLine();
        myGroup.RemoveStudent(id);
        logger.Log("Warning", $"Видалено ID: {id}");
    }

    static void ShowAllStudents()
    {
        foreach (var s in myGroup.GetAllStudents()) Console.WriteLine(s.GetFormattedInfo(true));
    }

    static void SearchStudent()
    {
        Console.Write("Ключове слово: "); string k = Console.ReadLine();
        foreach (var s in myGroup.GetAllStudents().Where(x => x.ContainsKeyword(k)))
            Console.WriteLine(s.GetFormattedInfo());
    }

    static void EditStudent(AdvancedLogger logger)
    {
        Console.Write("№ заліковки: "); string id = Console.ReadLine();
        var s = myGroup.GetAllStudents().FirstOrDefault(x => x.RecordBookNumber == id);
        if (s != null) { Console.Write("Нові нотатки: "); s.Notes = Console.ReadLine(); logger.Log("Info", $"Оновлено {s.FullName}"); }
    }

    static void ShowPerformanceCategories()
    {
        var excellent = myGroup.GetAllStudents().Where(s => s.AverageGrade >= 90);
        var failing = myGroup.GetAllStudents().Where(s => s.AverageGrade < 60);
        Console.WriteLine("Відмінники: " + string.Join(", ", excellent.Select(x => x.FullName)));
        Console.WriteLine("Низький бал: " + string.Join(", ", failing.Select(x => x.FullName)));
    }
}