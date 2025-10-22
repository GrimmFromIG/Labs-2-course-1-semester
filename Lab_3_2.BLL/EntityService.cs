using Lab_3_2.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Lab_3_2.BLL
{
    public class EntityService
    {
        private readonly EntityContext _context;
        private List<Student> _students;

        private const string DbFileName = "students.json";

        public EntityService()
        {
            _context = new EntityContext(DbFileName);
            _students = _context.ReadData<List<Student>>();

            if (_students == null)
            {
                _students = new List<Student>();
            }
        }

        public void AddStudent(Student student)
        {
            _students.Add(student);
            _context.WriteData(_students);
        }

        public List<Student> GetAllStudents()
        {
            return _students;
        }

        public List<Student> GetThirdYearsFromUkraine()
        {
            var filteredStudents = _students
                .Where(s => s.Course == 3 && s.Country.Equals("Україна", System.StringComparison.OrdinalIgnoreCase))
                .ToList();

            return filteredStudents;
        }
    }
}