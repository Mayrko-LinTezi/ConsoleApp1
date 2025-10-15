using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class ConsoleHelper
    {
        public static void PrintObjects<T>(string header, List<T> objects)
        {
            Console.WriteLine($"\n{header}");
            foreach (var ob in objects)
            {
                Console.WriteLine(ob);
            }
        }
    }
}