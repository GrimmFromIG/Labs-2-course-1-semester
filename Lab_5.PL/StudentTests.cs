using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab_5.BLL;
using System;

namespace Lab_5.PL
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
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

        [TestMethod]
        public void LastName_SetNullOrEmpty_ThrowsArgumentException()
        {
            var student = new Student();
            
            try
            {
                student.LastName = "";
                
                // Якщо код дійшов сюди, помилка не виникла = тест провалено
                Assert.Fail("Очікувався виняток ArgumentException, але він не виник.");
            }
            catch (ArgumentException)
            {
                // Успіх, виняток було зловлено
            }
            catch (Exception ex)
            {
                // Виняток іншого типу = тест провалено
                Assert.Fail($"Очікувався ArgumentException, але виник {ex.GetType().Name}.");
            }
        }

        [TestMethod]
        public void Course_SetInvalidValue_ThrowsArgumentException()
        {
            var student = new Student();

            try
            {
                student.Course = 7;
                Assert.Fail("Очікувався виняток ArgumentException, але він не виник.");
            }
            catch (ArgumentException)
            {
                // Успіх
            }
            catch (Exception ex)
            {
                Assert.Fail($"Очікувався ArgumentException, але виник {ex.GetType().Name}.");
            }
        }

        [TestMethod]
        public void AverageGrade_SetInvalidValue_ThrowsArgumentException()
        {
            var student = new Student();

            try
            {
                student.AverageGrade = 5.1;
                Assert.Fail("Очікувався виняток ArgumentException, але він не виник.");
            }
            catch (ArgumentException)
            {
                // Успіх
            }
            catch (Exception ex)
            {
                Assert.Fail($"Очікувався ArgumentException, але виник {ex.GetType().Name}.");
            }
        }
    }
}