using System;
using Zero.DesignPatterns;

namespace Zero.ConsoleCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            DesignPatternsHelper.Run(DesignPatternsType.Builder);

            Console.ReadKey();
        }
    }
}
