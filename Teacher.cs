using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Teacher
    {
        public string FullName { get; set; }
        public string Subject { get; set; }
        public DateTime HireDate { get; set; }

        public override string ToString()
        {
            return $"Преподаватель: {FullName}, Предмет: {Subject}, Дата приёма: {HireDate:yyyy.MM.dd}";
        }
    }
}