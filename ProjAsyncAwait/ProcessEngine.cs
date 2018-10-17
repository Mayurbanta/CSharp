using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjAsyncAwait
{
    public class ProcessEngine
    {
        private int GenerateDelay(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }

        #region Private Methods
        private string Method1()
        {
            int delay = GenerateDelay(200, 600);
            Thread.Sleep(delay);
            return "Method1";
        }

        private string Method2()
        {
            int delay = GenerateDelay(100, 900);
            Thread.Sleep(delay);
            return "Method2";
        }

        private string Method3()
        {
            int delay = GenerateDelay(700, 1500);
            Thread.Sleep(delay);
            return "Method3";
        }

        private string Method4()
        {
            int delay = GenerateDelay(1000, 2000);
            Thread.Sleep(delay);
            return "Method4";
        }

        private string Method5()
        {
            int delay = GenerateDelay(2500, 3000);
            Thread.Sleep(delay);
            return "Method5";
        }

        private string DoSomething(int sequence)
        {
            int delay = GenerateDelay(3000, 3000);
            Thread.Sleep(delay);
            return $"Method1: Sequence - {sequence.ToString()}";
        }
        #endregion

        #region Public Methods
        public List<string> ExecuteProcess()
        {
            List<string> Result = new List<string>
            {
                Method1(),
                Method2(),
                Method3(),
                Method4(),
                Method5()
            };

            return Result;
        }

        public async Task<List<string>> ExecuteProcessAsync()
        {
            List<string> lst = new List<string>();
            List<Task<string>> tasks = new List<Task<string>>
            {
                Task.Run(() => Method1()),
                Task.Run(() => Method2()),
                Task.Run(() => Method3()),
                Task.Run(() => Method4()),
                 Task.Run(() => Method5()),
            };


            var results = await Task.WhenAll(tasks);
            foreach (var item in results)
            {
                lst.Add(item);
            }

            return lst;
        }

        public async Task<List<string>> ExecuteProcessAsyncWithCallback(IProgress<ProgressReportModel> progress)
        {
            List<int> InputList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };


            ProgressReportModel report = new ProgressReportModel();
            List<string> output = new List<string>();

            await Task.Run(() =>
            {
                Parallel.ForEach<int>(InputList, (input) =>
                {
                    string result = DoSomething(input);
                    output.Add(result);

                    report.MethodsCompleted = output;
                    report.PercentageComplete = (output.Count * 100) / InputList.Count;
                    progress.Report(report);
                });
            });

            return output;
        }

        public async Task<decimal> ExecuteProcessAsyncWithCancellation( CancellationToken cancellationToken)
        {

            /*  List<int> InputList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };


              ProgressReportModel report = new ProgressReportModel();
              List<string> output = new List<string>();

              await Task.Run(() =>
              {
                  Parallel.ForEach<int>(InputList, (input) =>
                  {
                      string result = DoSomething(input);
                      output.Add(result);

                      cancellationToken.ThrowIfCancellationRequested();

                      report.MethodsCompleted = output;
                      report.PercentageComplete = (output.Count * 100) / InputList.Count;
                      progress.Report(report);
                  });
              });

              return output;*/

            decimal result = 0;
            try
            {
                 result = await Task.Run(() =>
                   {
                       decimal res = 0;

                // Loop for a defined number of iterations
                for (int i = 0; i < 100; i++)
                       {
                           // Check if a cancellation is requested, if yes,
                           // throw a TaskCanceledException.
                           if (cancellationToken.IsCancellationRequested)
                           {                               
                               break;
                               //throw new TaskCanceledException();                               
                           }

                           //cancellationToken.ThrowIfCancellationRequested();

                    // Do something that takes times like a Thread.Sleep in .NET Core 2.
                    Thread.Sleep(10);
                           res += i;
                       } 

                       return res;
                   });
            }
            catch (OperationCanceledException e)
            {
                
                throw new Exception(e.Message);
                
            }

            return result;
        }

        #endregion
    }
}
