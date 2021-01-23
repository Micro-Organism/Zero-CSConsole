using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.DelegateMain
{
    /// <summary>
    /// 当前时刻可以让指针p指向Max，
    /// 在后面的代码中，我们还可以利用指针p再指向Min函数，
    /// 但是不论p指向的是谁，调用p时的形式都一样，
    /// 这样可以很大程度上减少判断语句的使用，使代码的可读性增强！
    /// 在运行时利用delegate动态指向具备相同签名的方法（所谓的方法签名，是指一个方法的返回值类型及其参数列表的类型）。
    /// </summary>
    public class DelegateClass1
    {
        //建立委托（delegate），过程有点类似于建立一个函数指针。过程如下：
        //1. 建立一个委托类型，并声明该委托可以指向的方法的签名（函数原型）
        public delegate int MyDelegate(int param_a, int param_b);

        /// <summary>
        /// 构造函数
        /// </summary>
        public DelegateClass1()
        {

        }

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
            MyDelegate md_ctor = new MyDelegate(this.Max);
            //利用自动推断方式来指明要调用的方法，该形式更类型于函数指针
            MyDelegate md_auto = this.Max;

            //3.利用委托类实例调用所指向的方法
            var result = md_auto(2, 3);

        }

        /// <summary>
        /// 委托实例
        /// </summary>
        /// <param name="pram_x"></param>
        /// <param name="param_y"></param>
        /// <returns></returns>
        public int Max(int pram_x, int param_y)
        {
            return pram_x > param_y ? pram_x : param_y;
        }

        /// <summary>
        /// 委托实例
        /// </summary>
        /// <param name="pram_x"></param>
        /// <param name="param_y"></param>
        /// <returns></returns>
        public int Min(int pram_x, int param_y)
        {
            return pram_x < param_y ? pram_x : param_y;
        }

    }
}
