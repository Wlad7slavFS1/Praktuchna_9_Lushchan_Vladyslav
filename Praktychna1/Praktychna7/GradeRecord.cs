using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna7
{
    public readonly struct GradeRecord : IEquatable<GradeRecord>
    {
        public string Subject { get; init; }
        public double Score { get; init; }
        public DateTime Date { get; init; }

        public GradeRecord(string subject, double score, DateTime date)
        {
            Subject = subject;
            Score = score;
            Date = date;
        }

        public bool Equals(GradeRecord other) =>
            Subject == other.Subject && Score.Equals(other.Score) && Date.Equals(other.Date);

        public override bool Equals(object obj) => obj is GradeRecord other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Subject, Score, Date);

        public static bool operator ==(GradeRecord left, GradeRecord right) => left.Equals(right);
        public static bool operator !=(GradeRecord left, GradeRecord right) => !left.Equals(right);

        public override string ToString() => $"[{Date:dd.MM.yyyy}] {Subject}: {Score}";

        public void Deconstruct(out string subject, out double score) => (subject, score) = (Subject, Score);
    }
}
