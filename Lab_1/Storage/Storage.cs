using System;
using System.IO;
using UniversityApp.Domain;

namespace UniversityApp.Storage
{
    // Simple file DB that writes elements line-by-line in a readable format
    public class FileDatabase
    {
        private readonly string _path;
        public FileDatabase(string path) { _path = path; }

        public string PathToFile => _path;

        public void Save(Student[] students, Teacher[] teachers, Driver[] drivers)
        {
            using var sw = new StreamWriter(_path, false);
            foreach (var s in students)
            {
                if (s is null) continue;
                sw.WriteLine("Student " + s.FullName);
                sw.WriteLine("{");
                sw.WriteLine($"firstname: {s.FirstName}");
                sw.WriteLine($"lastname: {s.LastName}");
                sw.WriteLine($"studentid: {s.StudentId}");
                sw.WriteLine($"recordbook: {s.RecordBook}");
                sw.WriteLine($"country: {s.Country}");
                sw.WriteLine($"course: {s.Course}");
                sw.WriteLine($"group: {s.Group}");
                sw.WriteLine($"passport: {s.Passport}");
                sw.WriteLine($"taxid: {s.TaxId}");
                sw.WriteLine($"hasdriverlicense: {s.HasDriverLicense}");
                sw.WriteLine("};");
            }
            foreach (var t in teachers)
            {
                if (t is null) continue;
                sw.WriteLine("Teacher " + t.FullName);
                sw.WriteLine("{");
                sw.WriteLine($"firstname: {t.FirstName}");
                sw.WriteLine($"lastname: {t.LastName}");
                sw.WriteLine($"employeeid: {t.EmployeeId}");
                sw.WriteLine($"department: {t.Department}");
                sw.WriteLine($"passport: {t.Passport}");
                sw.WriteLine($"taxid: {t.TaxId}");
                sw.WriteLine($"hasdriverlicense: {t.HasDriverLicense}");
                sw.WriteLine("};");
            }
            foreach (var d in drivers)
            {
                if (d is null) continue;
                sw.WriteLine("Driver " + d.FullName);
                sw.WriteLine("{");
                sw.WriteLine($"firstname: {d.FirstName}");
                sw.WriteLine($"lastname: {d.LastName}");
                sw.WriteLine($"licensenumber: {d.LicenseNumber}");
                sw.WriteLine($"vehicletype: {d.VehicleType}");
                sw.WriteLine($"passport: {d.Passport}");
                sw.WriteLine($"taxid: {d.TaxId}");
                sw.WriteLine($"hasdriverlicense: {d.HasDriverLicense}");
                sw.WriteLine("};");
            }
        }

