using MyPC.Models;
using MyPC.Service;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPC.ViewModels
{
    class SensorsVM : BaseVM
    {
       
        private ObservableCollection<Sensor> _powerSensor;
        public ObservableCollection<Sensor> PowerSensor
        {
            get { return _powerSensor; }
            set { Set(ref _powerSensor, value); OnPropertyChanged(); }
        }

        private ObservableCollection<Sensor> _clockSensor;
        public ObservableCollection<Sensor> ClockSensor
        {
            get { return _clockSensor; }
            set { Set(ref _clockSensor, value); OnPropertyChanged(); }
        }


        private readonly PCService Service;

        private ObservableCollection<Sensor> _loadSensor;
        public ObservableCollection<Sensor> LoadSensor
        {
            get { return _loadSensor; }
            set { Set(ref _loadSensor, value); OnPropertyChanged(); }
        }

        private ObservableCollection<Sensor> _temperatureSensors;

        public ObservableCollection<Sensor> TemperatureSensors
        {
            get { return _temperatureSensors; }
            set { Set(ref _temperatureSensors, value); OnPropertyChanged(); }
        }
        
        public SensorsVM(PCService service)
        {

            Service = service;
            TemperatureSensors = new ObservableCollection<Sensor>();
            LoadSensor = new ObservableCollection<Sensor>();
            ClockSensor = new ObservableCollection<Sensor>();
            PowerSensor = new ObservableCollection<Sensor>();

            Task.Run(async() =>
            {


                while (true)
                {
                    var result = Service.GetSensors();
                    ClockSensor = new ObservableCollection<Sensor>(result.Item1);
                    LoadSensor = new ObservableCollection<Sensor>(result.Item2);
                    PowerSensor = new ObservableCollection<Sensor>(result.Item3);
                    TemperatureSensors = new ObservableCollection<Sensor>(result.Item4);
                    await Task.Delay(500);
                }
            });
           
        }
    }
}
