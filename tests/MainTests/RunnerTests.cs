using System;
using Xunit;
using MainLibrary;
using MainLibrary.Core;
using System.Collections.Generic;

namespace MainTests
{
    public class RunnerTests
    {
        [Fact]
        public void TestSumTotal()
        {
            var numbers = new List<Tuple<decimal, decimal>>
            {
                new Tuple<decimal, decimal>(2, 5),
                new Tuple<decimal, decimal>(1, 5.5M),
            };

            var runner = new Runner(new SumOperation());

            Assert.Equal(13.5M, runner.Total(numbers));
        }
    }
}
