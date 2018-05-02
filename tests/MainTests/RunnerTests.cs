using System;
using Xunit;
using MainLibrary;
using MainLibrary.Core;
using System.Collections.Generic;
using NFluent;
using Shouldly;
using AutoFixture;
using FakeItEasy;
using AutoFixture.Xunit2;

namespace MainTests
{
    public class RunnerTests
    {
        private Fixture _fixture;
        public RunnerTests()
        {
            _fixture = new Fixture();
            _fixture.Register<ICalculator>(() => {
                var calculator = A.Fake<ICalculator>();
                A.CallTo(() => calculator.Operate(A<decimal>._, A<decimal>._)).ReturnsLazily(p => {
                    var a = (decimal)p.Arguments[0];
                    var b = (decimal)p.Arguments[1];
                    return a + b;
                });

                return calculator;   
            });
        }
        
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
            total.ShouldBeGreaterThan(0);
        }

        [Fact]
        public void TestSumFixture()
        {
            var calculator = _fixture.Create<ICalculator>();
            var numbers = new List<Tuple<decimal, decimal>>
            {
                new Tuple<decimal, decimal>(4, 6),
                new Tuple<decimal, decimal>(2, 8),
            };

            var runner = new Runner(calculator);
            var total = runner.Total(numbers);

            total.ShouldBe(20);
        }
    }
}
