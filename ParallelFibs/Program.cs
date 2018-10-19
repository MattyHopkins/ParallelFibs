// Constant time fibonacci number calculator
// Written by Matthew Hopkins
// Shows two concepts
//  calculate fibs in constant time
//  calculations in parallel using C# PLINQ

/*
MIT License

Copyright (c) 2018 MattyHopkins

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;


namespace ParallelFibs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Parallel Fibs");

            // put in your range from and count here
            // In native C#, we are using double in the algorithm so we are accurate up to n=70 
            IEnumerable<int> numbers = Enumerable.Range(1/*start*/, 50 /*how many*/);

            // calculate each number in the range in parallel
            var parallelQuery =
                    from n in numbers.AsParallel()
                    .AsOrdered()
                    select FibN((uint)n);

            // force PLINQ using x tasks  
            ulong[] fib = parallelQuery
                        .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                        .WithDegreeOfParallelism(4) // match to number of CPU cores for best performance
                        .ToArray(); //and collate into a sequential array,


            // display result as list
            Console.WriteLine(string.Join(",", fib));

            Console.ReadKey();
        }


        /// <summary>
        /// calc nth fibonacci in constant time
        /// 
        /// This implemetation of the function is interesting in computer science terms because it calculates in O(1), 
        /// order 1 or what is known as constant time 
        /// 
        /// We use the square root of 5 in this formula, which in turn is used to calculate Phi, or the golden ratio
        /// background and formula:
        /// http://www.maths.surrey.ac.uk/hosted-sites/R.Knott/Fibonacci/fibFormula.html#formula
        /// 
        /// The limitation of this implementation is the accuracy of the representation of the irrational
        /// decimals used, the square root of 5 and the calculation of the golden ratio
        /// the higher the Nth the higher the degree of accuracy required by the algorithm.
        /// Also the range and precision of the numerical representation.  
        /// In native C#, we are using double here and we are accurate up to n=70, 
        /// and past this we drift and would require higher precision than double.
        /// </summary>
        /// <param name="N">The nth of the Fib to calculate</param>
        /// <returns>The fibonacci number for the given nth, in this case accurate n=0..<71 </returns>
        static ulong FibN(uint N)
        {
            ulong ret = (ulong)N;

            if (N > 1)
            {
                // could speed up the algo making these static or const members
                // used here for clarity
                double sqrt5 = Math.Sqrt(5.0);
                double Phi_goldenRatio = ((sqrt5 + 1) / 2);
                double phi_invGoldenRatio = ((sqrt5 - 1) / 2);

                // actual calc in O(1) time
                double Fib = Math.Pow(Phi_goldenRatio, N);
                Fib -= Math.Pow(phi_invGoldenRatio, N);
                Fib = Fib / sqrt5;
                ret = (ulong)Math.Round(Fib, 0);
            }

            return (ret);
        }
    }
}
