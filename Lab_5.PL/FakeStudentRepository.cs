using System.Collections.Generic;
using Lab_5.BLL;

namespace Lab_5.PL
{
    public class FakeStudentRepository : IStudentRepository
    {
        private readonly List<Student> _students;

        public FakeStudentRepository(List<Student> students)
        {
            _students = students;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }
    }
}