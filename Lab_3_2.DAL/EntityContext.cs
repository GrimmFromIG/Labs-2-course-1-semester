using System.IO;
using System.Text.Json;

namespace Lab_3_2.DAL
{
    public class EntityContext
    {
        private readonly string _filePath;

        public EntityContext(string filePath)
        {
            _filePath = filePath;
        }

        public void WriteData<T>(T data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(data, options);
            File.WriteAllText(_filePath, jsonString);
        }

        public T ReadData<T>()
        {
            if (!File.Exists(_filePath))
            {
                return default(T);
            }

            string jsonString = File.ReadAllText(_filePath);
            
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return default(T);
            }
            
            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}