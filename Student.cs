using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Student
    {
        public string FullName { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public int Grade { get; set; }

        public override string ToString()
        {
            return $"Студент: {FullName}, Предмет: {Subject}, Дата: {Date:yyyy.MM.dd}, Оценка: {Grade}";
        }
    }
}