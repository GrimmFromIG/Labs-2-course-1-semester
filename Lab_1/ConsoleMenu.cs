using Lab_1.Domain;
using System;

namespace Lab_1.UI
{
    public static class ConsoleMenu
    {
        public static void PrintHeader(string title)
        {
            Console.WriteLine(new string('=', 60));
            Console.WriteLine(title);
            Console.WriteLine(new string('=', 60));
        }

        public static void ShowCountAndList(Student[] students, int count)
        {
            // Рахуємо студентів 3 курсу з України
            int target = 0;
            for (int i = 0; i < count; i++)
            {
                var s = students[i];
                if (s != null && s.Course == 3 && s.Country == "Ukraine")
                {
                    target++;
                }
            }

            Console.WriteLine($"Кількість студентів 3-го курсу з України: {target}");
            Console.WriteLine();

            // Перераховуємо їх
            for (int i = 0; i < count; i++)
            {
                var s = students[i];
                if (s != null && s.Course == 3 && s.Country == "Ukraine")
                {
                    Console.WriteLine($"- ID: {s.ID} | {s.LastName} | StudentID: {s.StudentId} | Середній бал: {s.AvgGrade}");
                }
            }
            Console.WriteLine();
        }

        public static void ShowEditMenu(Student[] students, ref int studentCount)
        {
            while (true)
            {
                Console.Clear();
                PrintHeader("Редагування студентів");
                Console.WriteLine("1. Додати студента");
                Console.WriteLine("2. Редагувати студента");
                Console.WriteLine("3. Видалити студента");
                Console.WriteLine("4. Показати всіх студентів");
                Console.WriteLine("5. Грати в шахи зі студентом");
                Console.WriteLine("6. Повернутися до головного меню");
                Console.Write("Виберіть опцію: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddStudent(students, ref studentCount);
                        break;
                    case "2":
                        EditStudent(students, studentCount);
                        break;
                    case "3":
                        DeleteStudent(students, ref studentCount);
                        break;
                    case "4":
                        ShowAllStudents(students, studentCount);
                        break;
                    case "5":
                        PlayChessWithStudent(students, studentCount);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void AddStudent(Student[] students, ref int studentCount)
        {
            if (studentCount >= students.Length)
            {
                Console.WriteLine("Досягнуто максимальну кількість студентів!");
                Console.ReadKey();
                return;
            }

            try
            {
                Console.Write("Прізвище: ");
                string lastName = Console.ReadLine();
                
                Console.Write("ID студента: ");
                string studentId = Console.ReadLine();
                
                Console.Write("Курс (1-6): ");
                int course = int.Parse(Console.ReadLine());
                
                Console.Write("Середній бал (0-100): ");
                double avgGrade = double.Parse(Console.ReadLine());
                
                Console.Write("Країна: ");
                string country = Console.ReadLine();

                Console.Write("Унікальний ID: ");
                int id = int.Parse(Console.ReadLine());

                var student = new Student(lastName, studentId, course, avgGrade, country, id);
                students[studentCount++] = student;
                
                Console.WriteLine("Студента додано успішно!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            
            Console.ReadKey();
        }

        private static void EditStudent(Student[] students, int studentCount)
        {
            ShowAllStudents(students, studentCount);
            
            Console.Write("Введіть ID студента для редагування: ");
            int id = int.Parse(Console.ReadLine());

            for (int i = 0; i < studentCount; i++)
            {
                if (students[i]?.ID == id)
                {
                    try
                    {
                        Console.Write("Нове прізвище (поточне: {0}): ", students[i].LastName);
                        string lastName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(lastName))
                            students[i].LastName = lastName;

                        Console.Write("Новий StudentID (поточний: {0}): ", students[i].StudentId);
                        string studentId = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(studentId))
                            students[i].StudentId = studentId;

                        Console.Write("Новий курс (поточний: {0}): ", students[i].Course);
                        string courseStr = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(courseStr))
                            students[i].Course = int.Parse(courseStr);

                        Console.Write("Новий середній бал (поточний: {0}): ", students[i].AvgGrade);
                        string avgGradeStr = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(avgGradeStr))
                            students[i].AvgGrade = double.Parse(avgGradeStr);

                        Console.Write("Нова країна (поточна: {0}): ", students[i].Country);
                        string country = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(country))
                            students[i].Country = country;

                        Console.WriteLine("Студента оновлено успішно!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка: {ex.Message}");
                    }
                    
                    Console.ReadKey();
                    return;
                }
            }
            
            Console.WriteLine("Студента з таким ID не знайдено!");
            Console.ReadKey();
        }

        private static void DeleteStudent(Student[] students, ref int studentCount)
        {
            ShowAllStudents(students, studentCount);
            
            Console.Write("Введіть ID студента для видалення: ");
            int id = int.Parse(Console.ReadLine());

            for (int i = 0; i < studentCount; i++)
            {
                if (students[i]?.ID == id)
                {
                    // Зсуваємо всі елементи праворуч від видаленого на одну позицію вліво
                    for (int j = i; j < studentCount - 1; j++)
                    {
                        students[j] = students[j + 1];
                    }
                    students[studentCount - 1] = null;
                    studentCount--;
                    
                    Console.WriteLine("Студента видалено успішно!");
                    Console.ReadKey();
                    return;
                }
            }
            
            Console.WriteLine("Студента з таким ID не знайдено!");
            Console.ReadKey();
        }

        private static void ShowAllStudents(Student[] students, int studentCount)
        {
            Console.WriteLine("\nВсі студенти:");
            Console.WriteLine(new string('-', 80));
            
            for (int i = 0; i < studentCount; i++)
            {
                var s = students[i];
                Console.WriteLine($"{i + 1}. {s}");
            }
            Console.WriteLine();
        }

        private static void PlayChessWithStudent(Student[] students, int studentCount)
        {
            ShowAllStudents(students, studentCount);
            
            Console.Write("Введіть ID студента для гри в шахи: ");
            int id = int.Parse(Console.ReadLine());

            for (int i = 0; i < studentCount; i++)
            {
                if (students[i]?.ID == id)
                {
                    students[i].PlayChess();
                    Console.ReadKey();
                    return;
                }
            }
            
            Console.WriteLine("Студента з таким ID не знайдено!");
            Console.ReadKey();
        }
    }
}