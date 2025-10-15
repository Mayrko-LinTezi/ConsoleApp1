using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Course
    {
        public string Name { get; set; }
        public int Credits { get; set; }
        public string Department { get; set; }

        public override string ToString()
        {
            return $"Курс: {Name}, Кредиты: {Credits}, Кафедра: {Department}";
        }
    }
}