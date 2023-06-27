using MyPC.Models;
using MyPC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPC.ViewModels
{
    class SummaryVM : BaseVM
    {
        public PCService Service { get; }
        private SummaryInfo summaryInfo;
        public SummaryInfo SummaryInfo { get => summaryInfo; set { Set(ref summaryInfo, value); OnPropertyChanged(); } }



        private Hardware.Info.MemoryStatus _memoryStatus;
        public Hardware.Info.MemoryStatus MemoryStatus
        {
            get { return _memoryStatus; }
            set {Set(ref _memoryStatus, value); OnPropertyChanged(); }
        }

        public SummaryVM()
        {

        }
        public SummaryVM(PCService service, SummaryInfo summaryInfo)
        {
            Service = service;
            SummaryInfo = summaryInfo;


            Task.Run(async () =>
            {
                while (true)
                {
                    Service.hardwareInfo.RefreshMemoryStatus();
                    MemoryStatus = Service.hardwareInfo.MemoryStatus;
                    await Task.Delay(500);
                }
            });
        }

    }
}
