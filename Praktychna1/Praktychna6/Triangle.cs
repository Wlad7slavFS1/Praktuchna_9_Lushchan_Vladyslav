using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna6
{
    public class Triangle : Shape, IResizable, IDrawable, IPrintable
    {
        public double SideA { get; set; }
        public double SideB { get; set; }
        public double SideC { get; set; }

        public Triangle(string name, string color, double a, double b, double c) : base(name, color)
        {
            SideA = a; SideB = b; SideC = c;
        }

        public override double CalculateArea()
        {
            double p = CalculatePerimeter() / 2;
            return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC));
        }

        public override double CalculatePerimeter() => SideA + SideB + SideC;

        public override string GetDescription() => $"Трикутник зі сторонами {SideA}, {SideB}, {SideC}";

        public void Resize(double factor) { SideA *= factor; SideB *= factor; SideC *= factor; }

        public void Draw() => Console.WriteLine($"Малюємо трикутник {Color}...");

        public string GetPrintInfo() => $"{Name} (Трикутник): S = {CalculateArea():F2}";
    }
}
