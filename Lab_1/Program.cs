using System;
using Lab_1.Domain;
using Lab_1.IO;
using Lab_1.UI;

namespace Lab_1
{
    internal class Program
    {
        private static readonly Student[] _students = new Student[256];
        private static readonly McDonaldWorker[] _workers = new McDonaldWorker[64];
        private static readonly Manager[] _managers = new Manager[64];

        private static int _studentCount, _workerCount, _managerCount;

        static void Main()
        {
            var dataPath = "data.txt";
            var ds = new TextFileDataSource(dataPath);

            if (!System.IO.File.Exists(dataPath))
            {
                SeedDemoData();
                ds.SaveAll(_students, _studentCount, _workers, _workerCount, _managers, _managerCount);
            }

            // Чистимо масиви перед завантаженням
            Array.Clear(_students, 0, _students.Length);
            Array.Clear(_workers, 0, _workers.Length);
            Array.Clear(_managers, 0, _managers.Length);
            
            _studentCount = _workerCount = _managerCount = 0;

            // Читаємо з файлу
            ds.LoadAll(_students, out _studentCount, _workers, out _workerCount, _managers, out _managerCount);

            ShowMainMenu(ds);
        }

        private static void ShowMainMenu(TextFileDataSource ds)
        {
            while (true)
            {
                Console.Clear();
                ConsoleMenu.PrintHeader("Демонстрація ЛР 3.1 — робота з файлами та сутностями (варіант 3)");
                Console.WriteLine("1. Показати студентів 3-го курсу з України");
                Console.WriteLine("2. Редагувати студентів");
                Console.WriteLine("3. Зберегти дані");
                Console.WriteLine("4. Вийти");
                Console.Write("Виберіть опцію: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        ConsoleMenu.PrintHeader("Студенти 3-го курсу з України");
                        ConsoleMenu.ShowCountAndList(_students, _studentCount);
                        Console.WriteLine("Натисніть будь-яку клавішу, щоб продовжити...");
                        Console.ReadKey();
                        break;
                    case "2":
                        ConsoleMenu.ShowEditMenu(_students, ref _studentCount);
                        break;
                    case "3":
                        ds.SaveAll(_students, _studentCount, _workers, _workerCount, _managers, _managerCount);
                        Console.WriteLine("Дані збережено успішно!");
                        Console.ReadKey();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void SeedDemoData()
        {
            // Студенти 3-го курсу з України
            _students[_studentCount++] = new Student("Shevchenko", "KB0001", 3, 85.5, "Ukraine", 1);
            _students[_studentCount++] = new Student("Petrenko", "KB0002", 3, 78.2, "Ukraine", 2);
            _students[_studentCount++] = new Student("Koval", "KB0003", 3, 92.0, "Ukraine", 3);
            _students[_studentCount++] = new Student("Bondar", "KB0004", 3, 88.7, "Ukraine", 4);
            
            // Інші студенти
            _students[_studentCount++] = new Student("Ivanov", "KB0005", 1, 75.0, "Ukraine", 5);
            _students[_studentCount++] = new Student("Kowalski", "KB0006", 2, 82.3, "Poland", 6);
            _students[_studentCount++] = new Student("Smith", "KB0007", 4, 79.8, "USA", 7);

            _workers[_workerCount++] = new McDonaldWorker("Ivanova", "Cashier");
            _managers[_managerCount++] = new Manager("Sydorenko", "IT Department");
        }
    }
}