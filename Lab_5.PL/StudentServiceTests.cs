using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab_5.BLL;
using System.Collections.Generic;

namespace Lab_5.PL
{
    public class StudentServiceTests
    {
        public void CalculateThirdYearStudentsFromUkraine_ReturnsCorrectCount()
        {
            var students = new List<Student>
            {
                new Student { LastName = "A", Course = 3, Country = "Україна", AverageGrade = 4.0 },
                new Student { LastName = "B", Course = 3, Country = "Україна", AverageGrade = 4.1 },
                new Student { LastName = "C", Course = 2, Country = "Україна", AverageGrade = 4.2 },
                new Student { LastName = "D", Course = 3, Country = "Польща", AverageGrade = 4.3 }
            };
            var fakeRepo = new FakeStudentRepository(students);
            var service = new StudentService(fakeRepo);

            int count = service.CalculateThirdYearStudentsFromUkraine();

            Assert.AreEqual(2, count);
        }

        public void CalculateThirdYearStudentsFromUkraine_NoMatches_ReturnsZero()
        {
            var students = new List<Student>
            {
                new Student { LastName = "C", Course = 2, Country = "Україна", AverageGrade = 4.2 },
                new Student { LastName = "D", Course = 3, Country = "Польща", AverageGrade = 4.3 }
            };
            var fakeRepo = new FakeStudentRepository(students);
            var service = new StudentService(fakeRepo);

            int count = service.CalculateThirdYearStudentsFromUkraine();

            Assert.AreEqual(0, count);
        }

        public void ShouldBeExpelled_FailingGrade_ReturnsTrue()
        {
            var service = new StudentService(null); 
            var student = new Student { LastName = "E", AverageGrade = 2.9, Course = 1 };

            bool result = service.ShouldBeExpelled(student);

            Assert.IsTrue(result);
        }

        public void ShouldBeExpelled_PassingGrade_ReturnsFalse()
        {
            var service = new StudentService(null);
            var student = new Student { LastName = "F", AverageGrade = 3.0, Course = 1 };

            bool result = service.ShouldBeExpelled(student);

            Assert.IsFalse(result);
        }
    }
}