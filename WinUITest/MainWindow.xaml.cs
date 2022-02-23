using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUITest.Data;
using WinUITest.ViewModel;
using muxc = Microsoft.UI.Xaml.Controls;
//https://www.google.com/search?client=firefox-b-d&q=winui3+viewmodel

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITest
{
    
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        private BackgroundWorker worker;
        private static int barval = 0;

        public MainViewModel ViewModel { get; }

        public MainWindow()
        {
            this.InitializeComponent();
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            //worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            //worker.ProgressChanged += Worker_ProgressChanged;

            //MyWebView.Source = new Uri(@"file://c:\\temp\\DOC180920-18092020105120.pdf");

            //MainGrid.DataContext = App.Current.Services.GetService(typeof(CustomerViewModel));
            ViewModel = new MainViewModel();
            ViewModel.Load();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            barval = 0;
            BackgroundWorker w = sender as BackgroundWorker;
            if (w != null)
            {
                while (barval <= 100)
                {
                    Thread.Sleep(100);
                    w.ReportProgress(barval);
                    barval++;
                }
            }
        }

        //private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    determinateProgressBar.Value = barval;

        //}

        //private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    myButton.IsEnabled = true;
        //}

        //private void myButton_Click(object sender, RoutedEventArgs e)
        //{
        //    myButton.Content = "Clicked";
        //    myButton.IsEnabled = false;
        //    worker.RunWorkerAsync();
        //}

    }
}
