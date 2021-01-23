using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.LeetCode
{
    public class LeetCode002
    {
        /// <summary>
        /// 启动执行
        /// </summary>
        public static void LeetCodeRun()
        {
            LeetCode002 leet = new LeetCode002();
            ListNode l1 = new ListNode();//[2,4,3]
            ListNode l2 = new ListNode();//[5,6,4]
            leet.AddTwoNumbers(l1, l2);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode listNode = new ListNode();

            return listNode;//[7,0,8]
        }
    }


    #region 引用类
    /// <summary>
    /// 引用类
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
    #endregion

}
