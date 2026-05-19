using Praktychna1;
using Praktychna1.Praktychna1;
using Praktychna1.Praktychna4;
using Praktychna1.Praktychna5;
using Praktychna1.Praktychna6;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using static Praktychna1.Praktychna1.Student;
using Complex = Praktychna1.Praktychna4.Complex;
using Vector = Praktychna1.Praktychna4.Vector;
using Teacher = Praktychna1.Praktychna6.Teacher;
using Praktychna1.Praktychna7; // Вкажи простір імен саме для ПР №6

class Program
{
    static StudentGroup myGroup = new StudentGroup
    {
        GroupName = "К-321",
        Specialization = "Software Engineering",
        Course = 3
    };

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        while (true)
        {
            StringBuilder menuBuilder = new StringBuilder();
            menuBuilder.AppendLine("\n--- СИСТЕМА УПРАВЛІННЯ ГРУПОЮ (ПР №6) ---");
            menuBuilder.AppendLine("1.  Додати студента");
            menuBuilder.AppendLine("2.  Видалити студента");
            menuBuilder.AppendLine("3.  Вивести всіх студентів");
            menuBuilder.AppendLine("4.  Пошук за ключовим словом");
            menuBuilder.AppendLine("7.  Статистика групи");
            menuBuilder.AppendLine("8.  Зберегти дані (JSON)");
            menuBuilder.AppendLine("9.  Завантажити дані (JSON)");
            menuBuilder.AppendLine("10. Пошук за фрагментом ПІБ");

            // --- Відновлені пункти (ПР №3) ---
            menuBuilder.AppendLine("11. Згенерувати повний звіт (Statistics + All Students)");
            menuBuilder.AppendLine("12. Нормалізувати нотатки (видалити зайві пробіли)");
            menuBuilder.AppendLine("13. Перевірити паліндроми в нотатках");
            menuBuilder.AppendLine("14. Експорт групи у CSV");
            menuBuilder.AppendLine("15. Імпорт студентів з тексту");
            menuBuilder.AppendLine("16. Переглянути логи системи");
            menuBuilder.AppendLine("17. Порівняти продуктивність string vs StringBuilder");
            menuBuilder.AppendLine("18. Реверс тексту та підрахунок слів");

            // --- Нові пункти (ПР №4)  ---
            menuBuilder.AppendLine("19. Порівняти двох студентів (>, <, ==)");
            menuBuilder.AppendLine("20. Об’єднати дві групи (+)");
            menuBuilder.AppendLine("21. Продемонструвати роботу з класом Vector");
            menuBuilder.AppendLine("22. Продемонструвати роботу з GradePoint");
            menuBuilder.AppendLine("23. Знайти найкращого студента (BestStudent)");
            menuBuilder.AppendLine("24. Продемонструвати індивідуальний варіант (Complex)");
            // --- НОВІ ПУНКТИ ПР №5 (Вимога методички) ---
            menuBuilder.AppendLine("25. Додати специфічного студента (Відмінник/Іноземець/Працюючий/Випускник)");
            menuBuilder.AppendLine("26. Розрахувати та вивести стипендію для всіх (GetTotalScholarship)");
            menuBuilder.AppendLine("27. Показати інформацію про конкретний тип студентів (Generic)");
            menuBuilder.AppendLine("28. Тестування ієрархії та викликів base/override (Метод Enroll)");
            // --- НОВІ ПУНКТИ ПР №6 (Поліморфізм) ---
            menuBuilder.AppendLine("29. Розрахувати загальний фонд зарплати (Staff Salary)");
            menuBuilder.AppendLine("31. Додати нову фігуру (Circle/Rectangle/Triangle)");
            menuBuilder.AppendLine("32. Вивести всі фігури (Поліморфізм)");
            menuBuilder.AppendLine("33. Розрахувати загальну площу всіх фігур");
            menuBuilder.AppendLine("34. Змінити розмір всіх фігур (IResizable)");
            menuBuilder.AppendLine("35. Намалювати всі фігури (IDrawable)");
            menuBuilder.AppendLine("36. Інформація через IPrintable");
            menuBuilder.AppendLine("37. Демонстрація динамічного зв’язування");
            // --- НОВІ ПУНКТИ ПР №7 (Структури) ---
            menuBuilder.AppendLine("38. Продемонструвати роботу зі структурами (Point, GradeRecord)");
            menuBuilder.AppendLine("39. Порівняти продуктивність struct vs class");
            menuBuilder.AppendLine("40. Перетворити студента у StudentRecord");
            menuBuilder.AppendLine("41. Показати історію оцінок через структури");
            menuBuilder.AppendLine("42. Тестування Equals та IEquatable<T> (Point, DateRange)");
            menuBuilder.AppendLine("43. Оптимізація зберігання даних групи");
            menuBuilder.AppendLine("0.  Вийти");
            menuBuilder.Append("Виберіть дію: ");

            Console.Write(menuBuilder.ToString());
            string choice = Console.ReadLine();
            if (choice == "0") break;

            switch (choice)
            {
                case "1": AddStudent(); break;
                case "2": RemoveStudent(); break;
                case "3": ShowAllStudents(); break;
                case "4": SearchStudent(); break;
                case "7": Console.WriteLine(myGroup.GetGroupStatistics()); break;
                case "8": myGroup.SaveToFile("students.json"); Console.WriteLine("Збережено."); break;
                case "9": myGroup.LoadFromFile("students.json"); Console.WriteLine("Завантажено."); break;
                case "10":
                    Console.Write("Введіть фрагмент: ");
                    myGroup.SearchByNameFragment(Console.ReadLine());
                    break;
                case "11":
                    Console.WriteLine(myGroup.GetGroupStatistics());
                    ShowAllStudents();
                    break;
                case "12":
                    foreach (var s in myGroup.GetAllStudents()) s.Notes = s.Notes?.Trim();
                    Console.WriteLine("Нотатки нормалізовано.");
                    break;
                case "13":
                    foreach (var s in myGroup.GetAllStudents())
                    {
                        string clean = new string(s.Notes?.Where(char.IsLetterOrDigit).ToArray()).ToLower();
                        if (!string.IsNullOrEmpty(clean) && clean == new string(clean.Reverse().ToArray()))
                            Console.WriteLine($"Паліндром у {s.FullName}: {s.Notes}");
                    }
                    break;
                case "14":
                    myGroup.ExportToCsv("export.csv");
                    Console.WriteLine("Експортовано в export.csv");
                    break;
                case "15":
                    Console.WriteLine("Введіть імена (через кому):");
                    myGroup.ImportStudentsFromText(Console.ReadLine());
                    break;
                case "16": Console.WriteLine(myGroup.GetSystemLogs()); break;
                case "17":
                    var sw = System.Diagnostics.Stopwatch.StartNew();
                    string st = ""; for (int i = 0; i < 5000; i++) st += i;
                    sw.Stop(); long t1 = sw.ElapsedTicks;
                    sw.Restart();
                    StringBuilder sbb = new StringBuilder(); for (int i = 0; i < 5000; i++) sbb.Append(i);
                    sw.Stop(); long t2 = sw.ElapsedTicks;
                    Console.WriteLine($"String: {t1} | StringBuilder: {t2}");
                    break;
                case "18":
                    Console.Write("Текст: "); string t = Console.ReadLine() ?? "";
                    Console.WriteLine($"Реверс: {new string(t.Reverse().ToArray())}, Слів: {t.Split(' ').Length}");
                    break;
                case "19": CompareTwoStudents(); break;
                case "20": MergeWithAnotherGroup(); break;
                case "21": TestVector(); break;
                case "22": TestGradePoint(); break;
                case "23":
                    var best = myGroup.BestStudent();
                    Console.WriteLine(best != null ? $"Найкращий: {best.GetInfo()}" : "Порожньо.");
                    break;
                case "24": TestComplexNumbers(); break;
                case "25": AddSpecificStudent(); break; // Новий метод
                case "26": ShowScholarshipReport(); break; // Новий метод
                case "27": ShowSpecificTypeMembers(); break; // Новий метод
                case "28": TestHierarchyEnrollment(); break; // Новий метод
                case "29":
                    // Створюємо список працівників (можна також зберігати його в класі StudentGroup, якщо це доречно)
                    List<Employee> universityStaff = new List<Employee>
    {
        new Developer("Владислав Лущан", 40000, "C#"),
        new Teacher("Олександр Петрович", 15000, 80),
        new Developer("Анна Сидоренко", 35000, "Java"),
        new Teacher("Марія Іванівна", 12000, 120)
    };

                    decimal totalFund = 0;
                    Console.WriteLine("\n--- ВІДОМІСТЬ ПО ЗАРПЛАТІ ---");
                    foreach (var emp in universityStaff)
                    {
                        decimal salary = emp.CalculateSalary();
                        totalFund += salary;
                        // Поліморфний виклик GetPrintInfo та CalculateSalary
                        Console.WriteLine($"{emp.GetPrintInfo()} | До виплати: {salary} грн");
                    }
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine($"ЗАГАЛЬНИЙ ФОНД: {totalFund} грн");
                    break;
                case "31":
                    AddNewShape();
                    break;
                case "32":
                    // Використовуємо метод з StudentGroup, який викликає GetDescription() кожного об'єкта
                    myGroup.ShowAllShapesInfo();
                    break;
                case "33":
                    double totalArea = myGroup.GetTotalAreaOfAllShapes();
                    Console.WriteLine($"Загальна площа всіх фігур у групі: {totalArea:F2}");
                    break;
                case "34":
                    Console.Write("Введіть коефіцієнт масштабування (напр. 1,5): ");
                    if (double.TryParse(Console.ReadLine(), out double factor))
                        myGroup.ResizeAllShapes(factor);
                    else
                        Console.WriteLine("Невірне число.");
                    break;
                case "35":
                    myGroup.DrawAllShapes();
                    break;
                case "36":
                    // Виклик методу, що працює через інтерфейс IPrintable
                    myGroup.ShowAllShapesInfo();
                    break;
                case "37":
                    // Виклик методу для демонстрації пізнього зв'язування
                    myGroup.DemonstrateLateBinding();
                    break;
                case "38":
                    Point seat = new Point(3, 12);
                    Console.WriteLine($"Демонстрація Point: {seat}");
                    GradeRecord grade = new GradeRecord("C# OOP", 95.5, DateTime.Now);
                    Console.WriteLine($"Демонстрація GradeRecord: {grade}");
                    break;
                case "39":
                    PerformanceTest.Run(100000); // Наш клас тестування
                    break;
                case "40":
                    if (myGroup.Students.Any())
                    {
                        var record = myGroup.Students[0].ToRecord();
                        Console.WriteLine($"Зліпок даних (struct): {record}");
                    }
                    break;
                case "41":
                    if (myGroup.Students.Any()) myGroup.Students[0].ShowGrades();
                    break;
                case "42":
                    DateRange term1 = new DateRange(new DateTime(2023, 9, 1), new DateTime(2023, 12, 31));
                    DateRange term2 = new DateRange(new DateTime(2024, 2, 1), new DateTime(2024, 6, 30));
                    Console.WriteLine($"Період 1: {term1}");
                    Console.WriteLine($"Період 1 > Період 2 (за тривалістю): {term1 > term2}");
                    break;
                case "43":
                    myGroup.OptimizeStorage();
                    break;
                case "0": break;
                default: Console.WriteLine("Невірно."); break;
            }
        }
    }

    static void AddStudent()
    {
        try
        {
            Console.Write("ПІБ: "); string name = Console.ReadLine();
            Console.Write("№ заліковки (8 цифр): "); string id = Console.ReadLine();
            Console.Write("Email: "); string email = Console.ReadLine();
            Console.Write("Прогрес (0-100): "); int progress = int.Parse(Console.ReadLine());

            // 1. Створюємо об'єкт (ланцюжок Person -> Student)
            var s = new Student(
                name,
                DateTime.Now.AddYears(-18),
                email,
                id,
                0,
                StudentStatus.Active
            );

            s.CourseProgress = progress;

            // 2. Додавання лабораторних
            Console.Write("Скільки лабораторних ви хочете додати (1-10)? ");
            if (int.TryParse(Console.ReadLine(), out int count) && count >= 1 && count <= 10)
            {
                for (int i = 0; i < count; i++)
                {
                    Console.Write($"Введіть бал за лабораторну №{i + 1} (0-100): ");
                    if (byte.TryParse(Console.ReadLine(), out byte grade))
                    {
                        s.AddLabGrade(i, grade);
                    }
                }
            }

            // ПР №2/4: Додавання оцінок у список
            s.Grades.Add(new GradePoint(8.5));
            s.Grades.Add(new GradePoint(9.0));

            // 3. ПРЯМА ПРИВ'ЯЗКА (Це те, чого бракувало!)
            // Додаємо студента в обидва списки, щоб і стара логіка (п. 3), 
            // і нова поліморфна логіка (п. 32-35) бачили одного й того самого студента.
            myGroup.Students.Add(s);
            myGroup.AddMember(s);

            Console.WriteLine($"\nСтудента {name} додано успішно в групу {myGroup.GroupName}!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nПомилка: {e.Message}");
        }
    }

    static void RemoveStudent()
    {
        Console.Write("№ заліковки: "); string id = Console.ReadLine();
        myGroup.RemoveStudent(id);
        Console.WriteLine("Видалено.");
    }

    // ПР №5: Перероблено вивід під List<Person> з використанням поліморфізму
    static void ShowAllStudents()
    {
        Console.WriteLine("\n=== ПОВНИЙ СПИСОК ЧЛЕНІВ УНІВЕРСИТЕТУ (ПОЛІМОРФІЗМ) ===");
        foreach (var m in myGroup.GetAllMembers())
        {
            // ПОЛІМОРФІЗМ: викличеться GetInfo() конкретного підкласу
            Console.WriteLine(m.GetInfo());
        }
    }

    static void SearchStudent()
    {
        Console.Write("Ключове слово для пошуку: "); string k = Console.ReadLine();
        // Працюємо через GetAllMembers() для поліморфного пошуку
        foreach (var m in myGroup.GetAllMembers().Where(x => x.FullName.Contains(k, StringComparison.OrdinalIgnoreCase) || (x.Notes != null && x.Notes.Contains(k, StringComparison.OrdinalIgnoreCase))))
        {
            Console.WriteLine(m.GetInfo());
        }
    }

    static void CompareTwoStudents()
    {
        // Використовуємо наш новий метод, який повертає відфільтрованих студентів
        var students = myGroup.GetAllStudents();
        if (students.Count < 2) { Console.WriteLine("Треба мінімум 2 студенти в системі."); return; }

        Student s1 = students[0];
        Student s2 = students[1];

        Console.WriteLine($"Порівняння {s1.FullName} та {s2.FullName}:");
        Console.WriteLine($"s1 > s2: {s1 > s2}");
        Console.WriteLine($"s1 == s2: {s1 == s2}");
        Console.WriteLine(s1 + s2);
    }

    static void TestVector()
    {
        Vector v1 = new Vector(1, 2, 3);
        Vector v2 = new Vector(4, 5, 6);
        Console.WriteLine($"v1: {v1}, v2: {v2}");
        Console.WriteLine($"Сума v1 + v2: {v1 + v2}");
        Console.WriteLine($"Довжина v1: {(double)v1:F2}");
    }

    static void TestGradePoint()
    {
        GradePoint g1 = 7.5; // Неявне приведення
        GradePoint g2 = 9.2;
        Console.WriteLine($"Оцінка 1: {g1}, Оцінка 2: {g2}");
        if (g2) Console.WriteLine("Оцінка 2 — відмінна (>=8)");
    }

    static void MergeWithAnotherGroup()
    {
        // 1. Створюємо іншу групу (якщо в конструкторі StudentGroup немає обов'язкових параметрів, залишаємо так)
        StudentGroup other = new StudentGroup
        {
            GroupName = "K-321",
            Specialization = myGroup.Specialization,
            Course = myGroup.Course
        };

        // 2. Створюємо студента через конструктор (ПР №5), передаючи всі необхідні дані
        var testStudent = new Student(
            "Лущан Владислав",             // fullName
            new DateTime(2007, 10, 26),      // dateOfBirth
            "vlad@college.edu.ua",         // personalEmail
            "99999999",                    // recordBookNumber
            95.0,                          // averageGrade
            StudentStatus.Active           // status
        );

        // 3. Додаємо студента (використовуємо AddMember для поліморфізму)
        other.AddStudent(testStudent);

        // 4. Об'єднуємо групи через оператор +
        // Тепер він працює з List<Person> всередині StudentGroup
        var merged = myGroup + other;

        Console.WriteLine($"\nГрупи успішно об'єднано!");
        Console.WriteLine($"Нова назва: {merged.GroupName}");
        Console.WriteLine($"Загальна кількість осіб: {merged.GroupSize}");
    }
    static void TestComplexNumbers()
    {
        Console.WriteLine("\n--- ТЕСТУВАННЯ ВАРІАНТУ 1: КОМПЛЕКСНІ ЧИСЛА ---");
        Complex c1 = new Complex(3, 4);  // 3 + 4i
        Complex c2 = new Complex(1, -2); // 1 - 2i

        Console.WriteLine($"Число 1 (c1): {c1}");
        Console.WriteLine($"Число 2 (c2): {c2}");

        Console.WriteLine($"Додавання (c1 + c2): {c1 + c2}");
        Console.WriteLine($"Віднімання (c1 - c2): {c1 - c2}");
        Console.WriteLine($"Множення (c1 * c2): {c1 * c2}");
        Console.WriteLine($"Ділення (c1 / c2): {c1 / c2}");

        // Тест неявного приведення типів
        Complex c3 = 5.5; // double неявно стає Complex (5.5 + 0i)
        Console.WriteLine($"Неявне приведення (double 5.5 -> Complex): {c3}");

        // Тест явного приведення (Модуль числа)
        double modulus = (double)c1; // Math.Sqrt(3^2 + 4^2) = 5
        Console.WriteLine($"Явне приведення (Модуль числа c1): {modulus}");

        Console.WriteLine($"Перевірка рівності (c1 == c2): {c1 == c2}");
    }
    // ПР №5: Метод для створення різних типів студентів (п. 26 завдання)
    static void AddSpecificStudent()
    {
        try
        {
            Console.WriteLine("\nОберіть особу для додавання:");
            Console.WriteLine("1. Відмінник (ExcellentStudent)");
            Console.WriteLine("2. Іноземний студент (ForeignStudent)");
            Console.WriteLine("3. Працюючий студент (WorkingStudent)");
            Console.WriteLine("4. Випускник (GraduateStudent - Sealed)");
            Console.WriteLine("5. Асистент (Assistant)");
            Console.WriteLine("6. Професор (Professor - Sealed)");
            Console.Write("Ваш вибір: ");
            string typeChoice = Console.ReadLine();

            // Загальні поля для абсолютно всіх людей (базовий клас Person)
            Console.Write("ПІБ: "); string name = Console.ReadLine();
            Console.Write("Email: "); string email = Console.ReadLine();

            switch (typeChoice)
            {
                case "1":
                    Console.Write("№ заліковки (8 цифр): "); string id1 = Console.ReadLine();
                    Console.Write("Президентська надбавка (грн): "); decimal bonus = decimal.Parse(Console.ReadLine());
                    var es = new ExcellentStudent(name, DateTime.Now.AddYears(-19), email, id1, 95.5, StudentStatus.Active, bonus);
                    myGroup.AddMember(es);
                    Console.WriteLine("\nСтудента-відмінника успішно додано!");
                    break;

                case "2":
                    Console.Write("№ заліковки (8 цифр): "); string id2 = Console.ReadLine();
                    Console.Write("Країна походження: "); string country = Console.ReadLine();
                    var fs = new ForeignStudent(name, DateTime.Now.AddYears(-20), email, id2, 75.0, StudentStatus.Active, country, DateTime.Now.AddYears(1));
                    myGroup.AddMember(fs);
                    Console.WriteLine("\nІноземного студента успішно додано!");
                    break;

                case "3":
                    Console.Write("№ заліковки (8 цифр): "); string id3 = Console.ReadLine();
                    Console.Write("Посада/Місце роботи: "); string job = Console.ReadLine();
                    var ws = new WorkingStudent(name, DateTime.Now.AddYears(-21), email, id3, 68.4, StudentStatus.Active, job);
                    myGroup.AddMember(ws);
                    Console.WriteLine("\nПрацюючого студента успішно додано!");
                    break;

                case "4":
                    Console.Write("№ заліковки (8 цифр): "); string id4 = Console.ReadLine();
                    Console.Write("Тема дипломної роботи: "); string thesis = Console.ReadLine();
                    var gs = new GraduateStudent(name, DateTime.Now.AddYears(-22), email, id4, 88.0, StudentStatus.Active, thesis);
                    myGroup.AddMember(gs);
                    Console.WriteLine("\nСтудента-випускника успішно додано!");
                    break;

                case "5": // Індивідуальний Варіант 1
                    Console.Write("Кафедра: "); string depAst = Console.ReadLine();
                    Console.Write("Ставка (грн): "); decimal salaryAst = decimal.Parse(Console.ReadLine());
                    Console.Write("ПІБ куратора/ментора: "); string mentor = Console.ReadLine();
                    var ast = new Assistant(name, DateTime.Now.AddYears(-25), email, depAst, salaryAst, mentor);
                    myGroup.AddMember(ast);
                    Console.WriteLine("\nАсистента успішно додано до штату!");
                    break;

                case "6": // Індивідуальний Варіант 1
                    Console.Write("Кафедра: "); string depProf = Console.ReadLine();
                    Console.Write("Ставка (грн): "); decimal salaryProf = decimal.Parse(Console.ReadLine());
                    Console.Write("Вчене звання (напр. Доктор наук): "); string degree = Console.ReadLine();
                    Console.Write("Надбавка за звання (грн): "); decimal bonusProf = decimal.Parse(Console.ReadLine());
                    var prof = new Professor(name, DateTime.Now.AddYears(-45), email, depProf, salaryProf, degree, bonusProf);
                    myGroup.AddMember(prof);
                    Console.WriteLine("\nПрофесора успішно додано до штату!");
                    break;

                default:
                    Console.WriteLine("Невірний вибір.");
                    return;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Помилка створення: {e.Message}");
        }
    }

    // ПР №5: Виклик методу підрахунку загального стипендіального фонду (п. 28 завдання)
    static void ShowScholarshipReport()
    {
        Console.WriteLine("\n=== ВІДОМІСТЬ НАРАХУВАННЯ СТИПЕНДІЙ ===");
        foreach (var m in myGroup.GetAllMembers())
        {
            decimal sch = m.CalculateScholarship();
            if (sch > 0)
            {
                Console.WriteLine($"{m.FullName} | Стипендія: {sch} грн. (Класс: {m.GetType().Name})");
            }
        }
        Console.WriteLine($"----------------------------------------");
        Console.WriteLine($"ЗАГАЛЬНИЙ СТИПЕНДІАЛЬНИЙ ФОНД ГРУПИ: {myGroup.GetTotalScholarship()} грн.");
    }

    // ПР №5: Тестування Generic-методу з класу StudentGroup (п. 29 завдання)
    static void ShowSpecificTypeMembers()
    {
        Console.WriteLine("\nЯкий тип відобразити?");
        Console.WriteLine("1. Тільки відмінників (ExcellentStudent)");
        Console.WriteLine("2. Тільки іноземців (ForeignStudent)");
        Console.Write("Вибір: ");
        string subChoice = Console.ReadLine();

        if (subChoice == "1")
        {
            var excellents = myGroup.GetMembersByType<ExcellentStudent>();
            Console.WriteLine($"\nЗнайдено відмінників: {excellents.Count}");
            foreach (var e in excellents) Console.WriteLine($"{e.FullName} | Бонус: {e.PresidentialScholarshipBonus} грн.");
        }
        else if (subChoice == "2")
        {
            var foreigners = myGroup.GetMembersByType<ForeignStudent>();
            Console.WriteLine($"\nЗнайдено іноземних студентів: {foreigners.Count}");
            foreach (var f in foreigners) Console.WriteLine($"{f.FullName} | Країна: {f.CountryOfOrigin}");
        }
    }

    // ПР №5: Тест ланцюжка base/override через метод Enroll() (п. 30 завдання)
    static void TestHierarchyEnrollment()
    {
        Console.WriteLine("\n--- ТЕСТУВАННЯ ДИНАМІЧНОГО ЗВ'ЯЗУВАННЯ (Метод Enroll) ---");
        foreach (var m in myGroup.GetAllMembers())
        {
            Console.WriteLine($"Об'єкт класу: {m.GetType().Name}");
            m.Enroll(); // Динамічний поліморфний виклик
            Console.WriteLine(new string('-', 45));
        }
    }

    static void AddNewShape()
    {
        // Перевіряємо, чи є взагалі студенти в групі
        var allStudents = myGroup.GetAllStudents();
        if (!allStudents.Any())
        {
            Console.WriteLine("Помилка: Спочатку додайте хоча б одного студента (пункт 1)!");
            return;
        }

        Console.WriteLine("Оберіть тип: 1-Коло, 2-Прямокутник, 3-Трикутник, 4-Квадрат");
        string type = Console.ReadLine();
        Console.Write("Назва фігури: "); string name = Console.ReadLine();
        Console.Write("Колір: "); string color = Console.ReadLine();

        Shape shape = null;
        try
        {
            switch (type)
            {
                case "1":
                    Console.Write("Радіус: "); double r = double.Parse(Console.ReadLine());
                    shape = new Circle(name, color, r);
                    break;
                case "2":
                    Console.Write("Ширина: "); double w = double.Parse(Console.ReadLine());
                    Console.Write("Висота: "); double h = double.Parse(Console.ReadLine());
                    shape = new Rectangle(name, color, w, h);
                    break;
                case "3":
                    Console.Write("Сторона A: "); double a = double.Parse(Console.ReadLine());
                    Console.Write("Сторона B: "); double b = double.Parse(Console.ReadLine());
                    Console.Write("Сторона C: "); double c = double.Parse(Console.ReadLine());
                    shape = new Triangle(name, color, a, b, c);
                    break;
                case "4":
                    Console.Write("Сторона: "); double side = double.Parse(Console.ReadLine());
                    shape = new Square(name, color, side);
                    break;
                default:
                    Console.WriteLine("Невірний вибір типу.");
                    return;
            }
        }
        catch
        {
            Console.WriteLine("Помилка введення даних! Вводьте числа.");
            return;
        }

        // Program.cs всередині AddNewShape()
        if (shape != null)
        {
            // Звертаємося прямо до властивості Students об'єкта myGroup
            if (myGroup.Students != null && myGroup.Students.Count > 0)
            {
                // Додаємо першому студенту в ОРИГІНАЛЬНОМУ списку
                var firstStudent = myGroup.Students[0];
                firstStudent.AddShape(shape);

                Console.WriteLine($"[СИСТЕМА]: Фігуру '{name}' додано студенту {firstStudent.FullName}.");
                Console.WriteLine($"[DEBUG]: Тепер у цього студента {firstStudent.StudentShapes.Count} фігур(и).");
            }
            else
            {
                Console.WriteLine("Помилка: Студенти не знайдені в основному списку групи!");
            }
        }

        static void TestDateRange()
        {
            Console.WriteLine("\n--- ТЕСТУВАННЯ ВАРІАНТУ 1: ДІАПАЗОН ДАТ ---");
            try
            {
                // Створюємо два діапазони
                DateRange semester1 = new DateRange(new DateTime(2023, 9, 1), new DateTime(2023, 12, 31));
                DateRange winterBreak = new DateRange(new DateTime(2023, 12, 31), new DateTime(2024, 1, 15));

                Console.WriteLine($"Семестр 1: {semester1}");
                Console.WriteLine($"Зимові канікули: {winterBreak}");

                // Перевірка входження дати
                DateTime examDate = new DateTime(2023, 12, 25);
                Console.WriteLine($"Чи входить {examDate:dd.MM.yyyy} у Семестр 1? {semester1.Includes(examDate)}");

                // Порівняння за тривалістю (оператор >)
                if (semester1 > winterBreak)
                {
                    Console.WriteLine("Семестр довший за канікули (порівняння через оператори успішне).");
                }

                // Перевірка рівності
                DateRange copyOfSemester = new DateRange(new DateTime(2023, 9, 1), new DateTime(2023, 12, 31));
                Console.WriteLine($"Копія семестру дорівнює оригіналу? {semester1 == copyOfSemester}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}