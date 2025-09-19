using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

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

    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "sfustudent.txt";
            List<Student> students = ParseStudentsFromFile(filePath); // читаем студентов в список

            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {students[i]}");
            }

            List<string> printed = new List<string>(); // список уже напечатанных ФИО
            foreach (var student in students)
            {
                if (student.Grade <= 3 && !printed.Contains(student.FullName))
                {
                    Console.WriteLine($"{student.FullName}: {student.Grade}");
                    printed.Add(student.FullName);
                }
            }

            Console.WriteLine("\nКурсы");
            foreach (var c in ParseCoursesFromFile("courses.txt"))
            {
                Console.WriteLine(c);
            }

            Console.WriteLine("\nПреподаватели");
            foreach (var t in ParseTeachersFromFile("teachers.txt"))
            {
                Console.WriteLine(t);
            }

            Console.ReadLine(); // чтобы консоль не закрывалась
        }

        static List<Student> ParseStudentsFromFile(string filePath)
        {
            List<Student> students = new List<Student>();  // список студентов
            string[] lines = File.ReadAllLines(filePath);  // читаем все строки файла

            foreach (string line in lines)
            {
                List<string> parts = ParseLineWithQuotes(line); // разбиваем строку с учётом кавычек

                if (parts.Count >= 4) // проверяем, что в строке есть все данные
                {
                    students.Add(new Student
                    {
                        FullName = parts[0],   // имя студента
                        Subject = parts[1],    // предмет
                        Date = DateTime.ParseExact(parts[2], "yyyy.MM.dd", CultureInfo.InvariantCulture), // дата
                        Grade = int.Parse(parts[3]) // оценка
                    });
                }
            }

            return students;
        }

        static List<Course> ParseCoursesFromFile(string filePath)
        {//обработчик курсов
            List<Course> courses = new List<Course>();
            string[] lines = File.ReadAllLines(filePath);  // читаем все строки файла

            foreach (string line in lines)
            {
                List<string> parts = ParseLineWithQuotes(line);

                if (parts.Count >= 3)
                {
                    courses.Add(new Course
                    {
                        Name = parts[0],              // название курса
                        Credits = int.Parse(parts[1]),// кредиты
                        Department = parts[2]         // кафедра
                    });
                }
            }

            return courses;
        }

        static List<Teacher> ParseTeachersFromFile(string filePath)
        {//обработчик преподавателей
            List<Teacher> teachers = new List<Teacher>();
            string[] lines = File.ReadAllLines(filePath);  // читаем все строки файла

            foreach (string line in lines)
            {
                List<string> parts = ParseLineWithQuotes(line);

                if (parts.Count >= 3)
                {
                    teachers.Add(new Teacher
                    {
                        FullName = parts[0], // ФИО
                        Subject = parts[1],  // предмет
                        HireDate = DateTime.ParseExact(parts[2], "yyyy.MM.dd", CultureInfo.InvariantCulture) // дата
                    });
                }
            }

            return teachers;
        }

        static List<string> ParseLineWithQuotes(string line)
        {// разбиение строк с кавычками
            List<string> parts = new List<string>();
            bool inQuotes = false;                           // флаг внутри кавычек да/нет
            StringBuilder currentPart = new StringBuilder(); // собираем слово по символам

            foreach (char currentChar in line)
            {
                if (currentChar == '"')
                {
                    inQuotes = !inQuotes; // переключаем флаг
                    continue;
                }

                if (currentChar == ' ' && !inQuotes) // пробел вне кавычек
                {
                    if (currentPart.Length > 0)
                    {
                        parts.Add(currentPart.ToString()); // добавляем собранное слово
                        currentPart.Clear(); // очищаем буфер
                    }
                    continue;
                }

                currentPart.Append(currentChar); // добавляем символ к слову
            }

            if (currentPart.Length > 0)
            {
                parts.Add(currentPart.ToString()); // добавляем последнее слово
            }

            return parts;
        }
    }
}
