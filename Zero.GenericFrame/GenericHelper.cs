using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.GenericFrame
{
    public class GenericHelper
    {
        #region 单例模式
        private static GenericHelper _Instance;
        public static GenericHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GenericHelper();
                }
                return _Instance;
            }
        }
        #endregion

        #region 静态方法
        /// <summary>
        /// 交换元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">源数据</param>
        /// <param name="target">目标数据</param>
        public static void Swap<T>(ref T source, ref T target)
        {
            T temp;
            temp = source;
            source = target;
            target = temp;
        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 启动执行
        /// </summary>
        public void HelperRun()
        {
            // 声明一个整型数组
            GenericArray<int> intArray = new GenericArray<int>(5);
            // 设置值
            for (int index = 0; index < 5; index++)
            {
                intArray.SetItem(index, index * 5);
            }
            // 获取值
            for (int index = 0; index < 5; index++)
            {
                Console.Write(intArray.GetItem(index) + " ");
            }
            Console.WriteLine();

            // 声明一个字符数组
            GenericArray<char> charArray = new GenericArray<char>(5);
            // 设置值
            for (int index = 0; index < 5; index++)
            {
                charArray.SetItem(index, (char)(index + 97));
            }
            // 获取值
            for (int index = 0; index < 5; index++)
            {
                Console.Write(charArray.GetItem(index) + " ");
            }
            Console.WriteLine();
            Console.ReadKey();
        }
        #endregion
    }
}
