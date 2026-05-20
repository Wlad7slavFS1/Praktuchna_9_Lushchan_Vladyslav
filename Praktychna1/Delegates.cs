using Praktychna1.Praktychna1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1
{
    public class Delegates
    {

        // Делегат для операцій над одним студентом (наприклад, зміна статусу, друк інфо)
        public delegate void StudentOperation(Student student);

        // Делегат для операцій над групою (наприклад, генерація специфічного звіту)
        public delegate string GroupOperation(StudentGroup group);
    }
}
