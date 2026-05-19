using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna7
{
    public readonly struct DateRange : IEquatable<DateRange>
    {
        public DateTime Start { get; init; }
        public DateTime End { get; init; }

        public DateRange(DateTime start, DateTime end)
        {
            if (end < start)
                throw new ArgumentException("Дата завершення не може бути раніше дати початку.");
            Start = start;
            End = end;
        }

        public bool Equals(DateRange other) => Start == other.Start && End == other.End;
        public override bool Equals(object obj) => obj is DateRange other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(Start, End);

        public int TotalDays => (End - Start).Days;

        public static bool operator ==(DateRange left, DateRange right) => left.Equals(right);
        public static bool operator !=(DateRange left, DateRange right) => !left.Equals(right);
        public static bool operator >(DateRange left, DateRange right) => left.TotalDays > right.TotalDays;
        public static bool operator <(DateRange left, DateRange right) => left.TotalDays < right.TotalDays;

        public override string ToString() => $"{Start:dd.MM.yyyy} - {End:dd.MM.yyyy} ({TotalDays} днів)";

        public bool Includes(DateTime date) => date >= Start && date <= End;
        public void Deconstruct(out DateTime s, out DateTime e) => (s, e) = (Start, End);
    }
}
