using MyPC.Models;
using OpenHardwareMonitor.Hardware;
using System.Collections.Generic;
using System.Linq;

namespace MyPC.Service
{
    class PCService
    {
        public readonly Hardware.Info.IHardwareInfo hardwareInfo;
        public  List<CPU> GetCPU() 
            => hardwareInfo.CpuList.Select(cpu => new CPU
            {
                MaxClockSpeed = cpu.MaxClockSpeed,
                SocketDesignation = cpu.SocketDesignation,
                ProcessorId = cpu.ProcessorId,
                NumberOfLogicalProcessors = cpu.NumberOfLogicalProcessors,
                NumberOfCores = cpu.NumberOfCores,
                Name = cpu.Name,
                Caption = cpu.Caption,
                L1DataCacheSize = cpu.L1DataCacheSize,
                L3CacheSize = cpu.L3CacheSize,
                L2CacheSize = cpu.L2CacheSize
            }).ToList();
          
        

        public  List<VideoController> GetGPU()
            => hardwareInfo.VideoControllerList
                .Select(
                    x => new VideoController
                    {
                        Name = x.Name,
                        AdapterRAM = x.AdapterRAM,                       
                        Manufacturer = x.Manufacturer,
                        VideoProcessor = x.VideoProcessor
                    }).ToList();


        

        public  List<Network> GetNetworkAdapter()
            => hardwareInfo.NetworkAdapterList
                .Select(
                    x => new Network
                    {
                        Name = x.Name
                    }).ToList();
       


        //public  List<Memory> GetMemory()
        //{ 
        //    return hardwareInfo.MemoryList
        //        .Select(
        //            x => new Memory
        //            {
        //                BankLabel = x.BankLabel,
        //                Capacity = x.Capacity,
        //                Manufacturer = x.Manufacturer,
        //                PartNumber = x.PartNumber,
        //                SerialNumber = x.SerialNumber,
        //                Speed = x.Speed,
        //                FormFactor = x.FormFactor
        //            }).ToList();
        //}

        public List<Drive> GetDrive()
            => hardwareInfo.DriveList
                .Select(
                    x => new Drive
                    {
                        SerialNumber = x.SerialNumber,
                        Model = x.Model,
                        Name = x.Name,
                        Size = x.Size,
                        PartitionList = x.PartitionList.Select(y => new Partition { Name = y.Name, Size = y.Size }).ToList()
                    }).ToList();
        

        public Models.OperatingSystem GetOperatingSystem()
            => new Models.OperatingSystem
                {
                    Name = hardwareInfo.OperatingSystem.Name,
                    Version = hardwareInfo.OperatingSystem.VersionString
                };
        

        public List<Monitor> GetMonitors() 
            => hardwareInfo.MonitorList
                .Select(
                    x => new Monitor
                    {
                        Name = x.Name
                    }).ToList();
        

        public List<Motherboard> GetMotherboard() 
            => hardwareInfo.MotherboardList
                .Select(
                    x => new Motherboard
                    {
                        Manufacturer = x.Manufacturer,
                        Product = x.Product,
                        SerialNumber = x.SerialNumber
                    }).ToList();
        

        public (List<Sensor>, List<Sensor>, List<Sensor>, List<Sensor>) GetSensors()
        {
            UpdateVisitor updateVisitor = new UpdateVisitor();
            Computer computer = new Computer()
            {

                CPUEnabled = true,
                RAMEnabled = true,
                GPUEnabled = true,
                FanControllerEnabled = true,
                HDDEnabled = true,
                MainboardEnabled = true
            };
            computer.Open();

            computer.Accept(updateVisitor);
            List<Sensor> clockSensor = new List<Sensor>();
            List<Sensor> loadSensor = new List<Sensor>();
            List<Sensor> powerSensor = new List<Sensor>();
            List<Sensor> temperatureSensors = new List<Sensor>();
            for (int i = 0; i < computer.Hardware.Length; i++)
            {
                if (computer.Hardware[i].HardwareType == HardwareType.CPU)
                {
                    for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                    {
                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                            temperatureSensors.Add(new Sensor { Name = computer.Hardware[i].Sensors[j].Name, Value = computer.Hardware[i].Sensors[j].Value });
                        
                    }
                }
                if (computer.Hardware[i].HardwareType == HardwareType.HDD)
                {
                    for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                    {
                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                            temperatureSensors.Add(new Sensor { Name = computer.Hardware[i].Sensors[j].Name + " (DRIVE)", Value = computer.Hardware[i].Sensors[j].Value});
                        
                    }
                }
                for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                {
                    if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Clock)
                        clockSensor.Add(new Sensor { Name = computer.Hardware[i].Sensors[j].Name, Value = computer.Hardware[i].Sensors[j].Value });
                    if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Load)
                        loadSensor.Add(new Sensor { Name = computer.Hardware[i].Sensors[j].Name, Value = computer.Hardware[i].Sensors[j].Value });
                    if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Power)
                        powerSensor.Add(new Sensor { Name = computer.Hardware[i].Sensors[j].Name, Value = computer.Hardware[i].Sensors[j].Value });
                   
                }

            }
            computer.Close();
            return (clockSensor, loadSensor, powerSensor, temperatureSensors);
        }


        public PCService()
        {
            hardwareInfo = new Hardware.Info.HardwareInfo();
            hardwareInfo.RefreshAll();
        }
    }
}
