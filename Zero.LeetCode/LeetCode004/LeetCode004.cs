using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***
 *给定两个大小为 m 和 n 的正序（从小到大）数组 nums1 和 nums2。请你找出并返回这两个正序数组的中位数。
 *进阶：你能设计一个时间复杂度为 O(log (m+n)) 的算法解决此问题吗？
*/

namespace Zero.LeetCode
{
    /// <summary>
    /// 每日一题
    /// 第四题 寻找两个正序数组的中位数
    /// </summary>
    public class LeetCode004
    {
        /// <summary>
        /// 启动执行
        /// </summary>
        public static void LeetCodeRun()
        {
            LeetCode004 leet = new LeetCode004();
            int[] nums1 = new int[] { 1, 2 };
            int[] nums2 = new int[] { 3, 4 };
            leet.FindMedianSortedArrays(nums1, nums2);
        }

        /// <summary>
        /// 寻找两个正序数组的中位数
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            List<int> lstNums1 = nums1.ToList<int>();
            foreach (var item in nums2)
            {
                lstNums1.Add(item);
            }
            var nums = lstNums1.OrderBy(t => t).ToArray();
            if (nums.Length % 2 == 0)
            {
                //偶数
                return (nums[nums.Length / 2 - 1] + nums[nums.Length / 2]) / 2.00000;
            }
            else
            {
                //奇数
                return nums[nums.Length / 2];
            }
        }
    }

    #region 引用类

    #endregion

}
