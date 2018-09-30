using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace program1
{
    public delegate void TimeoutHander(object sender,DateTime date);
    public class MyClock
    {
        public event TimeoutHander TimeOut;
        public void Timeout(DateTime date)
        {
            TimeOut(this,date);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请设置闹钟时间：");
            int year,month,day,h, m, s;
            string year1,month1,day1,h1, m1, s1;
            Console.Write("年：");
            year1 = Console.ReadLine();
            Console.Write("月：");
            month1 = Console.ReadLine();
            Console.Write("日：");
            day1 = Console.ReadLine();
            Console.Write("时：");
            h1 = Console.ReadLine();
            Console.Write("分：");
            m1 = Console.ReadLine();
            Console.Write("秒：");
            s1 = Console.ReadLine();
            try
            {
                year = int.Parse(year1);
                month = int.Parse(month1);
                day = int.Parse(day1);
                h = int.Parse(h1);
                m = int.Parse(m1);
                s = int.Parse(s1);
            }
            catch
            {
                Console.WriteLine("输入错误，请输入整数!");
                return ;
            }
            Console.WriteLine("闹钟已启动，请勿关闭！");
            DateTime date1 = DateTime.Now;
            DateTime date2 = new DateTime(year,month,day,h,m,s);
            TimeSpan ts = date2 - date1;
            MyClock clock = new MyClock();
            clock.TimeOut += new TimeoutHander(timeOut1);
            clock.TimeOut += new TimeoutHander(timeOut2);
            //Console.WriteLine(ts.Days+" "+ts.Hours+" "+ts.Minutes+" "+ts.Seconds);
            Thread.Sleep(ts);
            clock.Timeout(date2);
        }
        static void timeOut1(object sender,DateTime date)
        {
            Console.WriteLine("时间到了！！！叮叮叮！！！");
        }
        static void timeOut2(object sendee,DateTime date)
        {
            Console.WriteLine("现在的时间是："+date);
        }
    }
}
