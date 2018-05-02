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
using Serilog;
using Valit;
using MainLibrary.Model;

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

            using (var container = GetContainer())
            {
                using (container.BeginScope())
                {
                    using (profiler.Step("Calculate"))
                    {
                        var logger = container.Resolve<ILogger>();
                        var runner = container.Resolve<Runner>();
                        logger.Information("Run operations");
                        var result = runner.Total(numbers);

                        logger.Information("Result: {result:0.00}", result);
                        Console.WriteLine($"Result: {result:0.00}");
                    }

                    using (profiler.Step("Validate"))
                    {
                        var obj = new UserModel{
                            Email = "admin@domain.com",
                            Password = "12345677890",
                            Age = 24
                        };

                        var result = container.Resolve<IValitRules<UserModel>>()
                                        .For(obj)
                                        .Validate();

                        Console.WriteLine($"Is Valid: {result.Succeeded}");
                    }
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
            container.Register(Component.For<ILogger>()
                     .UsingFactoryMethod(GetLogger)
                     .LifestyleScoped());
            container.Register(Component.For<IValitRules<UserModel>>()
                     .UsingFactoryMethod(Validator.GetUserValidator)
                     .LifestyleSingleton());

            return container;
        }

        static ILogger GetLogger()
        {
            var log = new LoggerConfiguration()
                        .WriteTo.ColoredConsole()
                        .WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day)
                        .CreateLogger();

            return log;
        }
    }
}
