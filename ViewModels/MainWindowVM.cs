using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MyPC.Commands;
using MyPC.Service;
namespace MyPC.ViewModels
{
    internal class MainWindowVM : BaseVM
    {
        private readonly PCService Service;

        private Models.SummaryInfo _summary;
        public Models.SummaryInfo Summary
        {
            get { return _summary; }
            set { _summary = value; }
        }

        #region Title
        private string _Title = "MyPC";
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region Current view
        private object _currentView;
        public object CurrentView
        {
            get => _currentView; set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        #endregion



        #region Commands
        public ICommand SummaryCommand { get; set; }


        private void SummaryExecution(object obj)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
            {
                Application.Current.MainWindow.Show();
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            CurrentView = new SummaryVM(Service, Summary);
        }
        public ICommand SensorCommand { get; set; }
        public void SensorsExecution(object obj)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
            {
                Application.Current.MainWindow.Show();
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            CurrentView = new SensorsVM(Service);
        }
        public ICommand AboutCommand { get; set; }
        public void AboutExecution(object obj)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
            {
                Application.Current.MainWindow.Show();
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            CurrentView = new AboutVM();
        }
        #endregion

        public MainWindowVM()
        {
            SummaryCommand = new RelayCommand(SummaryExecution);
            SensorCommand = new RelayCommand(SensorsExecution);
            AboutCommand = new RelayCommand(AboutExecution);


            Service = new PCService();

            Task.Run(() =>
            {
                Summary = new Models.SummaryInfo
                {
                    CPUs = Service.GetCPU(),
                    Drives = Service.GetDrive(),
                    VideoControllers = Service.GetGPU(),
                    Monitors = Service.GetMonitors(),
                   
                    Motherboards = Service.GetMotherboard(),
                    Networks = Service.GetNetworkAdapter(),
                    OS = Service.GetOperatingSystem()
                };
            }).Wait();

            
            CurrentView = new SummaryVM(Service, Summary);
        }
    }
}
