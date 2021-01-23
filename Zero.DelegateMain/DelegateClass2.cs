using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.DelegateMain
{
    //建立委托（delegate），过程有点类似于建立一个函数指针。过程如下：
    //1. 建立一个委托类型，并声明该委托可以指向的方法的签名（函数原型）
    //定义委托，它定义了可以代表的方法的类型
    public delegate void GreetingDelegate(string name);

    /// <summary>
    /// 当前时刻可以让指针p指向Max，
    /// </summary>
    public class DelegateClass2
    {
        /// <summary>
        /// 委托执行
        /// </summary>
        public void DelegateStart()
        {
            this.DelegateRun();
        }

        /// <summary>
        /// 委托执行
        /// </summary>
        public void DelegateRun()
        {
            //2.建立一个委托类的实例，并指向要调用的方法
            //利用委托类的构造方法指定，这是最为常见的一种方式
            GreetingManager gm = new GreetingManager();
            //3.利用委托类实例调用所指向的方法
            gm.GreetPeople("Jimmy Zhang", EnglishGreeting);
            gm.GreetPeople("张子阳", ChineseGreeting);

            GreetingManager gm_multi = new GreetingManager();
            GreetingDelegate delegate1;
            delegate1 = EnglishGreeting;
            delegate1 += ChineseGreeting;
            gm_multi.GreetPeople("Jimmy Zhang", delegate1);

            GreetingManager gm_delegate = new GreetingManager();
            gm_delegate.delegate1 = EnglishGreeting;
            gm_delegate.delegate1 += ChineseGreeting;
            //注意，这次不需要再传递 delegate1变量
            gm_delegate.GreetPeople("Jimmy Zhang");
        }

        private static void EnglishGreeting(string name)
        {
            Console.WriteLine("Morning, " + name);
        }

        private static void ChineseGreeting(string name)
        {
            Console.WriteLine("早上好, " + name);
        }

    }

    //新建的GreetingManager类
    public class GreetingManager
    {
        //在GreetingManager类的内部声明delegate1变量
        public GreetingDelegate delegate1;

        public void GreetPeople(string name)
        {                
            //如果有方法注册委托变量
            if (delegate1 != null)
            {     
                delegate1(name);      //通过委托调用方法
            }
        }

        public void GreetPeople(string name, GreetingDelegate MakeGreeting)
        {
            MakeGreeting(name);
        }

    }
}
