using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna6
{
    public class Square : Rectangle
    {
        // Передаємо значення сторони як ширину і висоту в базовий конструктор
        public Square(string name, string color, double side) : base(name, color, side, side) { }

        public override string GetDescription() => $"Квадрат зі стороною {Width}";
    }
}
