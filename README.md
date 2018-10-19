# ParallelFibs
A constant time Fibonacci number calculator in C# and a demo PLINQ scheduler.

An often asked computer science interview question is "write some code to show how you would calculate the nth Fibonacci number"
It's a good question as there are many ways to tackle it, including using techniques such as recursion or nested loops.
Amongst other things, the answer will show how efficiently your solution runs in 'Big O' terms.

As it turns out, there is a constant time way to solve this question, it involves using the square root of 5 and was independently discovered by historical mathematicians Binet, de Moivre and Euler

The program also shows how to schedule calculations in parallel using PLINQ.

## Getting Started
This is a simple console program with two parts.
The main of the program schedules requested calculations in parallel using PLINQ.
The FibN function of the program takes an index for the nth fibonacci number and returns the result in constant time

### Prerequisites
The project was built in Visual Studio 2017 as a .NET Core console, but the Program.cs could be used with most versions of Visual Studio or Code.

## Running
The program defines an Enumerable range which is a quick way to create a list of n's to solve. 
IEnumerable<int> numbers = Enumerable.Range(1/*start*/, 50 /*how many*/);

Note that the collection could contain arbitary n's they don't need to be contiguous, this is just to give the task scheduler something to do.

## Authors
Matthew Hopkins

## License
This project is licensed under the MIT License - see the [license file](./LICENSE) for details

## Acknowledgments
Thanks to [R.Knott](http://www.maths.surrey.ac.uk/hosted-sites/R.Knott/Fibonacci/fibFormula.html#formula) at Surrey University for the explaination of the formula



