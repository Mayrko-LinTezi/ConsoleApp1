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
            var result = Program.ParseLineWithQuotes(line);
            
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
                var students = Program.ParseStudentsFromFile(testFilePath);
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
            var students = Program.ParseStudentsFromFile("nonexistent_file_12345.txt");
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
                    Program.LowGradeStudents(students);
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
    }
}