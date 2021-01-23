using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zero.MultiThread
{
    public class ThreadClass2
    {
        #region 字段属性
        public Hashtable Hashtable { get; set; }

        public ManualResetEvent ManualResetEvent { get; set; }

        public static int pCount = 0;

        public static int pMaxCount = 0;
        #endregion

        #region 构造方法
        public ThreadClass2(int maxCount)
        {
            this.Hashtable = new Hashtable(maxCount);
            pMaxCount = maxCount;
        }
        #endregion

        #region 测试方法

        /// <summary>
        /// 多线程
        /// 测试方法2
        /// </summary>
        public static void ThreadStart()
        {
            Func<int, int, int> fun = TakeAWhile;
            fun.BeginInvoke(0, 1000, TakesAWhileCallBack, fun);//异步调用TakeAWhile,并指定回调函数TakesAWhileCallBack
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(i.ToString());
                Thread.Sleep(100);
            }
            Console.ReadKey();
        }
        #endregion

        #region 基本方法

        /// <summary>
        /// TakeAWhile
        /// </summary>
        /// <param name="data"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        static int TakeAWhile(int data, int times)
        {
            Console.WriteLine("TakeAWhile方法开始执行,时间是:{0}", DateTime.Now);
            Thread.Sleep(times);
            return ++data;
        }

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="fun">调用的委托</param>
        static void TakesAWhileCallBack(IAsyncResult fun)
        {
            if (fun == null)
            {
                throw new ArgumentNullException("fun");
            }
            Func<int, int, int> dl = (Func<int, int, int>)fun.AsyncState;
            int result = dl.EndInvoke(fun);
            Console.WriteLine("我是回调函数返回的结果:{0}", result);
            Console.WriteLine("TakeAWhile执行完成,时间是:{0}", DateTime.Now);
        }
        #endregion
    }
}
