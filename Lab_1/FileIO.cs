using Lab_1.Domain;
using System;
using System.IO;

namespace Lab_1.IO
{
    public sealed class TextFileDataSource
    {
        private readonly string _path;

        public TextFileDataSource(string path) { _path = path; }

        public void SaveAll(Student[] students, int studentCount,
                            McDonaldWorker[] workers, int workerCount,
                            Manager[] managers, int managerCount)
        {
            using var fs = new FileStream(_path, FileMode.Create);
            using var writer = new BinaryWriter(fs);
            
            writer.Write(studentCount);
            
            for (int i = 0; i < studentCount; i++)
            {
                var s = students[i];
                WriteString(writer, s.LastName);
                WriteString(writer, s.StudentId);
                writer.Write(s.Course);
                writer.Write(s.AvgGrade);
                WriteString(writer, s.Country);
                writer.Write(s.ID);
            }
            
            writer.Write(workerCount);
            for (int i = 0; i < workerCount; i++)
            {
                var w = workers[i];
                WriteString(writer, w.LastName);
                WriteString(writer, w.Position);
            }
            
            writer.Write(managerCount);
            for (int i = 0; i < managerCount; i++)
            {
                var m = managers[i];
                WriteString(writer, m.LastName);
                WriteString(writer, m.Department);
            }
        }

        public void LoadAll(Student[] students, out int studentCount,
                            McDonaldWorker[] workers, out int workerCount,
                            Manager[] managers, out int managerCount)
        {
            studentCount = workerCount = managerCount = 0;

            if (!File.Exists(_path)) return;

            using var fs = new FileStream(_path, FileMode.Open);
            using var reader = new BinaryReader(fs);
            
            try
            {
                studentCount = reader.ReadInt32();
                
                for (int i = 0; i < studentCount; i++)
                {
                    string lastName = ReadString(reader);
                    string studentId = ReadString(reader);
                    int course = reader.ReadInt32();
                    double avgGrade = reader.ReadDouble();
                    string country = ReadString(reader);
                    int id = reader.ReadInt32();
                    
                    students[i] = new Student(lastName, studentId, course, avgGrade, country, id);
                }
                
                workerCount = reader.ReadInt32();
                for (int i = 0; i < workerCount; i++)
                {
                    string lastName = ReadString(reader);
                    string position = ReadString(reader);
                    workers[i] = new McDonaldWorker(lastName, position);
                }
                
                managerCount = reader.ReadInt32();
                for (int i = 0; i < managerCount; i++)
                {
                    string lastName = ReadString(reader);
                    string department = ReadString(reader);
                    managers[i] = new Manager(lastName, department);
                }
            }
            catch (EndOfStreamException)
            {
            }
        }

        private static void WriteString(BinaryWriter writer, string value)
        {
            value ??= "";
            writer.Write(value.Length);
            foreach (char c in value)
            {
                writer.Write(c);
            }
        }

        private static string ReadString(BinaryReader reader)
        {
            int length = reader.ReadInt32();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = reader.ReadChar();
            }
            return new string(chars);
        }
    }
}