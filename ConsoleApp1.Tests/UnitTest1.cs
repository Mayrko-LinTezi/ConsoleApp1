using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp1;

namespace ConsoleApp1.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void ParseLineWithQuotes_ShouldParseCorrectly()
        {
            string line = "John \"Math Programming\" 2023.10.05 5";
            var result = FileParser.ParseLineWithQuotes(line); // Исправлено!

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual("John", result[0]);
            Assert.AreEqual("Math Programming", result[1]);
            Assert.AreEqual("2023.10.05", result[2]);
            Assert.AreEqual("5", result[3]);
        }

        [TestMethod]
        public void ParseStudentsFromFile_ShouldParseValidFile()
        {
            string testFilePath = "test_students.txt";
            File.WriteAllText(testFilePath, "Иванов \"Математика\" 2023.10.05 5");

            try
            {
                var students = FileParser.ParseStudentsFromFile(testFilePath); // Исправлено!
                Assert.AreEqual(1, students.Count);
                Assert.AreEqual("Иванов", students[0].FullName);
                Assert.AreEqual("Математика", students[0].Subject);
                Assert.AreEqual(new DateTime(2023, 10, 5), students[0].Date);
                Assert.AreEqual(5, students[0].Grade);
            }
            finally
            {
                if (File.Exists(testFilePath))
                    File.Delete(testFilePath);
            }
        }

        [TestMethod]
        public void ParseStudentsFromFile_ShouldHandleMissingFile()
        {
            var students = FileParser.ParseStudentsFromFile("nonexistent_file_12345.txt"); // Исправлено!
            Assert.AreEqual(0, students.Count);
        }

        [TestMethod]
        public void LowGradeStudents_ShouldFilterCorrectly()
        {
            var students = new List<Student>
            {
                new Student { FullName = "Алиса", Grade = 2 },
                new Student { FullName = "Боб", Grade = 5 },
                new Student { FullName = "Карл", Grade = 3 }
            };

            using (var sw = new StringWriter())
            {
                var originalOutput = Console.Out;
                Console.SetOut(sw);

                try
                {
                    StudentService.PrintLowGradeStudents(students); // Исправлено!
                    string result = sw.ToString().Trim();

                    Assert.IsTrue(result.Contains("Алиса"));
                    Assert.IsTrue(result.Contains("Карл"));
                    Assert.IsFalse(result.Contains("Боб"));
                }
                finally
                {
                    Console.SetOut(originalOutput);
                }
            }
        }

        [TestMethod]
        public void Student_ToString_ReturnsCorrectFormat()
        {
            var student = new Student
            {
                FullName = "Иванов",
                Subject = "Математика",
                Date = new DateTime(2023, 10, 5),
                Grade = 5
            };

            var result = student.ToString();
            Assert.AreEqual("Студент: Иванов, Предмет: Математика, Дата: 2023.10.05, Оценка: 5", result);
        }

        // Дополнительные тесты для новых классов
        [TestMethod]
        public void Course_ToString_ReturnsCorrectFormat()
        {
            var course = new Course
            {
                Name = "Математика",
                Credits = 5,
                Department = "Высшей математики"
            };

            var result = course.ToString();
            Assert.AreEqual("Курс: Математика, Кредиты: 5, Кафедра: Высшей математики", result);
        }

        [TestMethod]
        public void Teacher_ToString_ReturnsCorrectFormat()
        {
            var teacher = new Teacher
            {
                FullName = "Петров И.И.",
                Subject = "Математика",
                HireDate = new DateTime(2020, 9, 1)
            };

            var result = teacher.ToString();
            Assert.AreEqual("Преподаватель: Петров И.И., Предмет: Математика, Дата приёма: 2020.09.01", result);
        }

        [TestMethod]
        public void ParseCoursesFromFile_ShouldParseValidFile()
        {
            string testFilePath = "test_courses.txt";
            File.WriteAllText(testFilePath, "\"Высшая математика\" 5 \"Кафедра математики\"");

            try
            {
                var courses = FileParser.ParseCoursesFromFile(testFilePath);
                Assert.AreEqual(1, courses.Count);
                Assert.AreEqual("Высшая математика", courses[0].Name);
                Assert.AreEqual(5, courses[0].Credits);
                Assert.AreEqual("Кафедра математики", courses[0].Department);
            }
            finally
            {
                if (File.Exists(testFilePath))
                    File.Delete(testFilePath);
            }
        }

        [TestMethod]
        public void ParseTeachersFromFile_ShouldParseValidFile()
        {
            string testFilePath = "test_teachers.txt";
            File.WriteAllText(testFilePath, "\"Петров И.И.\" \"Математика\" 2020.09.01");

            try
            {
                var teachers = FileParser.ParseTeachersFromFile(testFilePath);
                Assert.AreEqual(1, teachers.Count);
                Assert.AreEqual("Петров И.И.", teachers[0].FullName);
                Assert.AreEqual("Математика", teachers[0].Subject);
                Assert.AreEqual(new DateTime(2020, 9, 1), teachers[0].HireDate);
            }
            finally
            {
                if (File.Exists(testFilePath))
                    File.Delete(testFilePath);
            }
        }
    }
}