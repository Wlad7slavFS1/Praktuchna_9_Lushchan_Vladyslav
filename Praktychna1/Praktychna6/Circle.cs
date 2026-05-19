using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna6
{
    public class Circle : Shape, IResizable, IDrawable, IPrintable
    {
        public double Radius { get; set; }

        public Circle(string name, string color, double radius) : base(name, color)
        {
            Radius = radius;
        }

        public override double CalculateArea() => Math.PI * Math.Pow(Radius, 2);

        public override double CalculatePerimeter() => 2 * Math.PI * Radius;

        public override string GetDescription() => $"Коло з радіусом {Radius:F2}";

        public void Resize(double factor) => Radius *= factor;

        public void Draw() => Console.WriteLine($"Малюємо коло {Color} кольору...");

        public string GetPrintInfo() => $"{Name} (Коло): Площа = {CalculateArea():F2}, Периметр = {CalculatePerimeter():F2}";
    }
}
