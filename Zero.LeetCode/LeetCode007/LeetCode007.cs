using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***
 *整数反转
*/

namespace Zero.LeetCode
{
    /// <summary>
    /// 每日一题
    /// 第7题 整数反转
    /// </summary>
    public class LeetCode007
    {
        /// <summary>
        /// 启动执行
        /// </summary>
        public static void LeetCodeRun()
        {
            LeetCode007 leet = new LeetCode007();
            int num = -2147483648;//1534236469;//12300
            leet.Reverse(num);
        }

        /// <summary>
        /// 整数反转
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int Reverse(int x)
        {
            try
            {
                if (checked(Math.Abs(x)) < 10)
                {
                    return x;
                }
            }
            catch (OverflowException exc)
            {
                //整数溢出
                return 0;
            }


            int iResponse = 0;
            var temp = Math.Abs(x);

            List<char> lstTemp = temp.ToString().ToCharArray().ToList<char>();
            lstTemp.Reverse();//反转
            for (int index = 0; index < lstTemp.Count; index++)
            {
                if (lstTemp[0] == '0')
                {
                    lstTemp.RemoveAt(0);
                    continue;
                }
                break;
            }

            foreach (var item in lstTemp)
            {
                try
                {
                    iResponse = checked(iResponse * 10 + Int32.Parse(item.ToString()));
                }
                catch (OverflowException exc)
                {
                    //整数溢出
                    return 0;
                }
            }

            return x > 0 ? iResponse : -iResponse;
        }
    }

    #region 引用类

    #endregion

}
