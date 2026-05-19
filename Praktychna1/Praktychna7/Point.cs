using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna7
{
    // readonly гарантує незмінність і допомагає оптимізації
    public readonly struct Point : IEquatable<Point>
    {
        public int X { get; init; } // Ряд
        public int Y { get; init; } // Місце

        public Point(int x, int y) => (X, Y) = (x, y);

        // Реалізація IEquatable<T> для швидкості (уникнення boxing)
        public bool Equals(Point other) => X == other.X && Y == other.Y;

        public override bool Equals(object obj) => obj is Point other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public static bool operator ==(Point left, Point right) => left.Equals(right);
        public static bool operator !=(Point left, Point right) => !left.Equals(right);

        public override string ToString() => $"[Ряд: {X}, Місце: {Y}]";

        // Deconstruct для зручного використання
        public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
    }
}
