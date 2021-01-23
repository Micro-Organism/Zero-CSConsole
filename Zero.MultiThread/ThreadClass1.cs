using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zero.MultiThread
{
    public class ThreadClass1
    {
        #region 字段属性
        public Hashtable Hashtable { get; set; }

        public ManualResetEvent ManualResetEvent { get; set; }

        public static int pCount = 0;

        public static int pMaxCount = 0;
        #endregion

        #region 构造方法
        public ThreadClass1(int maxCount)
        {
            this.Hashtable = new Hashtable(maxCount);
            pMaxCount = maxCount;
        }
        #endregion

        #region 测试方法
        /// <summary>
        /// 多线程
        /// 测试方法1
        /// </summary>
        public static void ThreadStart()
        {
            bool enableThreadPool = false;
            int iMaxCount = 20;
            ManualResetEvent aManualResetEvent = new ManualResetEvent(false);

            Console.WriteLine("Insert {0} items to Thread Pool.", iMaxCount);
            ThreadClass1 aThreadClass = new ThreadClass1(iMaxCount);
            aThreadClass.ManualResetEvent = aManualResetEvent;

            // First, add an item to check if your system supports ThreadPool API function or not.
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(aThreadClass.ThreadRun), new ParamObject(0));
                enableThreadPool = true;
            }
            catch (NotSupportedException exc)
            {
                Console.WriteLine("Thread Pool API is not supported in this system. \r\n " + exc.Message);
                enableThreadPool = false;
            }

            if (enableThreadPool)
            {
                for (int i = 1; i < iMaxCount; i++)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(aThreadClass.ThreadRun), new ParamObject(i));
                }
                Console.WriteLine("Waiting for thread pool to drain");
                aManualResetEvent.WaitOne(Timeout.Infinite, true);

                Console.WriteLine("Thread Pool has been drained.");
                Console.WriteLine("Load threads info:");
                foreach (object key in aThreadClass.Hashtable.Keys)
                {
                    Console.WriteLine("Key: {0}, Value: {1}", key, aThreadClass.Hashtable[key]);
                }
            }
            Console.ReadLine();
        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 线程执行
        /// </summary>
        /// <param name="param"></param>
        public void ThreadRun(object param) 
        {
            var currentHashCode = Thread.CurrentThread.GetHashCode();
            Console.WriteLine("HashCode: {0}, Number is Object: {1};", currentHashCode, ((ParamObject)param).Number);
            lock (this.Hashtable) 
            {
                if (!this.Hashtable.ContainsKey(currentHashCode))
                {
                    this.Hashtable.Add(currentHashCode, 0);
                }
                this.Hashtable[currentHashCode] = (int)this.Hashtable[currentHashCode] + 1;
            }
            Thread.Sleep(3000);

            Interlocked.Increment(ref pCount);
            if (pCount == pMaxCount)
            {
                Console.WriteLine("Setting ManualResetEvent...");
                this.ManualResetEvent.Set();
            }
        }
        #endregion
    }
}
