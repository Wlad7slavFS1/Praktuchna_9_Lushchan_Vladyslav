using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna7
{
    public readonly struct StudentRecord : IEquatable<StudentRecord>
    {
        public string FullName { get; init; }
        public string RecordBookNumber { get; init; }
        public double AverageGrade { get; init; }

        public StudentRecord(string fullName, string id, double grade)
        {
            FullName = fullName;
            RecordBookNumber = id;
            AverageGrade = grade;
        }

        public bool Equals(StudentRecord other) =>
            FullName == other.FullName && RecordBookNumber == other.RecordBookNumber;

        public override bool Equals(object obj) => obj is StudentRecord other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(FullName, RecordBookNumber);

        public static bool operator ==(StudentRecord left, StudentRecord right) => left.Equals(right);
        public static bool operator !=(StudentRecord left, StudentRecord right) => !left.Equals(right);
    }
}
