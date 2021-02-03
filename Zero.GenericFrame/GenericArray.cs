using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.GenericFrame
{
    public class GenericArray<T>
    {
        /// <summary>
        /// 泛型元素
        /// </summary>
        private T[] array;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="size"></param>
        public GenericArray(int size)
        {
            array = new T[size + 1];
        }

        /// <summary>
        /// 获取元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetItem(int index) 
        {
            return array[index];
        }

        /// <summary>
        /// 设置元素
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetItem(int index, T value) 
        {
            array[index] = value;
        }
    }
}
