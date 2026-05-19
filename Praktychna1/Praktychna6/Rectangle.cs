using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna6
{
    public class Rectangle : Shape, IResizable, IDrawable, IPrintable
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(string name, string color, double width, double height) : base(name, color)
        {
            Width = width;
            Height = height;
        }

        public override double CalculateArea() => Width * Height;

        public override double CalculatePerimeter() => 2 * (Width + Height);

        public override string GetDescription() => $"Прямокутник {Width}x{Height}";

        public void Resize(double factor)
        {
            Width *= factor;
            Height *= factor;
        }

        public void Draw() => Console.WriteLine($"Малюємо прямокутник {Color}...");

        public string GetPrintInfo() => $"{Name} (Прямокутник): S = {CalculateArea():F2}";
    }
}
