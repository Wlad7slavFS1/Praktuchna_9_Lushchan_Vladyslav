using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna6
{
    public abstract class Shape
    {
        // Спільні властивості для всіх фігур
        public string Name { get; set; }
        public string Color { get; set; }

        protected Shape(string name, string color)
        {
            Name = name;
            Color = color;
        }

        // Віртуальні методи: мають базову реалізацію, яку можна змінити
        public virtual double CalculateArea() => 0;
        public virtual double CalculatePerimeter() => 0;

        // Абстрактний метод: не має тіла, обов'язковий для реалізації в нащадках
        public virtual string GetDescription() => $"Фігура: {Name}";
    }
}
