using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zero.MultiThread
{
    public class ThreadPoolHelper
    {
        #region 字段属性

        #endregion

        #region 构造方法
        public ThreadPoolHelper()
        {

        }
        #endregion

        #region 基本方法
        /// <summary>
        /// 线程启动
        /// </summary>
        public static void ThreadStart() 
        { 
            
        }

        /// <summary>
        /// 计数操作
        /// </summary>
        public static void Count(CancellationToken token, int iCount)
        {
            for (int index = 0; index < iCount; index++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Count is cancelled");
                    Console.WriteLine(index);
                    break;
                }
            }
            Console.WriteLine(iCount);
            Thread.Sleep(200);
        }

        /// <summary>
        /// SimpleCancellationToken
        /// </summary>
        public static void SimpleCancellationToken() 
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.Token.Register(new Action(() => {
                Console.WriteLine("cst cancelled");
            }));

            string state = "state";

            ThreadPool.QueueUserWorkItem(new WaitCallback(param => 
            {
                Console.WriteLine(param.ToString());
                Count(cts.Token, 100);
            }), state);

            Console.WriteLine("Press <Enter> to Cancel the Operation");
            Console.WriteLine();
            cts.Cancel();
            Console.ReadLine();
        }

        /// <summary>
        /// TaskMethod
        /// </summary>
        public static void TaskMethod() 
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.Token.Register(new Action(() => {
                Console.WriteLine("cst cancelled");
            }));

            Task task = new Task(new Action(()=> {
                Console.WriteLine("Task");
                Count(cts.Token, 100);
            }), cts.Token);

            Task<int> taskT = Task.Run(() => 
            {
                Console.WriteLine("Task Run");
                return 100;
            }, cts.Token);

            taskT.Wait();
            Console.WriteLine("The Result is: " + taskT.Result);
        }

        public static void ContinueWithTask() 
        {
            //创建并启动一个Task 第一个任务
            Task<int> task = Task.Run(() => Sum(CancellationToken.None, 1000));
            //继续另一个任务 第二个任务 串行执行
            Task taskN = task.ContinueWith(t => {

                Console.WriteLine("The Sum is : " + t.Result);
            });
        }

        public static int Sum(CancellationToken cancellationToken, int iNum) 
        {
            int sum = 0;
            for (; iNum > 0; iNum --)
            {
                cancellationToken.ThrowIfCancellationRequested();
                checked 
                {
                    sum += iNum;
                }
            }
            return sum;
        }

        public static void TaskFactoryMethod() 
        {
            var cts = new CancellationTokenSource();
            var tf = new TaskFactory<int>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
            var chidTask = new[]
            {
                tf.StartNew(()=>Sum(cts.Token, 10000)),
                tf.StartNew(()=>Sum(cts.Token, 10000)),
                tf.StartNew(()=>Sum(cts.Token, 10000)),
            };
        }

        public static void ParallelMethod() 
        {
            Parallel.For(0, 100, index => { Console.WriteLine(index); });

            List<string> lstTemp = new List<string>() { "111", "222", "333",};
            Parallel.ForEach(lstTemp, item => { Console.WriteLine(item); });

            Parallel.Invoke(
                () => { Console.WriteLine(lstTemp[0]); },
                () => { Console.WriteLine(lstTemp[1]); },
                () => { Console.WriteLine(lstTemp[2]); });
        }
        #endregion

    }


}
