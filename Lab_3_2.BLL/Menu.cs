using System;
using System.Collections.Generic;

namespace Lab_3_2.BLL
{
    public class Menu
    {
        private readonly EntityService _service;

        public Menu()
        {
            _service = new EntityService();
        }

        public void MainMenu()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("--- ГОЛОВНЕ МЕНЮ ---");
                Console.WriteLine("1. Додати студента");
                Console.WriteLine("2. Показати всіх студентів");
                Console.WriteLine("3. Завдання варіанту 5 (3-й курс, Україна)");
                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ShowAllStudents();
                        break;
                    case "3":
                        ShowFilteredStudents();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Натисніть Enter.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("--- ДОДАВАННЯ СТУДЕНТА ---");
            try
            {
                Student s = new Student();
                
                Console.Write("Прізвище: ");
                s.LastName = Console.ReadLine();
                
                Console.Write("Курс (1-6): ");
                s.Course = int.Parse(Console.ReadLine());
                
                Console.Write("Номер студ. квитка: ");
                s.StudentId = Console.ReadLine();
                
                Console.Write("Середній бал: ");
                s.AverageGrade = double.Parse(Console.ReadLine());
                
                Console.Write("Країна (напр. 'Україна'): ");
                s.Country = Console.ReadLine();
                
                Console.Write("Номер залікової книжки: ");
                s.GradebookNumber = Console.ReadLine();

                _service.AddStudent(s);
                Console.WriteLine("Студента успішно додано. Натисніть Enter.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка вводу: {ex.Message}. Натисніть Enter.");
            }
            Console.ReadLine();
        }

        private void ShowAllStudents()
        {
            Console.Clear();
            Console.WriteLine("--- СПИСОК ВСІХ СТУДЕНТІВ ---");
            var students = _service.GetAllStudents();
            if (students.Count == 0)
            {
                Console.WriteLine("Список порожній.");
            }
            else
            {
                PrintStudentList(students);
            }
            Console.WriteLine("Натисніть Enter.");
            Console.ReadLine();
        }

        private void ShowFilteredStudents()
        {
            Console.Clear();
            Console.WriteLine("--- Студенти 3-го курсу з України ---");
            
            var students = _service.GetThirdYearsFromUkraine();
            
            Console.WriteLine($"Знайдено студентів: {students.Count}");
            Console.WriteLine(new string('-', 20));

            if (students.Count > 0)
            {
                PrintStudentList(students);
            }
            
            Console.WriteLine("Натисніть Enter.");
            Console.ReadLine();
        }

        private void PrintStudentList(List<Student> students)
        {
            foreach (var s in students)
            {
                Console.WriteLine($"Прізвище: {s.LastName}, Курс: {s.Course}, Країна: {s.Country}");
                Console.WriteLine($"  Студ. квиток: {s.StudentId}, Бал: {s.AverageGrade}");
                Console.WriteLine();
            }
        }
    }
}