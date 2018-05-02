using System;
using System.Collections.Generic;
using MainLibrary.Core;

namespace MainLibrary
{
    public class Runner
    {
        private readonly ICalculator calculator;

        public Runner(ICalculator calculator)
        {
            this.calculator = calculator;
        }

        public decimal Total(List<Tuple<decimal, decimal>> elements)
        {
            decimal total = 0;

            foreach (var element in elements)
            {
                total += calculator.Operate(element.Item1, element.Item2);
            }

            return total;
        }
    }
}