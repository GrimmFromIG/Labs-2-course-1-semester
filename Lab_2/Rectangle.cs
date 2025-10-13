using System;

namespace Shapes
{
    public class Rectangle : IComparable<Rectangle>
    {
        public string ColorFill { get; set; }
        public string ColorBorder { get; set; }
        public double A { get; set; }
        public double B { get; set; }

        public Rectangle(string colorFill, string colorBorder, double a, double b)
        {
            ColorFill = colorFill;
            ColorBorder = colorBorder;
            A = a;
            B = b;
        }

        public double GetArea() => A * B;
        public double GetPerimeter() => 2 * (A + B);

        public override string ToString()
        {
            return $"Прямокутник: {A}x{B}, площа={GetArea():F2}, периметр={GetPerimeter():F2}, " +
                   $"заливка={ColorFill}, контур={ColorBorder}";
        }

        public int CompareTo(Rectangle other)
        {
            if (other == null) return 1;
            return GetArea().CompareTo(other.GetArea());
        }
    }
}