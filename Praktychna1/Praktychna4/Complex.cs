using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna4
{
    public class Complex
    {
        // Властивості для дійсної та уявної частин
        public double Real { get; set; }
        public double Imaginary { get; set; }

        // Конструктор
        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        // 1. Арифметичні оператори (+, -, *, /)
        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }

        public static Complex operator -(Complex c1, Complex c2)
        {
            return new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
        }

        public static Complex operator *(Complex c1, Complex c2)
        {
            // Формула: (a + bi)*(c + di) = (ac - bd) + (bc + ad)i
            double realPart = c1.Real * c2.Real - c1.Imaginary * c2.Imaginary;
            double imaginaryPart = c1.Real * c2.Imaginary + c1.Imaginary * c2.Real;
            return new Complex(realPart, imaginaryPart);
        }

        public static Complex operator /(Complex c1, Complex c2)
        {
            double denominator = c2.Real * c2.Real + c2.Imaginary * c2.Imaginary;
            if (denominator == 0)
                throw new DivideByZeroException("Ділення на нуль (знаменник комплексного числа дорівнює 0).");

            // Формула ділення комплексних чисел
            double realPart = (c1.Real * c2.Real + c1.Imaginary * c2.Imaginary) / denominator;
            double imaginaryPart = (c1.Imaginary * c2.Real - c1.Real * c2.Imaginary) / denominator;
            return new Complex(realPart, imaginaryPart);
        }

        // 2. Оператори порівняння (==, !=)
        public static bool operator ==(Complex c1, Complex c2)
        {
            if (ReferenceEquals(c1, c2)) return true;
            if (c1 is null || c2 is null) return false;
            return c1.Real == c2.Real && c1.Imaginary == c2.Imaginary;
        }

        public static bool operator !=(Complex c1, Complex c2) => !(c1 == c2);

        // 3. Перетворення типів (Приведення)
        // Неявне (implicit): double автоматично стає Complex (уявна частина = 0)
        public static implicit operator Complex(double d)
        {
            return new Complex(d, 0);
        }

        // Явне (explicit): повертає модуль комплексного числа (double)
        public static explicit operator double(Complex c)
        {
            return Math.Sqrt(c.Real * c.Real + c.Imaginary * c.Imaginary);
        }

        // Текстове представлення для зручного виводу в консоль
        public override string ToString()
        {
            if (Imaginary >= 0)
                return $"{Real} + {Imaginary}i";
            else
                return $"{Real} - {Math.Abs(Imaginary)}i";
        }

        // Перевизначення стандартних методів через роботу оператора ==
        public override bool Equals(object obj) => obj is Complex c && this == c;
        public override int GetHashCode() => (Real, Imaginary).GetHashCode();
    }
}