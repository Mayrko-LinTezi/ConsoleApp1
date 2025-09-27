using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    public class ProgramTests
    {
        [Fact] // проверяем разбор строки с кавычками
        public void ParseLineWithQuotes_ShouldSplitCorrectly()
        {
            string input = "Иванов \"Математика\" 2023.05.20 5";
            List<string> result = Program.ParseLineWithQuotes(input);

            Assert.Equal(4, result.Count);
            Assert.Equal("Иванов", result[0]);
            Assert.Equal("Математика", result[1]);
            Assert.Equal("2023.05.20", result[2]);
            Assert.Equal("5", result[3]);
        }

        [Fact] // проверяем, что несуществующий файл возвращает пустой список студентов
        public void ParseStudentsFromFile_FileNotFound_ReturnsEmptyList()
        {
            var students = Program.ParseStudentsFromFile("nofile.txt");

            Assert.Empty(students);
        }

        [Fact] // проверяем студента с низкой оценкой
        public void LowGradeStudents_ShouldDetectLowGrade()
        {
            var students = new List<Student>
            {
                new Student { FullName = "Петров", Subject = "Физика", Date = DateTime.Now, Grade = 2 },
                new Student { FullName = "Сидоров", Subject = "Математика", Date = DateTime.Now, Grade = 5 }
            };

            // перехватим вывод в консоль
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.LowGradeStudents(students);
                string output = sw.ToString();

                Assert.Contains("Петров", output);
                Assert.DoesNotContain("Сидоров", output);
            }
        }
    }
}
