using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingConsoleApp
{
    class Program
    {
        static async void Main(string[] args)
        {
            await mymethod();
        }

        static async Task mymethod()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Engine engine = new Engine();
            var i = await engine.Execute();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Total execution time: { elapsedMs }");
            Console.Read();
        }
    }
}
