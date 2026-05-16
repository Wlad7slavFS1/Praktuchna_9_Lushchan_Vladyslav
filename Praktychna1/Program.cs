using Praktychna1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using static Praktychna1.Student;
using Complex = Praktychna1.Complex;
using Vector = Praktychna1.Vector;

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
            menuBuilder.AppendLine("\n--- СИСТЕМА УПРАВЛІННЯ ГРУПОЮ (ПР №4) ---");
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
            Console.Write("Email: "); string email = Console.ReadLine(); // Додаємо email для Person
            Console.Write("Прогрес (0-100): "); int progress = int.Parse(Console.ReadLine());

            // ПР №5: Створення об'єкта через конструктор (ланцюжок Person -> Student)
            var s = new Student(
                name,                              // fullName
                DateTime.Now.AddYears(-18),       // dateOfBirth (за замовчуванням)
                email,                             // personalEmail
                id,                                // recordBookNumber
                0,                                 // averageGrade (початковий бал)
                StudentStatus.Active               // status
            );

            // Встановлюємо прогрес окремо, якщо це властивість з set
            s.CourseProgress = progress;

            // --- Додавання лабораторних ---
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
                    else
                    {
                        Console.WriteLine("Некоректний бал, встановлено 0.");
                    }
                }
            }

            // ПР №2/4: Додавання оцінок у список
            s.Grades.Add(new GradePoint(8.5));
            s.Grades.Add(new GradePoint(9.0));

            // ПР №5: Використовуємо універсальний метод додавання
            myGroup.AddStudent(s);

            Console.WriteLine("\nСтудента додано успішно!");
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
}