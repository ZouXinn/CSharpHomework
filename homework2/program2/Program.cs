using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = new int[10] { 5, 9, 56, 32, 6, 54, 8, 6, 99, 1 };
            int max = nums[0], min = nums[0];
            double ave = 0.0, sum = 0.0;
            for (int i =0; i< nums.Length;i++)
            {
                if(nums[i]>max)
                    max = nums[i];
                if (nums[i] < min)
                    min = nums[i];
                sum += nums[i];
            }
            ave = sum / nums.Length;
            Console.Write($"数组的最大值为{max}，最小值为{min}，和为{sum}，平均值为{ave}");
        }
    }
}
