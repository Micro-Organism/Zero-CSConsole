using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.DataBases
{
    public class DatabaseHelper
    {
        #region 单例模式

        private static DatabaseHelper _Instance;
        public static DatabaseHelper Instance 
        {
            get 
            {
                if (_Instance == null)
                {
                    _Instance = new DatabaseHelper();
                }
                return _Instance;
            }
        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 启动执行
        /// </summary>
        public void DatabaseStart()
        {
            SQLServerHelper.SQLServerRun();
        }

        #endregion

        #region 基本方法

        #endregion
    }
}
