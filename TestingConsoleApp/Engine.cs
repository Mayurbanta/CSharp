using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestingConsoleApp
{
    class Engine
    {
        public int Cal1()
        {
            Thread.Sleep(4000);
            return 5;
        }

        public  int Cal2()
        {
            Thread.Sleep(2000);
            return   2;
        }


        public async Task<int> Execute()
        {
            int i = 2;
            i += 2;

            List<Task<int>> tsk = new List<Task<int>>();
            tsk.Add(Task.Run(() => Cal1()));
            tsk.Add(Task.Run(() => Cal2()));

            //var m1 = await Task.Run(() => Cal1());

            //var m2 = await Cal2();

            var res = await Task.WhenAll(tsk);

            foreach (var item in res)
            {
                i += item;
            }

            return i;
        }

    }
}
