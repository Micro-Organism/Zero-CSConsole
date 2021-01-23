using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.LeetCode
{
    /// <summary>
    /// 每日一题
    /// 第三题
    /// </summary>
    public class LeetCode003
    {
        /// <summary>
        /// 启动执行
        /// </summary>
        public static void LeetCodeRun()
        {
            LeetCode003 leet = new LeetCode003();
            string str = "abcabcbb";//string.Empty;
            leet.LengthOfLongestSubstring(str);
        }

        /// <summary>
        /// 最大长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int LengthOfLongestSubstring(string s)
        {
            int iResponse = 0;
            int iCount = s.ToCharArray().Count() - 1;
            List<char> lstTemps = new List<char>();
            List<List<char>> lstResults = new List<List<char>>();
            //添加字符
            foreach (char item in s)
            {
                if (lstTemps.Contains(item))
                {
                    lstResults.Add(lstTemps.ToList());
                    lstTemps.Clear();
                    lstTemps.Add(item);
                }
                else
                {
                    lstTemps.Add(item);
                }
            }
            lstResults.Add(lstTemps.ToList());

            //遍历整个lstResults
            foreach (var item in lstResults)
            {
                if (item.Count > iResponse)
                {
                    iResponse = item.Count;
                    lstTemps = item;
                }
            }

            return iResponse;
        }
    }

    #region 引用类

    #endregion

}
