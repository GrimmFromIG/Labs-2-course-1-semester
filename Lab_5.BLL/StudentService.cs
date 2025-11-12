using System.Collections.Generic;
using System.Linq;

namespace Lab_5.BLL
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudents();
    }

    public class StudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public int CalculateThirdYearStudentsFromUkraine()
        {
            var students = _repository.GetAllStudents();
            return students.Count(s => s.Course == 3 && s.Country == "Україна");
        }

        public bool ShouldBeExpelled(Student student)
        {
            if (student.AverageGrade < 3.0)
            {
                return true;
            }
            return false;
        }
    }
}