using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.LeetCode
{
    public class LeetCodeHelper
    {
        #region 单例模式
        private static LeetCodeHelper _Instance;
        public static LeetCodeHelper Instance 
        {
            get 
            {
                if (_Instance == null)
                {
                    _Instance = new LeetCodeHelper();
                }
                return _Instance;
            }
        }
        #endregion

        #region 静态方法

        /// <summary>
        /// 启动执行
        /// </summary>
        public void LeetCodeStart()
        {
            LeetCode007.LeetCodeRun();
        }

        #endregion

        #region 基本方法

        #endregion
    }
}
