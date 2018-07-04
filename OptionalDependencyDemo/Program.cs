using System;
using PluginLibrary;

namespace OptionalDependencyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Plugin.New().Text);
            Console.ReadLine();
        }
    }
}