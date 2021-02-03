using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.ReflectionFrame
{
    [AttributeUsage(AttributeTargets.All)]
    public class ReflectionHelper : System.Attribute
    {
        #region 单例模式
        private static ReflectionHelper _Instance;
        public static ReflectionHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ReflectionHelper();
                }
                return _Instance;
            }
        }
        #endregion

        #region 字段属性
        public readonly string Url;        
        private string _Topic;
        public string Topic
        {
            get
            {
                return _Topic;
            }
            set
            {
                _Topic = value;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        ///  构造函数
        /// </summary>
        public ReflectionHelper()
        {

        }
        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="url">定位（positional）参数</param>
        public ReflectionHelper(string url)
        {
            this.Url = url;
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
            System.Reflection.MemberInfo info = typeof(ReflectionHelper);
            object[] attributes = info.GetCustomAttributes(true);
            for (int index = 0; index < attributes.Length; index++)
            {
                Console.WriteLine(attributes[index]);
            }
            Console.ReadKey();
        }
        #endregion
    }
}
