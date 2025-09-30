using System;
using DataAccess;
using Domain;

namespace AppConsole
{
    public class ConsoleMenu
    {
        private readonly IDataRepository _repo;

        public ConsoleMenu(IDataRepository repo)
        {
            _repo = repo;
        }

        public void Run()
        {
            Console.WriteLine("=== University App ===");
            Console.Write("Enter path to data file: ");
            string? path = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("No path provided. Exiting.");
                return;
            }

            object[] objects;
            try
            {
                objects = _repo.ReadAll(path.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
                return;
            }

            int count3rdUkraine = 0;
            Student[] found = new Student[0];

            foreach (var o in objects)
            {
                if (o is Student s)
                {
                    if (s.Course == 3 && s.Country != null && s.Country.Trim().ToLower().Contains("ukrain"))
                    {
                        count3rdUkraine++;
                        Student[] tmp = new Student[found.Length + 1];
                        for (int i = 0; i < found.Length; i++) tmp[i] = found[i];
                        tmp[found.Length] = s;
                        found = tmp;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Кількість студентів 3-го курсу, які проживають в Україні: {count3rdUkraine}");
            if (found.Length > 0)
            {
                Console.WriteLine("Деталі:");
                foreach (var s in found) Console.WriteLine(s.ToString());
            }
            else
            {
                Console.WriteLine("Не знайдено відповідних студентів.");
            }

            Console.WriteLine();
            Console.WriteLine("Виконати Play() для тих сутностей, що можуть грати:");
            foreach (var o in objects)
            {
                if (o is IPlayable p)
                {
                    p.Play();
                }
            }

            Console.WriteLine("Натисніть Enter для виходу...");
            Console.ReadLine();
        }
    }
}
