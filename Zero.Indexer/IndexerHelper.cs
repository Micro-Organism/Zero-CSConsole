using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Indexer
{
    public class IndexerHelper
    {
        #region 单例模式
        private static IndexerHelper _Instance;
        public static IndexerHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new IndexerHelper();
                }
                return _Instance;
            }
        }
        #endregion

        #region 静态方法

        #endregion

        #region 基本方法
        /// <summary>
        /// 启动执行
        /// </summary>
        public void HelperRun() 
        {
            IndexerNames names = new IndexerNames();
            names[0] = "Zara";
            names[1] = "Riz";
            names[2] = "Nuha";
            names[3] = "Asif";
            names[4] = "Davinder";
            names[5] = "Sunil";
            names[6] = "Rubic";
            // 使用带有 int 参数的第一个索引器
            for (int index = 0; index < IndexerNames.size; index++)
            {
                Console.WriteLine(names[index]);
            }

            // 使用带有 string 参数的第二个索引器
            for (int index = 0; index < IndexerNames.size; index++)
            {
                Console.WriteLine(names[names[index]]);
            }
            Console.ReadKey();
        }
        #endregion
    }
}
