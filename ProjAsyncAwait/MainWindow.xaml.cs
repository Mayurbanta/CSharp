using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjAsyncAwait
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSyncProcess_Click(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            ProcessEngine processEngine = new ProcessEngine();

            var results = processEngine.ExecuteProcess();
            DisplayResult(results);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            TextBlockResult.Text += $"Total execution time: { elapsedMs }";

        }

        private void DisplayResult(List<string> results)
        {
            TextBlockResult.Text = string.Empty;
            foreach (var item in results)
            {
                TextBlockResult.Text += item + Environment.NewLine;
            }
        }

        private async void btnAsyncProcess_Click(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            ProcessEngine processEngine = new ProcessEngine();

            var results = await processEngine.ExecuteProcessAsync();
            DisplayResult(results);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            TextBlockResult.Text += $"Total execution time: { elapsedMs }";
        }

        private async void btnAsyncProcessWithCallback_Click(object sender, RoutedEventArgs e)
        {

            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += Progress_ProgressChanged; ;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            ProcessEngine processEngine = new ProcessEngine();

            var results = await processEngine.ExecuteProcessAsyncWithCallback(progress);

            PrintResults(results);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            TextBlockResult.Text += $"Total execution time: { elapsedMs }";
        }

        private void Progress_ProgressChanged(object sender, ProgressReportModel e)
        {
            dashboardProgress.Value = e.PercentageComplete;
            PrintResults(e.MethodsCompleted);
        }
        private void PrintResults(List<string> results)
        {
            TextBlockResult.Text = "";
            foreach (var item in results)
            {
                TextBlockResult.Text += item + Environment.NewLine;
            }
        }        

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }

        private async void btnAsyncProcessWithCancellationToken_Click(object sender, RoutedEventArgs e)
        {
            ProcessEngine processEngine = new ProcessEngine();
            var results = await processEngine.ExecuteProcessAsyncWithCancellation(cts.Token);
            TextBlockResult.Text = "RESULT IS " + results.ToString();
            if(cts.IsCancellationRequested)
            {
                cts.Dispose();
                cts = new CancellationTokenSource();
            }
         


        }
    }
}
