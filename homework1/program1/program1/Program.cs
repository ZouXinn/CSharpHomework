using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program1
{
    class Program
    {
        static void Main(string[] args)
        {
            int a, b;
            Console.Write("please enter the first int:");
            a = Int32.Parse(Console.ReadLine());
            Console.Write("please enter the second int:");
            b = Int32.Parse(Console.ReadLine());
            Console.WriteLine($"{a} * {b} = {a * b}");
        }
    }
}
