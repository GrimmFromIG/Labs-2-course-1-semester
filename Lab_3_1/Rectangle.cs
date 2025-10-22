using System;
public class Rectangle
{
    public string FillColor { get; set; } 
    public string BorderColor { get; set; }
    public double SideA { get; set; }
    public double SideB { get; set; }
    
    public double Area => SideA * SideB;
    public double Perimeter => 2 * (SideA + SideB);
    
    public Rectangle() { }

    public Rectangle(string fillColor, string borderColor, double sideA, double sideB)
    {
        FillColor = fillColor;
        BorderColor = borderColor;
        SideA = sideA;
        SideB = sideB;
    }
    
    public void DisplayInfo()
    {
        Console.WriteLine($"Прямокутник:");
        Console.WriteLine($"  Колір: {FillColor} (контур: {BorderColor})");
        Console.WriteLine($"  Сторони: {SideA} x {SideB}");
        Console.WriteLine($"  Площа: {Area}");
        Console.WriteLine($"  Периметр: {Perimeter}");
        Console.WriteLine(new string('-', 20));
    }
}