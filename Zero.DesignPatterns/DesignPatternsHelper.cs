using System;

namespace Zero.DesignPatterns
{
    /// <summary>
    /// 设计模式帮助类
    /// </summary>
    public class DesignPatternsHelper
    {
        /// <summary>
        /// 执行方法
        /// </summary>
        public static void Run(DesignPatternsType patternsType)
        {
            switch (patternsType)
            {
                case DesignPatternsType.Singleton:
                    { 
                        break;
                    }
                case DesignPatternsType.SimpleFactory:
                    {
                        break;
                    }
                case DesignPatternsType.FactoryMethod:
                    {
                        break;
                    }
                case DesignPatternsType.AbstractFactory:
                    {
                        break;
                    }
                case DesignPatternsType.Builder:
                    {
                        PatternBuilder.Run();

                        break;
                    }
                case DesignPatternsType.Prototype:
                    {
                        PatternPrototype.Run();

                        break;
                    }
                case DesignPatternsType.Adapter:
                    {
                        PatternAdapter.Run();

                        break;
                    }
                case DesignPatternsType.Bridge:
                    {
                        PatternBridge.Run();

                        break;
                    }
                case DesignPatternsType.Decorator:
                    {
                        break;
                    }
                case DesignPatternsType.Composite:
                    {
                        break;
                    }
                case DesignPatternsType.Facade:
                    {
                        break;
                    }
                case DesignPatternsType.Flyweight:
                    {
                        break;
                    }
                case DesignPatternsType.Proxy:
                    {
                        break;
                    }
                case DesignPatternsType.Template:
                    {
                        break;
                    }
                case DesignPatternsType.Command:
                    {
                        break;
                    }
                case DesignPatternsType.Iterator:
                    {
                        break;
                    }
                case DesignPatternsType.Observer:
                    {
                        PatternObserver.Run();

                        break;
                    }
                case DesignPatternsType.Mediator:
                    {
                        break;
                    }
                case DesignPatternsType.State:
                    {
                        break;
                    }
                case DesignPatternsType.Stragety:
                    {
                        break;
                    }
                case DesignPatternsType.ChainOfResponsibility:
                    {
                        break;
                    }
                case DesignPatternsType.Vistor:
                    {
                        break;
                    }
                case DesignPatternsType.Memento:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
