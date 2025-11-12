using System.Collections.Generic;
using Lab_5.BLL;

namespace Lab_5.DAL
{
    public class FileStudentRepository : IStudentRepository
    {
        private readonly string _filePath;

        public FileStudentRepository(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return new List<Student>();
        }
    }
}