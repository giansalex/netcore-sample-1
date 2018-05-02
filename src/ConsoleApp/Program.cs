using System;
using System.Collections.Generic;
using MainLibrary;
using MainLibrary.Core;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new List<Tuple<decimal, decimal>>
            {
                new Tuple<decimal, decimal>(2, 5),
                new Tuple<decimal, decimal>(1, 3.5M),
                new Tuple<decimal, decimal>(6.4M, 3),
            };

            var runner = new Runner(new SumOperation());
            var result = runner.Total(numbers);

            Console.WriteLine("Init Program!");
            Console.WriteLine($"Result: {result:0.00}");
        }
    }
}
