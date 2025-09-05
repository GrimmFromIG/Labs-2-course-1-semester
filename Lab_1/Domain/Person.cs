using System;

namespace UniversityApp.Domain
{
    public interface IIdentifiable
    {
        string UniqueId { get; }
    }

    public abstract class Person : IIdentifiable
    {
        public string FirstName { get; set; } = "";
        public string LastName  { get; set; } = "";
        public string Passport  { get; set; } = "";   // e.g., AA123456
        public string TaxId     { get; set; } = "";   // 10 digits in UA
        public bool HasDriverLicense { get; set; }

        public abstract string UniqueId { get; }

        public virtual void Study()
        {
            Console.WriteLine($"{FirstName} {LastName} is self-studying.");
        }

        public virtual void Teach()
        {
            Console.WriteLine($"{FirstName} {LastName} shares knowledge with others.");
        }

        public virtual void Drive()
        {
            if (HasDriverLicense)
                Console.WriteLine($"{FirstName} {LastName} drives a vehicle.");
            else
                Console.WriteLine($"{FirstName} {LastName} has no driver license.");
        }

        public string FullName => $"{FirstName} {LastName}";
    }

    public class Student : Person
    {
        public string StudentId { get; set; } = "";     // e.g., KB123456
        public string RecordBook { get; set; } = "";    // e.g., AB-123456
        public string Country { get; set; } = "Ukraine";
        public int Course { get; set; } = 1;            // 1..6
        public string Group { get; set; } = "CS-11";

        public override string UniqueId => StudentId;

        public override void Study()
        {
            Console.WriteLine($"Student {FullName} studies at the university (course {Course}).");
        }
    }

    public class Teacher : Person
    {
        public string EmployeeId { get; set; } = "";    // internal id
        public string Department { get; set; } = "IT";
        public override string UniqueId => EmployeeId;

        public override void Teach()
        {
            Console.WriteLine($"Teacher {FullName} teaches in {Department}.");
        }
    }

    public class Driver : Person
    {
        public string LicenseNumber { get; set; } = ""; // e.g., ABC123456
        public string VehicleType { get; set; } = "Car";
        public override string UniqueId => LicenseNumber;

        public override void Drive()
        {
            if (HasDriverLicense)
                Console.WriteLine($"Driver {FullName} drives a {VehicleType} (license {LicenseNumber}).");
            else
                base.Drive();
        }
    }
}
