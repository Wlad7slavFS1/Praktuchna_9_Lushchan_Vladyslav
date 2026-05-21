using Praktychna1.Praktychna1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna9
{
    // Клас для передачі даних про студента в подіях
    public class StudentEventArgs : EventArgs
    {
        public Student Student { get; }
        public string Message { get; }

        public StudentEventArgs(Student student, string message)
        {
            Student = student;
            Message = message;
        }
    }
}
