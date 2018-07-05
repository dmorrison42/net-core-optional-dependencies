using System;
using OptionalDependency;
using PluginLibrary;

namespace OptionalDependencyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var iterations = 1_000_000;
            var len = 0;
            Console.WriteLine($"Iterations: {iterations}");

            Console.WriteLine("Initial Load");
            var start = DateTime.Now;
            len += Plugin.New().Text.Length;
            Console.WriteLine(DateTime.Now - start);


            Console.WriteLine("Factory Time");
            start = DateTime.Now;
            for (var i = 0; i < iterations; i++) {
                len += Plugin.New().Text.Length;
            }
            Console.WriteLine(DateTime.Now - start);

            Console.WriteLine("Direct initialization Time");
            start = DateTime.Now;
            for (var i = 0; i < iterations; i++) {
                len += (new CustomPlugin()).Text.Length;
            }
            Console.WriteLine(DateTime.Now - start);

            Console.ReadLine();
        }
    }
}