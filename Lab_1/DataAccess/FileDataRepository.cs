using System;
using System.IO;
using Domain;
using System.Text;

namespace DataAccess
{
    public class FileDataRepository : IDataRepository
    {
        public object[] ReadAll(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            string all = File.ReadAllText(path, Encoding.UTF8);
            string[] blocks = all.Split(new string[] { "};" }, StringSplitOptions.RemoveEmptyEntries);
            object[] result = new object[0];

            foreach (var raw in blocks)
            {
                string block = raw.Trim();
                if (string.IsNullOrWhiteSpace(block))
                    continue;

                var lines = block.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length < 2) continue;
                var header = lines[0].Trim();
                var parts = header.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 1) continue;
                var type = parts[0].Trim();

                int braceIndex = block.IndexOf('{');
                if (braceIndex < 0) continue;
                string body = block.Substring(braceIndex + 1).Trim();
                string[] linesBody = body.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                string firstname = null, lastname = null, country = null, studentId = null, subject = null;
                int course = 0;
                double avgGrade = 0;

                foreach (var l in linesBody)
                {
                    var s = l.Trim().TrimEnd(',').Trim();
                    int idx = s.IndexOf(':');
                    if (idx < 0) continue;
                    string key = s.Substring(0, idx).Trim().Trim('"', ' ');
                    string val = s.Substring(idx + 1).Trim().Trim('"', ' ', ',');
                    val = val.Trim().Trim('"');

                    switch (key.ToLower())
                    {
                        case "firstname":
                            firstname = val;
                            break;
                        case "lastname":
                            lastname = val;
                            break;
                        case "country":
                            country = val;
                            break;
                        case "studentid":
                            studentId = val;
                            break;
                        case "course":
                            int.TryParse(val, out course);
                            break;
                        case "avggrade":
                            double.TryParse(val, out avgGrade);
                            break;
                        case "subject":
                            subject = val;
                            break;
                        default:
                            break;
                    }
                }

                if (type.Equals("Student", StringComparison.OrdinalIgnoreCase))
                {
                    var st = new Student()
                    {
                        Firstname = firstname ?? string.Empty,
                        Lastname = lastname ?? string.Empty,
                        Country = country ?? string.Empty,
                        StudentId = studentId ?? string.Empty,
                        Course = course,
                        AvgGrade = avgGrade
                    };
                    result = AppendToArray(result, st);
                }
                else if (type.Equals("Teacher", StringComparison.OrdinalIgnoreCase))
                {
                    var t = new Teacher()
                    {
                        Firstname = firstname ?? string.Empty,
                        Lastname = lastname ?? string.Empty,
                        Country = country ?? string.Empty,
                        Subject = subject ?? string.Empty
                    };
                    result = AppendToArray(result, t);
                }
            }

            return result;
        }

        public void WriteAll(string path, object[] objects)
        {
            using (var sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                foreach (var o in objects)
                {
                    if (o is Student s)
                    {
                        sw.WriteLine($"Student {s.Firstname}{s.Lastname}");
                        sw.WriteLine("{");
                        sw.WriteLine($"\"firstname\": \"{s.Firstname}\",");
                        sw.WriteLine($"\"lastname\": \"{s.Lastname}\",");
                        sw.WriteLine($"\"studentId\": \"{s.StudentId}\",");
                        sw.WriteLine($"\"course\": \"{s.Course}\",");
                        sw.WriteLine($"\"avgGrade\": \"{s.AvgGrade}\",");
                        sw.WriteLine($"\"country\": \"{s.Country}\"}};");
                    }
                    else if (o is Teacher t)
                    {
                        sw.WriteLine($"Teacher {t.Firstname}{t.Lastname}");
                        sw.WriteLine("{");
                        sw.WriteLine($"\"firstname\": \"{t.Firstname}\",");
                        sw.WriteLine($"\"lastname\": \"{t.Lastname}\",");
                        sw.WriteLine($"\"subject\": \"{t.Subject}\",");
                        sw.WriteLine($"\"country\": \"{t.Country}\"}};");
                    }
                }
            }
        }

        private object[] AppendToArray(object[] arr, object item)
        {
            int n = arr.Length;
            object[] newArr = new object[n + 1];
            for (int i = 0; i < n; i++) newArr[i] = arr[i];
            newArr[n] = item;
            return newArr;
        }
    }
}
