using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zero.MultiThread
{
    public class ThreadClass3
    {
        public delegate bool PrintDelegate(string s);

        private delegate string SyncMethodDelegate(string str);

        /// <summary>
        /// 委托必须和要调用的异步方法有相同的签名
        /// </summary>
        /// <param name="callDuration">sleep时间</param>
        /// <param name="threadId">当前线程id</param>
        /// <returns></returns>
        public delegate string AsyncMethodCaller(int callDuration, out int threadId);

        public static void ThreadStart()
        {
            PrintDelegate printDelegate = Print;
            Console.WriteLine("主线程");

            //AsyncMethodCaller caller = new AsyncMethodCaller(TestMethodAsync);
            //int threadid = 0;
            ////开启异步操作
            //IAsyncResult result = caller.BeginInvoke(1000, out threadid, callBackMethod, caller);
            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("其它业务" + i.ToString());
            //}
            ////调用EndInvoke,等待异步执行完成
            //Console.WriteLine("等待异步方法TestMethodAsync执行完成");
            ////等待异步执行完毕信号
            ////result.AsyncWaitHandle.WaitOne();
            ////Console.WriteLine("收到WaitHandle信号");
            ////通过循环不停的检查异步运行状态
            ////while (result.IsCompleted==false)
            ////{
            ////    Thread.Sleep(100);
            ////    Console.WriteLine("异步方法，running........");
            ////}
            ////异步结束，拿到运行结果
            ////string res = caller.EndInvoke(out threadid, result);
            //////显示关闭句柄
            ////result.AsyncWaitHandle.Close();
            //Console.WriteLine("关闭了WaitHandle句柄");
            //Console.Read();


            //IAsyncResult result = printDelegate.BeginInvoke("Hello World.", null, null);
            //Console.WriteLine("主线程:" + Thread.CurrentThread.ManagedThreadId + ",继续执行...");

            //当使用BeginInvoke异步调用方法时，如果方法未执行完，EndInvoke方法就会一直阻塞，直到被调用的方法执行完毕
            //printDelegate.EndInvoke(result);

            //可以看到，与EndInvoke类似，只是用WaitOne函数代码了EndInvoke而已。
            //result.AsyncWaitHandle.WaitOne(-1, false);

            // 3.轮询
            // 之前提到的两种方法，只能等下异步方法执行完毕，
            // 在完毕之前没有任何提示信息，整个程序就像没有响应一样，用户体验不好，
            // 可以通过检查IasyncResult类型的IsCompleted属性来检查异步调用是否完成，
            // 如果没有完成，则可以适时地显示一些提示信息
            //while (!result.IsCompleted)
            //{
            //    Console.WriteLine("等待中......");
            //    Thread.Sleep(500);
            //}

            printDelegate.BeginInvoke("Hello world.", PrintComplete, printDelegate);
            Console.WriteLine("主线程继续执行...");

            //WrapperSyncMethodAsync("ABC");
            //Trace.WriteLine("Main thread continue...");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        public static bool Print(string s)
        {
            bool flag = true;
            string response = "字符串 ";
            Console.WriteLine(response + "异步线程开始执行：" + s);
            Thread.Sleep(5000);
            return flag;
        }

        /// <summary>
        /// 回调方法要求
        /// 1.返回类型为void
        /// 2.只有一个参数IAsyncResult
        /// </summary>
        /// <param name="result"></param>
        public static void PrintComplete(IAsyncResult result)
        {
            var response =  (result.AsyncState as PrintDelegate).EndInvoke(result);
            Console.WriteLine("当前线程结束: " + result.AsyncState.ToString() + "\r\n 执行结果是：" + response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        private static void WrapperSyncMethodAsync(string str)
        {
            SyncMethodDelegate syncMethodDelegate = SyncMethod;
            syncMethodDelegate.BeginInvoke(str, param =>
            {
                var result = syncMethodDelegate.EndInvoke(param);

                // using the result to do something
                Trace.WriteLine(result);
            }, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string SyncMethod(string str)
        {
            Thread.Sleep(2000);
            return str;
        }

        /// <summary>
        /// 异步方法回调方法,异步执行完毕，会回调该方法
        /// </summary>
        /// <param name="ar"></param>
        private static void callBackMethod(IAsyncResult ar)
        {
            AsyncMethodCaller caller = ar.AsyncState as AsyncMethodCaller;
            string result = caller.EndInvoke(out int threadid, ar);
            Console.WriteLine("Completed!");
            Console.WriteLine(result);

        }

        /// <summary>
        /// 与委托对应的方法
        /// </summary>
        /// <param name="callDuration"></param>
        /// <param name="threadId"></param>
        /// <returns></returns>
        private static string TestMethodAsync(int callDuration, out int threadId)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("异步TestMethodAsync开始");
            for (int i = 0; i < 5; i++)
            {   
                // 模拟耗时操作
                Thread.Sleep(callDuration);
                Console.WriteLine("TestMethodAsync:" + i.ToString());
            }
            sw.Stop();
            threadId = Thread.CurrentThread.ManagedThreadId;
            return string.Format("耗时{0}ms.", sw.ElapsedMilliseconds.ToString());
        }
    }
}
