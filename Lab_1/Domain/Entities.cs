using System;

namespace Domain
{
    public abstract class Person
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public Person() { }
    }

    public interface IPlayable
    {
        void Play();
    }

    public class Student : Person, IPlayable
    {
        public string StudentId { get; set; } = string.Empty;
        public int Course { get; set; }
        public double AvgGrade { get; set; }

        public void Play()
        {
            Console.WriteLine($"{Firstname} {Lastname} (student) plays chess.");
        }

        public override string ToString()
        {
            return $"Student {Firstname} {Lastname}, Id={StudentId}, Course={Course}, Avg={AvgGrade}, Country={Country}";
        }
    }

    public class Teacher : Person
    {
        public string Subject { get; set; } = string.Empty;
        public override string ToString()
        {
            return $"Teacher {Firstname} {Lastname}, Subject={Subject}, Country={Country}";
        }
    }
}
