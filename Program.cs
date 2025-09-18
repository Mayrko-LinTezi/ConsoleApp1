using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Data.Common;

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
        public string Name { get; set; }                // Название курса
        public int Credits { get; set; }                // Количество кредитов вывод как целое число
        public string Department { get; set; }          // Кафедра, которой принадлежит курс

        public override string ToString()
        {
            return $"Курс: {Name}, Кредиты: {Credits}, Кафедра: {Department}";
        }
    }

    public class Teacher
    {
        public string FullName { get; set; }            // ФИО преподавателя
        public string Subject { get; set; }             // Предмет, который ведёт преподаватель
        public DateTime HireDate { get; set; }          // Дата приёма на работу тип datetime

        public override string ToString()
        {
            return $"Преподаватель: {FullName}, Предмет: {Subject}, Дата приёма: {HireDate:yyyy.MM.dd}";
        }
    }

    // var biologGrade = student.Grades.First(g => g.Subject == "Биология"); Console.WriteLine($"{student.Name}: Биология - {biologGrade.Score}");
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "sfustudent.txt";

            List<Student> students = ParseStudentsFromFile(filePath);       //чтение файла и созд. список студов

            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {students[i]}");
            }
            List<string> studentsone = new List<string>();

            foreach (var student in students)
            {
                if (student.Grade <= 3)
                {
                    if (!studentsone.Contains(student.FullName))
                    {
                        Console.WriteLine($"{student.FullName}: {student.Grade}");
                        studentsone.Add(student.FullName);
                    }
                }
            }
            Console.ReadLine();
        }

        static List<Student> ParseStudentsFromFile(string filePath)     // возвращает список студентов
        {
            List<Student> students = new List<Student>();          //список для студентов
            string[] lines = File.ReadAllLines(filePath);        // read строк из файла в массив строк

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                List<string> parts = ParseLineWithQuotes(line);     // разбивает строку на части, учитываем кавычки

                if (parts.Count >= 4) // цикл обработчик 
                {
                    Student student = new Student  // создаем объект
                    {
                        FullName = parts[0],
                        Subject = parts[1],
                        Date = DateTime.ParseExact(parts[2], "yyyy.MM.dd", CultureInfo.InvariantCulture),//CultureInfo.InvariantCulture отказ от настроек ПК
                        Grade = int.Parse(parts[3])
                    };

                    students.Add(student);
                }
            }

            return students;
        }
        //пер. для хран. тек. символ                          поддерж. опер. срав.
        static List<string> ParseLineWithQuotes(string line)  // разбив на части
        {
            List<string> parts = new List<string>();
            bool inQuotes = false;                            // находимся ли мы внутри кавычек да/нет
            StringBuilder currentPart = new StringBuilder();  // образуем переменную, где собир. часть строки символ за символ. в самом цикле

            for (int i = 0; i < line.Length; i++)               // это сам цикл 
            {
                char currentChar = line[i];                     //+

                if (currentChar == '"')
                {
                    inQuotes = !inQuotes;                        // переключение true или false. ! отрицание (не)
                    continue;
                }

                if (currentChar == ' ' && !inQuotes)
                {
                    if (currentPart.Length > 0)                  // проверка на накопилось ли слово, дойдя до пробела сохран. плучившиеся слово
                    {
                        parts.Add(currentPart.ToString());      // добавляем получиашиеся слово в список parts
                        currentPart.Clear();          // очищ. получ. слово и нач. новое слово
                    }
                    continue;
                }

                currentPart.Append(currentChar);     // если пробел вне кавычек, то добавляем к слову
            }

            if (currentPart.Length > 0)             // цикл добав. словотолько тогда, когда встр пробел вне кавычек
            {
                parts.Add(currentPart.ToString()); // если не пустой, то добавляется
            }

            return parts;
        }
    }
}
