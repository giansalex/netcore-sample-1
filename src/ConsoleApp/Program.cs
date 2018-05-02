using Castle.Windsor;
using System;
using System.Collections.Generic;
using MainLibrary;
using MainLibrary.Core;
using Castle.Windsor.Installer;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Lifestyle;
using StackExchange.Profiling;

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

            var profiler = MiniProfiler.StartNew(nameof(ConsoleApp));
            
            using (profiler.Step(nameof(Main)))
            using (var container = GetContainer())
            {
                using (container.BeginScope())
                {
                    var runner = container.Resolve<Runner>();
                    var result = runner.Total(numbers);

                    Console.WriteLine($"Result: {result:0.00}");
                }
            }

            profiler.Stop();
            Console.WriteLine(MiniProfiler.Current.RenderPlainText());
        }

        static IWindsorContainer GetContainer()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<ICalculator>()
                     .ImplementedBy<SumOperation>()
                     .LifestyleSingleton());
            container.Register(Component.For<Runner>()
                     .LifestyleScoped());

            return container;
        }
    }
}
