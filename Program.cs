using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // читаем все объекты из файлов
                var students = FileParser.ParseStudentsFromFile("sfustudent.txt");
                var courses = FileParser.ParseCoursesFromFile("courses.txt");
                var teachers = FileParser.ParseTeachersFromFile("teachers.txt");

                // выводим все объекты
                ConsoleHelper.PrintObjects("Студенты", students);
                StudentService.PrintLowGradeStudents(students);
                ConsoleHelper.PrintObjects("Курсы", courses);
                ConsoleHelper.PrintObjects("Преподаватели", teachers);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка выполнения самой программы: " + ex.Message);
            }
            Console.ReadLine();
        }
    }
}