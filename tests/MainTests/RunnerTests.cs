using System;
using Xunit;
using MainLibrary;
using MainLibrary.Core;
using System.Collections.Generic;
using NFluent;

namespace MainTests
{
    public class RunnerTests
    {
        [Fact]
        public void TestCheckType()
        {
            Check.That(new SumOperation()).InheritsFrom<ICalculator>();
        }

        [Fact]
        public void TestSumTotal()
        {
            var numbers = new List<Tuple<decimal, decimal>>
            {
                new Tuple<decimal, decimal>(2, 5),
                new Tuple<decimal, decimal>(1, 5.5M),
            };

            var runner = new Runner(new SumOperation());

            var total = runner.Total(numbers);
            Assert.Equal(13.5M, total);
            Check.That(total).IsStrictlyGreaterThan(0);
        }
    }
}
