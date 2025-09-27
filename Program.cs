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

    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // читаем все объекты из файлов
                var students = ParseStudentsFromFile("sfustudent.txt");                                                                                                  // студенты
                var courses = ParseCoursesFromFile("courses.txt");                                                                                                       // курсы
                var teachers = ParseTeachersFromFile("teachers.txt");                                                                                                    // преподаватели

                // выводим все объекты
                PrintObjects("Студенты", students);
                LowGradeStudents(students);                                                                                                                             // вывод студентов с оценкой <=3
                PrintObjects("Курсы", courses);
                PrintObjects("Преподаватели", teachers);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка выполнения самой программы: " + ex.Message);
            }
            Console.ReadLine();                                                                                                                                   // чтобы консоль не закрывалась
        }

                                                                                                                                                                    // не отдельная функция вывода списка объектови заголовокв
        static void PrintObjects<T>(string header, List<T> objects)
        {
            Console.WriteLine($"\n{header}");
            foreach (var ob in objects)
            {
                Console.WriteLine(ob);
            }
        }

                                                                                                                                                                    // отдельная функция для студентов с оценкой <=3
        public static void LowGradeStudents(List<Student> students)
        {
            List<string> printed = new List<string>();                                                                                                            // список уже напечатанных ФИО
            foreach (var student in students)
            {
                if (student.Grade <= 3 && !printed.Contains(student.FullName))
                {
                    Console.WriteLine($"{student.FullName}: {student.Grade}");
                    printed.Add(student.FullName);
                }
            }
        }

        // парсинг студентов из файла
        public static List<Student> ParseStudentsFromFile(string filePath)
        {
            List<Student> students = new List<Student>();

            if (!File.Exists(filePath)) // проверка на существование файла
            {
                Console.WriteLine($"Файл {filePath} не найден!");
                return students; // возвращаем пустой список
            }

            string[] lines = File.ReadAllLines(filePath); // читаем все строки файла

            foreach (string line in lines)
            {
                try // оборачиваем каждую строку в try/catch
                {
                    List<string> parts = ParseLineWithQuotes(line); // разбиваем строку с учётом кавычек
                    if (parts.Count >= 4) // проверяем, что в строке есть все данные
                    {
                        students.Add(new Student
                        {
                            FullName = parts[0], // имя студента
                            Subject = parts[1],  // предмет
                            Date = DateTime.ParseExact(parts[2], "yyyy.MM.dd", CultureInfo.InvariantCulture), // дата
                            Grade = int.Parse(parts[3]) // оценка
                        });
                    }
                    else
                    {
                        Console.WriteLine($"Строка пропущена (недостаточно данных): {line}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка обработки строки: {line}. {ex.Message}");
                }
            }
            return students;
        }

        // парсинг курсов из файла
        static List<Course> ParseCoursesFromFile(string filePath)
        {
            List<Course> courses = new List<Course>();

            if (!File.Exists(filePath)) // проверка на файл
            {
                Console.WriteLine($"Файл {filePath} не найден!");
                return courses;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                try
                {
                    List<string> parts = ParseLineWithQuotes(line);
                    if (parts.Count >= 3)
                    {
                        courses.Add(new Course
                        {
                            Name = parts[0],                  // название курса
                            Credits = int.Parse(parts[1]),    // кредиты
                            Department = parts[2]             // кафедра
                        });
                    }
                    else
                    {
                        Console.WriteLine($"Строка пропущена (недостаточно данных): {line}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка обработки строки: {line}. {ex.Message}");
                }
            }
            return courses;
        }

        // парсинг преподавателей из файла
        static List<Teacher> ParseTeachersFromFile(string filePath)
        {
            List<Teacher> teachers = new List<Teacher>();

            if (!File.Exists(filePath)) // проверка файла
            {
                Console.WriteLine($"Файл {filePath} не найден!");
                return teachers;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                try
                {
                    List<string> parts = ParseLineWithQuotes(line);
                    if (parts.Count >= 3)
                    {
                        teachers.Add(new Teacher
                        {
                            FullName = parts[0], // ФИО
                            Subject = parts[1],  // предмет
                            HireDate = DateTime.ParseExact(parts[2], "yyyy.MM.dd", CultureInfo.InvariantCulture) // дата приёма
                        });
                    }
                    else
                    {
                        Console.WriteLine($"Строка пропущена (недостаточно данных): {line}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка обработки строки: {line}. {ex.Message}");
                }
            }
            return teachers;
        }

        // разбиение строк с учётом кавычек
        public static List<string> ParseLineWithQuotes(string line)
        {
            List<string> parts = new List<string>();
            bool inQuotes = false;                                                                                                                                        // флаг внутри кавычек да/нет
            StringBuilder currentPart = new StringBuilder();                                                                                                             // собираем слово по символам

            foreach (char currentChar in line)
            {
                if (currentChar == '"')
                {
                    inQuotes = !inQuotes;                                                                                                                                 // переключаем флаг
                    continue;
                }

                if (currentChar == ' ' && !inQuotes)                                                                                                                      // пробел вне кавычек
                {
                    if (currentPart.Length > 0)
                    {
                        parts.Add(currentPart.ToString());                                                                                                               // добавляем собранное слово
                        currentPart.Clear();                                                                                                                             // очищаем буфер
                    }
                    continue;
                }

                currentPart.Append(currentChar);                                                                                                                           // добавляем символ к слову
            }

            if (currentPart.Length > 0)
            {
                parts.Add(currentPart.ToString());                                                                                                               // добавляем последнее слово
            }

            return parts;
        }
    }
}
