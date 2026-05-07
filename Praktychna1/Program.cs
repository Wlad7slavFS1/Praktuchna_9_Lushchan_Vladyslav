using Praktychna1;
using System;
using System.Text;

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
        // Встановлення кодування для коректного виводу кирилиці
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        while (true)
        {
            // Використання StringBuilder для формування тексту меню (вимога №2)
            StringBuilder menuBuilder = new StringBuilder();
            menuBuilder.AppendLine("\n--- СИСТЕМА УПРАВЛІННЯ ГРУПОЮ ТА ЛАБОРАТОРІЄЮ (ПР №2) ---");
            menuBuilder.AppendLine("1.  Додати студента");
            menuBuilder.AppendLine("2.  Вивести список студентів (детально)");
            menuBuilder.AppendLine("3.  Видалити студента");
            menuBuilder.AppendLine("4.  Додати оцінку за предмет (Journal)");
            menuBuilder.AppendLine("5.  Призначити студенту робоче місце (Порт)");
            menuBuilder.AppendLine("6.  Симулювати виконання лабораторної роботи");
            menuBuilder.AppendLine("7.  Показати стан матриці портів (16x16)");
            menuBuilder.AppendLine("8.  Переглянути логи системи (StringBuilder)");
            menuBuilder.AppendLine("9.  Зберегти дані в JSON");
            menuBuilder.AppendLine("10. Завантажити дані з JSON");
            menuBuilder.AppendLine("11. Вивести статистику групи");
            menuBuilder.AppendLine("12. Знайти відкриті порти");
            menuBuilder.AppendLine("0.  Вийти");
            menuBuilder.Append("Виберіть дію: ");

            Console.Write(menuBuilder.ToString());
            string choice = Console.ReadLine();
            if (choice == "0") break;

            switch (choice)
            {
                case "1": AddStudent(); break;
                case "2": ShowAllStudents(); break;
                case "3": RemoveStudent(); break;
                case "4": AddSubjectGrade(); break;
                case "5": AssignToPort(); break;
                case "6": RunLabSimulation(); break;
                case "7": Console.WriteLine(myGroup.GetPortMap()); break;
                case "8": Console.WriteLine(myGroup.GetSystemLogs()); break;
                case "9": myGroup.SaveToFile("university_v2.json"); Console.WriteLine("Збережено!"); break;
                case "10": myGroup.LoadFromFile("university_v2.json"); Console.WriteLine("Завантажено!"); break;
                case "11": Console.WriteLine(myGroup.GetGroupStatistics()); break;
                case "12": FindOpenPorts(); break;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }
    }

    // --- Методи реалізації пунктів меню ---

    static void AddStudent()
    {
        try
        {
            Console.Write("ПІБ (мін. 5 симв.): ");
            string name = Console.ReadLine();
            Console.Write("Номер заліковки (8 цифр): ");
            string id = Console.ReadLine();

            myGroup.AddStudent(new Student
            {
                FullName = name,
                RecordBookNumber = id,
                DateOfBirth = new DateTime(2005, 5, 20), // Приклад
                Status = StudentStatus.Active
            });
            Console.WriteLine("Студента додано успішно!");
        }
        catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
    }

    static void AssignToPort()
    {
        try
        {
            Console.Write("Номер заліковки: ");
            string id = Console.ReadLine();
            Console.Write("Ряд матриці (0-15): ");
            int row = int.Parse(Console.ReadLine());
            Console.Write("Стовпець матриці (0-15): ");
            int col = int.Parse(Console.ReadLine());

            myGroup.AssignStudentToPort(id, row, col);
            Console.WriteLine("Місце успішно закріплено!");
        }
        catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
    }

    static void RunLabSimulation()
    {
        try
        {
            Console.Write("Номер заліковки: ");
            string id = Console.ReadLine();
            Console.Write("Номер лаби (0-9): ");
            int labNum = int.Parse(Console.ReadLine());
            Console.Write("Оцінка (0-100): ");
            byte grade = byte.Parse(Console.ReadLine());

            myGroup.SimulateLab(id, labNum, grade);
            Console.WriteLine("Симуляція завершена, дані внесено.");
        }
        catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
    }

    static void ShowAllStudents()
    {
        var students = myGroup.GetAllStudents();
        if (!students.Any()) Console.WriteLine("Група порожня.");
        else foreach (var s in students) s.ShowDetailedInfo();
    }

    static void RemoveStudent()
    {
        Console.Write("Номер заліковки для видалення: ");
        string id = Console.ReadLine();
        myGroup.RemoveStudent(id);
        Console.WriteLine("Запит на видалення оброблено.");
    }

    static void AddSubjectGrade()
    {
        Console.Write("Номер заліковки: ");
        string id = Console.ReadLine();
        var student = myGroup.GetAllStudents().FirstOrDefault(s => s.RecordBookNumber == id);
        if (student != null)
        {
            Console.Write("Назва предмета: ");
            string sub = Console.ReadLine();
            Console.Write("Оцінка: ");
            if (double.TryParse(Console.ReadLine(), out double g))
                student.Journal.SetGrade(sub, g);
        }
    }

    static void FindOpenPorts()
    {
        // Приклад пошуку в двовимірному масиві для високої оцінки
        Console.WriteLine("Функція пошуку відкритих портів у розробці або реалізована в PortMatrix.");
    }
}