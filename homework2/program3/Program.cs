using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = new int[99];
            for(int i = 0; i < 99;i++)
            {
                nums[i] = i+2;
            }
            int max = 99 ,del =0,n = 99;
            for(int i = 2; i<= 100; i++)
            {
                for(int j =0; j <n;j++)
                {
                    if(nums[j]/i>=2&&nums[j]%i==0)
                    {
                        del++;
                        max--;
                    }
                    else
                    {                       
                        nums[j-del] = nums[j];
                    }
                }
                del = 0;
                n = max;
            }
            Console.WriteLine("2到100内的素数有：");
            for(int i = 0; i <max; i++)
            {
                Console.Write(nums[i] + " ");
            }
        }
    }
}
