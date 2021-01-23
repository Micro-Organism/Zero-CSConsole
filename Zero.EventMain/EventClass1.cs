using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.EventMain
{
    public class EventClass1
    {
        public static void EventStart() 
        {
            EventRun();
        }

        public static void EventRun() 
        {
            //Heater heater = new Heater();
            //Alarm alarm = new Alarm();
            //heater.BoilEvent += alarm.MakeAlert;    //注册方法
            //heater.BoilEvent += (new Alarm()).MakeAlert;   //给匿名对象注册方法
            //heater.BoilEvent += Display.ShowMsg;       //注册静态方法

            Heater.BoilEvent += Alarm.MakeAlert;    //注册方法
            Heater.BoilEvent += Display.ShowMsg;       //注册静态方法
            Heater.BoilWater();   //烧水，会自动调用注册过对象的方法
        }
    }

    /// <summary>
    /// 事件产生器
    /// 热水器
    /// </summary>
    public class Heater
    {
        private static int temperature; //监听参数
        public delegate void BoilHandler(int param);   //声明委托

        //声明一个事件不过类似于声明一个进行了封装的委托类型的变量而已
        public static event BoilHandler BoilEvent;        //声明事件

        /// <summary>
        /// 事件产生方法
        /// 烧开水
        /// </summary>
        public static void BoilWater()
        {
            for (int index = 0; index <= 100; index++)
            {
                temperature = index;

                if (temperature > 95)
                {                        
                    //如果有对象注册
                    if (BoilEvent != null)
                    { 
                        BoilEvent(temperature);  //调用所有注册对象的方法
                    }
                }
            }
        }
    }

    /// <summary>
    /// 事件观察者
    /// 警报器
    /// </summary>
    public class Alarm
    {
        /// <summary>
        /// 警报器
        /// </summary>
        /// <param name="param"></param>
        public static void MakeAlert(int param)
        {
            if (param >= 100)
            {
                Console.WriteLine("Alarm：嘀嘀嘀，水已经 {0} 度了：", param);
            }
            else
            {
                Console.WriteLine("Alarm：呜呜呜，水现在 {0} 度了：", param);
            }
        }
    }

    /// <summary>
    /// 事件观察者
    /// 显示器
    /// </summary>
    public class Display
    {
        /// <summary>
        ///  显示器
        /// </summary>
        /// <param name="param"></param>
        public static void ShowMsg(int param)
        {
            if (param >= 100)
            {
                Console.WriteLine("Display：水已烧开了，温度已经：{0}度。", param);
            }
            else
            {
                Console.WriteLine("Display：水快烧开了，当前温度：{0}度。", param);
            }
        }
    }

}
