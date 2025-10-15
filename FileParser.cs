using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    public static class FileParser
    {
        // парсинг студентов из файла
        public static List<Student> ParseStudentsFromFile(string filePath)
        {
            List<Student> students = new List<Student>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} не найден!");
                return students;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                try
                {
                    List<string> parts = ParseLineWithQuotes(line);
                    if (parts.Count >= 4)
                    {
                        students.Add(new Student
                        {
                            FullName = parts[0],
                            Subject = parts[1],
                            Date = DateTime.ParseExact(parts[2], "yyyy.MM.dd", CultureInfo.InvariantCulture),
                            Grade = int.Parse(parts[3])
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
        public static List<Course> ParseCoursesFromFile(string filePath)
        {
            List<Course> courses = new List<Course>();

            if (!File.Exists(filePath))
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
                            Name = parts[0],
                            Credits = int.Parse(parts[1]),
                            Department = parts[2]
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
        public static List<Teacher> ParseTeachersFromFile(string filePath)
        {
            List<Teacher> teachers = new List<Teacher>();

            if (!File.Exists(filePath))
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
                            FullName = parts[0],
                            Subject = parts[1],
                            HireDate = DateTime.ParseExact(parts[2], "yyyy.MM.dd", CultureInfo.InvariantCulture)
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
            bool inQuotes = false;
            StringBuilder currentPart = new StringBuilder();

            foreach (char currentChar in line)
            {
                if (currentChar == '"')
                {
                    inQuotes = !inQuotes;
                    continue;
                }

                if (currentChar == ' ' && !inQuotes)
                {
                    if (currentPart.Length > 0)
                    {
                        parts.Add(currentPart.ToString());
                        currentPart.Clear();
                    }
                    continue;
                }

                currentPart.Append(currentChar);
            }

            if (currentPart.Length > 0)
            {
                parts.Add(currentPart.ToString());
            }

            return parts;
        }
    }
}