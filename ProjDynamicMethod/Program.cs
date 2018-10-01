using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjDynamicMethod
{
    using System.Diagnostics;
    class Program
    {
        static void Main(string[] args)
        {
            bool res;
            long ticks = 0;
            Stopwatch stopwatch = new Stopwatch();

            Worker worker = new Worker(10);

            //By Reflection-------------------------------------*
            stopwatch.Start();
            res = worker.InstanceByReflection();
            stopwatch.Stop();
            ticks = 0;
            ticks = stopwatch.ElapsedTicks;
            System.Console.WriteLine($"By Reflection: {ticks.ToString()}");


            //By Dynamic Method-------------------------------------
            stopwatch.Restart();
            res = worker.InstanceByReflection();
            stopwatch.Stop();
            ticks = 0;
            ticks = stopwatch.ElapsedTicks;
            System.Console.WriteLine($"By Dynamic Method: {ticks.ToString()}");


            //By Direct Reference-------------------------------------
            stopwatch.Restart();
            res = worker.InstanceByDirectReference();
            stopwatch.Stop();
            ticks = 0;
            ticks = stopwatch.ElapsedTicks;
            System.Console.WriteLine($"By Direct Reference: {ticks.ToString()}");

            System.Console.Read();
        }
    }
}