        public void Load(out Student[] students, out Teacher[] teachers, out Driver[] drivers)
        {
            students = new Student[0];
            teachers = new Teacher[0];
            drivers = new Driver[0];
            if (!File.Exists(_path)) return;

            using var sr = new StreamReader(_path);
            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith("Student "))
                {
                    var obj = new Student();
                    ParseBlock(sr, out var dict);
                    Apply(dict, obj);
                    students = ArrayUtils.Append(students, obj);
                }
                else if (line.StartsWith("Teacher "))
                {
                    var obj = new Teacher();
                    ParseBlock(sr, out var dict);
                    Apply(dict, obj);
                    teachers = ArrayUtils.Append(teachers, obj);
                }
                else if (line.StartsWith("Driver "))
                {
                    var obj = new Driver();
                    ParseBlock(sr, out var dict);
                    Apply(dict, obj);
                    drivers = ArrayUtils.Append(drivers, obj);
                }
            }
        }

        private static void ParseBlock(StreamReader sr, out System.Collections.Generic.Dictionary<string, string> dict)
        {
            dict = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            // Expect '{'
            string? l = sr.ReadLine();
            if (l == null) return;
            while ((l = sr.ReadLine()) != null)
            {
                l = l.Trim();
                if (l == "};") break;
                var idx = l.IndexOf(':');
                if (idx > 0)
                {
                    var key = l.Substring(0, idx).Trim();
                    var value = l.Substring(idx + 1).Trim();
                    dict[key] = value;
                }
            }
        }

        private static void Apply(System.Collections.Generic.Dictionary<string, string> d, Student s)
        {
            s.FirstName = d.GetValueOrDefault("firstname","");
            s.LastName  = d.GetValueOrDefault("lastname","");
            s.StudentId = d.GetValueOrDefault("studentid","");
            s.RecordBook= d.GetValueOrDefault("recordbook","");
            s.Country   = d.GetValueOrDefault("country","");
            s.Group     = d.GetValueOrDefault("group","CS-11");
            if (int.TryParse(d.GetValueOrDefault("course","1"), out var k)) s.Course = k;
            s.Passport  = d.GetValueOrDefault("passport","");
            s.TaxId     = d.GetValueOrDefault("taxid","");
            if (bool.TryParse(d.GetValueOrDefault("hasdriverlicense","false"), out var b)) s.HasDriverLicense = b;
        }
        private static void Apply(System.Collections.Generic.Dictionary<string, string> d, Teacher t)
        {
            t.FirstName = d.GetValueOrDefault("firstname","");
            t.LastName  = d.GetValueOrDefault("lastname","");
            t.EmployeeId= d.GetValueOrDefault("employeeid","");
            t.Department= d.GetValueOrDefault("department","IT");
            t.Passport  = d.GetValueOrDefault("passport","");
            t.TaxId     = d.GetValueOrDefault("taxid","");
            if (bool.TryParse(d.GetValueOrDefault("hasdriverlicense","false"), out var b)) t.HasDriverLicense = b;
        }
        private static void Apply(System.Collections.Generic.Dictionary<string, string> d, Driver t)
        {
            t.FirstName = d.GetValueOrDefault("firstname","");
            t.LastName  = d.GetValueOrDefault("lastname","");
            t.LicenseNumber= d.GetValueOrDefault("licensenumber","");
            t.VehicleType= d.GetValueOrDefault("vehicletype","Car");
            t.Passport  = d.GetValueOrDefault("passport","");
            t.TaxId     = d.GetValueOrDefault("taxid","");
            if (bool.TryParse(d.GetValueOrDefault("hasdriverlicense","false"), out var b)) t.HasDriverLicense = b;
        }
    }

    public static class ArrayUtils
    {
        public static T[] Append<T>(T[] array, T item)
        {
            var n = array.Length;
            var result = new T[n + 1];
            for (int i = 0; i < n; i++) result[i] = array[i];
            result[n] = item;
            return result;
        }

        public static T[] RemoveAt<T>(T[] array, int index)
        {
            var n = array.Length;
            if (index < 0 || index >= n) return array;
            var result = new T[n - 1];
            for (int i = 0, j = 0; i < n; i++)
            {
                if (i == index) continue;
                result[j++] = array[i];
            }
            return result;
        }
    }

    public class Repository
    {
        private Student[] _students = new Student[0];
        private Teacher[] _teachers = new Teacher[0];
        private Driver[]  _drivers  = new Driver[0];
        private readonly FileDatabase _db;

        public Repository(FileDatabase db) { _db = db; }

        public (Student[], Teacher[], Driver[]) Snapshot() => (_students, _teachers, _drivers);

        public void Add(Student s) => _students = ArrayUtils.Append(_students, s);
        public void Add(Teacher t) => _teachers = ArrayUtils.Append(_teachers, t);
        public void Add(Driver d)  => _drivers  = ArrayUtils.Append(_drivers, d);

        public bool DeleteById(string id)
        {
            for (int i = 0; i < _students.Length; i++) if (_students[i].UniqueId == id) { _students = ArrayUtils.RemoveAt(_students, i); return true; }
            for (int i = 0; i < _teachers.Length; i++) if (_teachers[i].UniqueId == id) { _teachers = ArrayUtils.RemoveAt(_teachers, i); return true; }
            for (int i = 0; i < _drivers.Length;  i++) if (_drivers[i].UniqueId  == id) { _drivers  = ArrayUtils.RemoveAt(_drivers,  i); return true; }
            return false;
        }

        public Domain.Person? FindByLastName(string lastName)
        {
            foreach (var s in _students) if (s.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)) return s;
            foreach (var t in _teachers) if (t.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)) return t;
            foreach (var d in _drivers)  if (d.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)) return d;
            return null;
        }

        public Domain.Person? FindById(string id)
        {
            foreach (var s in _students) if (s.UniqueId == id) return s;
            foreach (var t in _teachers) if (t.UniqueId == id) return t;
            foreach (var d in _drivers)  if (d.UniqueId == id) return d;
            return null;
        }

        public void Save() => _db.Save(_students, _teachers, _drivers);
        public void Load() => _db.Load(out _students, out _teachers, out _drivers);
        public string FilePath => _db.PathToFile;
    }
}
