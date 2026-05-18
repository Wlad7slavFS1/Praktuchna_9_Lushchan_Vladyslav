using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna4
{
    public class GradePoint
    {
        // Поле Value від 0 до 10
        public double Value { get; set; }

        public GradePoint(double value)
        {
            // Обмежуємо значення діапазоном 0-10
            Value = value < 0 ? 0 : value > 10 ? 10 : value;
        }

        // 1. Арифметичні оператори
        public static GradePoint operator +(GradePoint g1, GradePoint g2) => new GradePoint(g1.Value + g2.Value);

        // 2. Унарні оператори
        public static GradePoint operator ++(GradePoint g) => new GradePoint(g.Value + 1);
        public static GradePoint operator --(GradePoint g) => new GradePoint(g.Value - 1);

        // 3. Оператори порівняння
        public static bool operator >(GradePoint g1, GradePoint g2) => g1.Value > g2.Value;
        public static bool operator <(GradePoint g1, GradePoint g2) => g1.Value < g2.Value;
        public static bool operator >=(GradePoint g1, GradePoint g2) => g1.Value >= g2.Value;
        public static bool operator <=(GradePoint g1, GradePoint g2) => g1.Value <= g2.Value;

        // 4. Оператори true та false
        // true — якщо оцінка ≥ 8
        public static bool operator true(GradePoint g) => g.Value >= 8;
        public static bool operator false(GradePoint g) => g.Value < 8;

        // 5. Неявне приведення типу (implicit) GradePoint ↔ double
        public static implicit operator double(GradePoint g) => g.Value;
        public static implicit operator GradePoint(double d) => new GradePoint(d);

        public override string ToString() => Value.ToString("F1");
    }
}
