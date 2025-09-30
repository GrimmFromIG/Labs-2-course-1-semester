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
            using var w = new StreamWriter(_path, false);
            
            // Студенти
            for (int i = 0; i < studentCount; i++)
            {
                var s = students[i];
                w.WriteLine($"Student {s.LastName}");
                w.WriteLine("{");
                w.WriteLine($"  \"lastname\": \"{s.LastName}\",");
                w.WriteLine($"  \"studentId\": \"{s.StudentId}\",");
                w.WriteLine($"  \"course\": \"{s.Course}\",");
                w.WriteLine($"  \"avggrade\": \"{s.AvgGrade}\",");
                w.WriteLine($"  \"country\": \"{s.Country}\",");
                w.WriteLine($"  \"id\": \"{s.ID}\"");
                w.WriteLine("};");
            }

            // Працівники McDonald's
            for (int i = 0; i < workerCount; i++)
            {
                var ww = workers[i];
                w.WriteLine($"McDonaldWorker {ww.LastName}");
                w.WriteLine("{");
                w.WriteLine($"  \"lastname\": \"{ww.LastName}\",");
                w.WriteLine($"  \"position\": \"{ww.Position}\"");
                w.WriteLine("};");
            }

            // Менеджери
            for (int i = 0; i < managerCount; i++)
            {
                var m = managers[i];
                w.WriteLine($"Manager {m.LastName}");
                w.WriteLine("{");
                w.WriteLine($"  \"lastname\": \"{m.LastName}\",");
                w.WriteLine($"  \"department\": \"{m.Department}\"");
                w.WriteLine("};");
            }
        }

        public void LoadAll(Student[] students, out int studentCount,
                            McDonaldWorker[] workers, out int workerCount,
                            Manager[] managers, out int managerCount)
        {
            studentCount = workerCount = managerCount = 0;

            if (!File.Exists(_path)) return;

            using var r = new StreamReader(_path);
            string line;
            while ((line = ReadNonEmpty(r)) != null)
            {
                var header = line.Trim();
                var space = header.IndexOf(' ');
                if (space <= 0) throw new InvalidDataException("Bad header line: " + header);
                var type = header.Substring(0, space);

                string block = ReadBlock(r); 

                ParseKeyValues(block, out string lastname,
                               out string studentId, out string courseStr,
                               out string avgGradeStr, out string country,
                               out string idStr, out string position, out string department);

                switch (type)
                {
                    case "Student":
                        {
                            int course = SafeInt(courseStr, 1);
                            double avgGrade = SafeDouble(avgGradeStr, 0);
                            int id = SafeInt(idStr, 0);
                            
                            var s = new Student(lastname, studentId, course, avgGrade, country, id);
                            students[studentCount++] = s;
                        }
                        break;

                    case "McDonaldWorker":
                        {
                            var ww = new McDonaldWorker(lastname, position ?? "");
                            workers[workerCount++] = ww;
                        }
                        break;

                    case "Manager":
                        {
                            var m = new Manager(lastname, department ?? "");
                            managers[managerCount++] = m;
                        }
                        break;

                    default:
                        throw new InvalidDataException("Unknown type: " + type);
                }
            }
        }

        private static string ReadNonEmpty(StreamReader r)
        {
            string s;
            while ((s = r.ReadLine()) != null)
            {
                s = s.Trim();
                if (s.Length == 0) continue;
                return s;
            }
            return null;
        }

        private static string ReadBlock(StreamReader r)
        {
            string open = ReadNonEmpty(r);
            if (open == null || !open.StartsWith("{"))
                throw new InvalidDataException("Expected '{'");

            string line, acc = "";
            while ((line = r.ReadLine()) != null)
            {
                if (line.Trim() == "};") break;
                acc += line + "\n";
            }
            return acc;
        }

        private static void ParseKeyValues(
            string block,
            out string lastname,
            out string studentId, out string course,
            out string avgGrade, out string country,
            out string id, out string position, out string department)
        {
            lastname = studentId = course = avgGrade = country = id = position = department = null;

            var lines = block.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                var raw = lines[i].Trim().TrimEnd(',');
                if (raw.Length == 0) continue;
                
                int c = raw.IndexOf(':');
                if (c <= 0) continue;
                var k = raw.Substring(0, c).Trim().Trim('"');
                var v = raw.Substring(c + 1).Trim().Trim('"');

                switch (k)
                {
                    case "lastname": lastname = v; break;
                    case "studentId": studentId = v; break;
                    case "course": course = v; break;
                    case "avggrade": avgGrade = v; break;
                    case "country": country = v; break;
                    case "id": id = v; break;
                    case "position": position = v; break;
                    case "department": department = v; break;
                }
            }
        }

        private static int SafeInt(string s, int defVal)
        {
            if (int.TryParse(s, out var x)) return x;
            return defVal;
        }

        private static double SafeDouble(string s, double defVal)
        {
            if (double.TryParse(s, out var x)) return x;
            return defVal;
        }
    }
}