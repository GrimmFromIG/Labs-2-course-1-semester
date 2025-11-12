using System;

namespace Lab_5.BLL
{
    public class Student
    {
        private string _lastName;
        private int _course;
        private double _averageGrade;

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Прізвище не може бути порожнім.");
                _lastName = value;
            }
        }

        public int Course
        {
            get => _course;
            set
            {
                if (value < 1 || value > 6)
                    throw new ArgumentException("Курс має бути в діапазоні 1-6.");
                _course = value;
            }
        }

        public string StudentId { get; set; }
        
        public double AverageGrade
        {
            get => _averageGrade;
            set
            {
                if (value < 0.0 || value > 5.0)
                    throw new ArgumentException("Середній бал має бути в діапазоні 0.0-5.0.");
                _averageGrade = value;
            }
        }
        
        public string Country { get; set; }
        public string RecordBookNumber { get; set; }
    }
}