using System;
using UniversityApp.Domain;
using UniversityApp.Validation;
using UniversityApp.Storage;

namespace UniversityApp.UI
{
    public class ConsoleMenu
    {
        private readonly Repository _repo;
        public ConsoleMenu(Repository repo) { _repo = repo; }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n=== DB Menu ===");
                Console.WriteLine("1) Add Student");
                Console.WriteLine("2) Add Teacher");
                Console.WriteLine("3) Add Driver");
                Console.WriteLine("4) List All");
                Console.WriteLine("5) Search by Last Name");
                Console.WriteLine("6) Search by Unique ID");
                Console.WriteLine("7) Delete by Unique ID");
                Console.WriteLine("8) Save to File");
                Console.WriteLine("9) Load from File");
                Console.WriteLine("0) Exit");
                Console.Write("Choose: ");
                var key = Console.ReadLine();
                Console.WriteLine();
                switch (key)
                {
                    case "1": _repo.Add(ReadStudent()); break;
                    case "2": _repo.Add(ReadTeacher()); break;
                    case "3": _repo.Add(ReadDriver()); break;
                    case "4": ListAll(); break;
                    case "5": SearchByLastName(); break;
                    case "6": SearchById(); break;
                    case "7": DeleteById(); break;
                    case "8": _repo.Save(); Console.WriteLine($"Saved to {_repo.FilePath}"); break;
                    case "9": _repo.Load(); Console.WriteLine($"Loaded from {_repo.FilePath}"); break;
                    case "0": return;
                    default: Console.WriteLine("Unknown option."); break;
                }
            }
        }

        private static string Ask(string prompt, Func<string,bool> validator, string errorTip)
        {
            while (true)
            {
                Console.Write(prompt);
                var v = Console.ReadLine() ?? "";
                if (validator(v)) return v;
                Console.WriteLine("Invalid input: " + errorTip);
            }
        }

        private static bool AskBool(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " (y/n): ");
                var v = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
                if (v == "y" || v == "yes") return true;
                if (v == "n" || v == "no") return false;
            }
        }

        private Student ReadStudent()
        {
            var s = new Student();
            s.FirstName = Ask("First name: ", Validators.IsName, "Only letters, space, dash, 2..40.");
            s.LastName  = Ask("Last name: ", Validators.IsName, "Only letters, space, dash, 2..40.");
            s.StudentId = Ask("StudentID (e.g., KB123456): ", Validators.IsStudentId, "Format: 2 capital letters + 6 digits.");
            s.RecordBook= Ask("RecordBook (e.g., AB-123456): ", Validators.IsRecordBook, "Format: 2 capital letters + '-' + 6 digits.");
            s.Country   = Ask("Country: ", Validators.IsName, "Letters/spaces only.");
            s.Group     = Ask("Group (e.g., CS-11): ", Validators.IsGroup, "Format: letters-2digits, e.g., CS-11.");
            s.Course    = int.Parse(Ask("Course [1..6]: ", Validators.IsCourse, "Must be 1..6."));
            s.Passport  = Ask("Passport (AA123456): ", Validators.IsPassport, "2 capital letters + 6 digits.");
            s.TaxId     = Ask("Tax ID (10 digits): ", Validators.IsTaxId, "Exactly 10 digits.");
            s.HasDriverLicense = AskBool("Has driver license");
            return s;
        }

        private Teacher ReadTeacher()
        {
            var t = new Teacher();
            t.FirstName = Ask("First name: ", Validators.IsName, "Only letters, space, dash, 2..40.");
            t.LastName  = Ask("Last name: ", Validators.IsName, "Only letters, space, dash, 2..40.");
            t.EmployeeId= Ask("Employee ID (AA123456): ", Validators.IsPassport, "2 capital letters + 6 digits.");
            t.Department= Ask("Department: ", Validators.IsName, "Letters/spaces only.");
            t.Passport  = Ask("Passport (AA123456): ", Validators.IsPassport, "2 capital letters + 6 digits.");
            t.TaxId     = Ask("Tax ID (10 digits): ", Validators.IsTaxId, "Exactly 10 digits.");
            t.HasDriverLicense = AskBool("Has driver license");
            return t;
        }

        private Driver ReadDriver()
        {
            var d = new Driver();
            d.FirstName = Ask("First name: ", Validators.IsName, "Only letters, space, dash, 2..40.");
            d.LastName  = Ask("Last name: ", Validators.IsName, "Only letters, space, dash, 2..40.");
            d.LicenseNumber= Ask("License (ABC123456): ", Validators.IsLicense, "2-3 capital letters + 6-7 digits.");
            d.VehicleType  = Ask("Vehicle type: ", Validators.IsName, "Letters/spaces only.");
            d.Passport  = Ask("Passport (AA123456): ", Validators.IsPassport, "2 capital letters + 6 digits.");
            d.TaxId     = Ask("Tax ID (10 digits): ", Validators.IsTaxId, "Exactly 10 digits.");
            d.HasDriverLicense = true;
            return d;
        }

        private void ListAll()
        {
            var (students, teachers, drivers) = _repo.Snapshot();
            Console.WriteLine("-- Students --");
            for (int i = 0; i < students.Length; i++)
            {
                var s = students[i];
                Console.WriteLine($"{i+1}. {s.FullName}, ID: {s.StudentId}, Course:{s.Course}, Group:{s.Group}");
            }
            Console.WriteLine("-- Teachers --");
            for (int i = 0; i < teachers.Length; i++)
            {
                var t = teachers[i];
                Console.WriteLine($"{i+1}. {t.FullName}, EmployeeID: {t.EmployeeId}, Dept:{t.Department}");
            }
            Console.WriteLine("-- Drivers --");
            for (int i = 0; i < drivers.Length; i++)
            {
                var d = drivers[i];
                Console.WriteLine($"{i+1}. {d.FullName}, License: {d.LicenseNumber}, Vehicle:{d.VehicleType}");
            }
        }

        private void SearchByLastName()
        {
            Console.Write("Last name to search: ");
            var ln = Console.ReadLine() ?? "";
            var p = _repo.FindByLastName(ln);
            if (p == null) { Console.WriteLine("Not found."); return; }
            Console.WriteLine($"{p.GetType().Name}: {p.FullName} (ID: {p.UniqueId})");
        }

        private void SearchById()
        {
            Console.Write("Unique ID: ");
            var id = Console.ReadLine() ?? "";
            var p = _repo.FindById(id);
            if (p == null) { Console.WriteLine("Not found."); return; }
            Console.WriteLine($"{p.GetType().Name}: {p.FullName} (ID: {p.UniqueId})");
        }

        private void DeleteById()
        {
            Console.Write("Unique ID to delete: ");
            var id = Console.ReadLine() ?? "";
            Console.WriteLine(_repo.DeleteById(id) ? "Deleted." : "Not found.");
        }
    }
}
