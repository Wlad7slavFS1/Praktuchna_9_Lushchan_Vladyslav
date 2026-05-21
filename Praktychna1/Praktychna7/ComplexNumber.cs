using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna7
{
    public readonly struct ComplexNumber : IEquatable<ComplexNumber>
    {
        public double Real { get; init; }
        public double Imaginary { get; init; }

        public ComplexNumber(double real, double imaginary) => (Real, Imaginary) = (real, imaginary);

        public bool Equals(ComplexNumber other) =>
            Real.Equals(other.Real) && Imaginary.Equals(other.Imaginary);

        public override bool Equals(object obj) => obj is ComplexNumber other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Real, Imaginary);

        public static bool operator ==(ComplexNumber left, ComplexNumber right) => left.Equals(right);
        public static bool operator !=(ComplexNumber left, ComplexNumber right) => !left.Equals(right);

        public override string ToString() => $"{Real} + {Imaginary}i";

        public void Deconstruct(out double r, out double i) => (r, i) = (Real, Imaginary);
    }
}
