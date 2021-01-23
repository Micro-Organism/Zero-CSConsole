using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Algorithm
{
    public class FibonacciSequence
    {
        public int fib(int N)
        {
            if (N == 1 || N == 2)
            {
                return 1;
            }

            return fib(N - 1) + fib(N - 2);
        }
    }
}
