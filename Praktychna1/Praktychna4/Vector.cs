using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna4
{
    internal class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }

        // 1. Арифметичні оператори 
        public static Vector operator +(Vector v1, Vector v2) => new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        public static Vector operator -(Vector v1, Vector v2) => new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);

        // Скалярне множення 
        public static double operator *(Vector v1, Vector v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

        // 2. Унарні оператори (++ та -- збільшують/зменшують всі координати на 1)
        public static Vector operator ++(Vector v) => new Vector(v.X + 1, v.Y + 1, v.Z + 1);
        public static Vector operator --(Vector v) => new Vector(v.X - 1, v.Y - 1, v.Z - 1);

        // 3. Оператори порівняння (порівнюємо довжини векторів) 
        public static bool operator >(Vector v1, Vector v2) => (double)v1 > (double)v2;
        public static bool operator <(Vector v1, Vector v2) => (double)v1 < (double)v2;

        public static bool operator ==(Vector v1, Vector v2) => v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
        public static bool operator !=(Vector v1, Vector v2) => !(v1 == v2);

        // 4. Оператор приведення типу (explicit) — повертає довжину вектора
        public static explicit operator double(Vector v) => Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);

        // Перевизначення Equals та GetHashCode (вимагається при перевантаженні ==)
        public override bool Equals(object obj) => obj is Vector v && this == v;
        public override int GetHashCode() => (X, Y, Z).GetHashCode();

        public override string ToString() => $"Vector({X}, {Y}, {Z})";
    }
}
