using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Indexer
{
    public class IndexerNames
    {
        static public int size = 10;
        private string[] name_list = new string[size];

        /// <summary>
        /// 构造函数
        /// </summary>
        public IndexerNames()
        {
            for (int index = 0; index < size; index++)
            {
                name_list[index] = "N. A.";
            }
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string this[int index]
        {
            get
            {
                string tmp;

                if (index >= 0 && index <= size - 1)
                {
                    tmp = name_list[index];
                }
                else
                {
                    tmp = "N. A.";
                }

                return (tmp);
            }
            set
            {
                if (index >= 0 && index <= size - 1)
                {
                    name_list[index] = value;
                }
            }
        }

        /// <summary>
        /// 重载索引器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int this[string name]
        {
            get
            {
                int index = 0;
                while (index < size)
                {
                    if (name_list[index] == name)
                    {
                        return index;
                    }
                    index++;
                }
                return index;
            }
        }

    }
}
