using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab_5.BLL;
using System;

namespace Lab_5.PL
{
    public class StudentTests
    {
        public void Student_ValidData_PropertiesSetCorrectly()
        {
            var student = new Student();

            student.LastName = "Петренко";
            student.Course = 3;
            student.AverageGrade = 4.5;

            Assert.AreEqual("Петренко", student.LastName);
            Assert.AreEqual(3, student.Course);
            Assert.AreEqual(4.5, student.AverageGrade);
        }

        public void LastName_SetNullOrEmpty_ThrowsArgumentException()
        {
            var student = new Student();
            student.LastName = ""; 
        }

        public void Course_SetInvalidValue_ThrowsArgumentException()
        {
            var student = new Student();
            student.Course = 7; 
        }

        public void AverageGrade_SetInvalidValue_ThrowsArgumentException()
        {
            var student = new Student();
            student.AverageGrade = 5.1; 
        }
    }
}