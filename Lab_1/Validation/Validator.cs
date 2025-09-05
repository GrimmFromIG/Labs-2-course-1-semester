using System.Text.RegularExpressions;

namespace UniversityApp.Validation
{
    public static class Validators
    {
        // Names/Country: letters (latin/cyrillic), hyphen and space
        public static bool IsName(string value) =>
            Regex.IsMatch(value, @"^[A-Za-zА-Яа-яЁёІіЇїЄєҐґ' -]{2,40}$");

        public static bool IsCourse(string v) => int.TryParse(v, out var k) && k >= 1 && k <= 6;

        public static bool IsStudentId(string v) => Regex.IsMatch(v, @"^[A-Z]{2}\d{6}$");
        public static bool IsRecordBook(string v) => Regex.IsMatch(v, @"^[A-Z]{2}-\d{6}$");
        public static bool IsPassport(string v) => Regex.IsMatch(v, @"^[A-Z]{2}\d{6}$");
        public static bool IsTaxId(string v) => Regex.IsMatch(v, @"^\d{10}$");
        public static bool IsMilitaryCard(string v) => Regex.IsMatch(v, @"^[A-Z]{2}\d{7}$");
        public static bool IsLicense(string v) => Regex.IsMatch(v, @"^[A-Z]{2,3}\d{6,7}$");
        public static bool IsGroup(string v) => Regex.IsMatch(v, @"^[A-Za-z]{2,5}-\d{2}$");
    }
}