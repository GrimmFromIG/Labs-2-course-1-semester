using System;

namespace Lab_1.Domain
{
    public enum Gender { Unknown = 0, Male = 1, Female = 2 }

    public abstract class Person
    {
        public string LastName;    

        protected Person(string last)
        {
            if (string.IsNullOrWhiteSpace(last)) throw new ArgumentException("LastName is required");
            LastName = last.Trim();
        }
    }

    public interface ICanPlayChess { void PlayChess(); }

    public sealed class Student : Person, ICanPlayChess
    {
        public string StudentId;   
        public int Course;         
        public double AvgGrade;    // Середній бал
        public string Country;     // Країна проживання
        public int ID;             // Унікальний ідентифікатор

        public Student(string last, string studentId, int course, double avgGrade, string country, int id)
            : base(last)
        {
            if (string.IsNullOrWhiteSpace(studentId)) throw new ArgumentException("StudentId is required");
            if (course < 1 || course > 6) throw new ArgumentException("Course must be 1..6");
            if (avgGrade < 0 || avgGrade > 100) throw new ArgumentException("AvgGrade must be 0-100");
            StudentId = studentId.Trim();
            Course = course;
            AvgGrade = avgGrade;
            Country = country ?? "Ukraine";
            ID = id;
        }

        public void PlayChess() 
        { 
            Console.WriteLine($"Студент {LastName} грає в шахи");
        }

        public override string ToString()
        {
            return $"ID: {ID} | Прізвище: {LastName} | StudentID: {StudentId} | Курс: {Course} | Середній бал: {AvgGrade} | Країна: {Country}";
        }
    }

    public sealed class McDonaldWorker : Person, ICanPlayChess
    {
        public string Position;
        public McDonaldWorker(string last, string position = null) : base(last)
        {
            Position = position ?? "";
        }

        public void PlayChess() 
        { 
            Console.WriteLine($"Працівник McDonald's {LastName} грає в шахи");
        }
    }

    public sealed class Manager : Person, ICanPlayChess
    {
        public string Department;
        public Manager(string last, string department = null) : base(last)
        {
            Department = department ?? "";
        }

        public void PlayChess() 
        { 
            Console.WriteLine($"Менеджер {LastName} грає в шахи");
        }
    }
}