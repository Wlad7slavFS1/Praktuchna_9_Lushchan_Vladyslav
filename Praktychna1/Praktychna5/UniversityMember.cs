using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktychna1.Praktychna5
{
    public abstract class UniversityMember
    {
        // Абстрактний метод — не має реалізації, кожен дочірній клас зобов'язаний написати свою
        public abstract decimal CalculateScholarship();

        // Віртуальний метод — має базову реалізацію, яку можна перевизначити за бажанням
        public virtual void Enroll()
        {
            Console.WriteLine("Учасника успішно зараховано до університету.");
        }
    }
}