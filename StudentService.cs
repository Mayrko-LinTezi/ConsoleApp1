using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class StudentService
    {
        // вывод студентов с оценкой <=3
        public static void PrintLowGradeStudents(List<Student> students)
        {
            List<string> printed = new List<string>();
            foreach (var student in students)
            {
                if (student.Grade <= 3 && !printed.Contains(student.FullName))
                {
                    Console.WriteLine($"{student.FullName}: {student.Grade}");
                    printed.Add(student.FullName);
                }
            }
        }
    }
}