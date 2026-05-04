using Praktychna1;
using System;

class Program
{
    static void Main()
    {
        StudentGroup myGroup = new StudentGroup { GroupName = "RPZ-21", Specialization = "Software Engineering", Course = 3 };

        while (true)
        {
            Console.WriteLine("\n--- МЕНЮ УПРАВЛІННЯ ГРУПОЮ ---");
            Console.WriteLine("1. Додати студента");
            Console.WriteLine("2. Вивести всіх (з оцінками)");
            Console.WriteLine("3. Зберегти у файл");
            Console.WriteLine("4. Додати оцінку за предмет");
            Console.WriteLine("0. Вийти");

            string choice = Console.ReadLine();
            if (choice == "0") break;

            switch (choice)
            {
                case "1":
                    try
                    {
                        Console.Write("Введіть ПІБ: ");
                        string name = Console.ReadLine();
                        Console.Write("Номер заліковки (8 цифр): ");
                        string id = Console.ReadLine();

                        myGroup.AddStudent(new Student
                        {
                            FullName = name,
                            RecordBookNumber = id,
                            DateOfBirth = new DateTime(2005, 1, 1),
                            Status = StudentStatus.Active
                        });
                        Console.WriteLine("Студента додано успішно!");
                    }
                    catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
                    break;

                case "2":
                    var all = myGroup.GetAllStudents();
                    if (!all.Any()) Console.WriteLine("Група порожня.");
                    foreach (var s in all) s.ShowDetailedInfo();
                    break;

                case "3":
                    myGroup.SaveToFile("group.json");
                    Console.WriteLine("Дані збережено у файл group.json!");
                    break;

                case "4":
                    Console.Write("Введіть номер заліковки студента: ");
                    string searchId = Console.ReadLine();
                    var student = myGroup.GetAllStudents().FirstOrDefault(s => s.RecordBookNumber == searchId);

                    if (student != null)
                    {
                        Console.Write("Предмет: ");
                        string subject = Console.ReadLine();
                        Console.Write("Оцінка (0-100): ");
                        if (double.TryParse(Console.ReadLine(), out double grade))
                        {
                            try
                            {
                                student.Journal.SetGrade(subject, grade);
                                Console.WriteLine("Оцінку додано!");
                            }
                            catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
                        }
                    }
                    else Console.WriteLine("Студента не знайдено.");
                    break;
            }
        }
    }
}