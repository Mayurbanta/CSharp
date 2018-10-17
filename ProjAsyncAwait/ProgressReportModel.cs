using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjAsyncAwait
{
    public class ProgressReportModel
    {
        public int PercentageComplete { get; set; } = 0;
        public List<string> MethodsCompleted { get; set; } = new List<string>();
    }
}
