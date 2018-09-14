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
            Console.Write("请输入一个正整数x，我将求出1到x内的所有素数：");
            string s = Console.ReadLine();
            int n = 0;
            bool ok = false;
            while (!ok)
            {
                if (!Int32.TryParse(s, out n))
                {
                    Console.Write("输入格式错误，请重新输入整数：");
                    s = Console.ReadLine();
                }
                else
                {
                    ok = true;
                }
            }
            Console.WriteLine($"1到{n}之间的素数有：");
            for(int i = 2; i <= n; i++)
            {
                if(i == 2 || i == 3)
                {
                    Console.Write(i + " ");
                    continue;
                }
                for(int j = 2; j <= i/2;j++)
                {
                    if(i%j==0)
                    {
                        break;
                    }
                    if(j == i/2)
                    {
                        Console.Write(i + " ");
                        break;
                    }
                }
            }
        }
    }
}
