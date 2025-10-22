using System;
using System.IO;
using System.Text.Json;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Rectangle[] rectangles = new Rectangle[5];
        rectangles[0] = new Rectangle("Червоний", "Чорний", 10, 5);
        rectangles[1] = new Rectangle("Синій", "Жовтий", 7, 7);
        rectangles[2] = new Rectangle("Зелений", "Чорний", 8.5, 3);
        rectangles[3] = new Rectangle("Білий", "Сірий", 12, 10);
        rectangles[4] = new Rectangle("Жовтий", "Чорний", 4, 6);

        string fileName = "rectangles.json";

        Console.WriteLine("Серіалізація масиву у файл " + fileName);
        
        var options = new JsonSerializerOptions { WriteIndented = true }; 
        string jsonString = JsonSerializer.Serialize(rectangles, options);
        File.WriteAllText(fileName, jsonString);

        Console.WriteLine("Серіалізацію завершено.");
        Console.WriteLine("\n=============================\n");

        Console.WriteLine("Десеріалізація даних з файлу " + fileName);
        
        Rectangle[] deserializedRectangles = null;
        
        if (File.Exists(fileName))
        {
            string jsonFromFile = File.ReadAllText(fileName);
            deserializedRectangles = JsonSerializer.Deserialize<Rectangle[]>(jsonFromFile);
            
            Console.WriteLine("Дані відновлено. Вміст нового масиву:");
            
            foreach (var rect in deserializedRectangles)
            {
                rect.DisplayInfo();
            }
        }
        else
        {
            Console.WriteLine("Файл для десеріалізації не знайдено.");
        }
    }
}