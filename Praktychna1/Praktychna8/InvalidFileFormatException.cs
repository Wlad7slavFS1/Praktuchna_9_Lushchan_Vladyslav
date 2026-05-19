using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna8
{
    // Власний виняток для помилок формату файлів 
    public class InvalidFileFormatException : Exception
    {
        public InvalidFileFormatException(string message) : base(message) { }
    }
}
