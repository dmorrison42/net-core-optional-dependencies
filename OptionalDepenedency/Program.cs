using Dependency;
using System;

namespace OptionalDepenedency
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prints a different message when this project has a reference to OptionalSubDependency!
            Console.WriteLine(Message.Value);
            Console.ReadKey();
        }
    }
}