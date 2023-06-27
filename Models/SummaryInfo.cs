using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPC.Models
{
    class SummaryInfo : ViewModels.BaseVM
    {
        public List<CPU> CPUs { get; set; }
        public List<VideoController> VideoControllers { get; set; }
        public List<Drive> Drives { get; set; }
        public List<Monitor> Monitors { get; set; }
        public List<Network> Networks { get; set; }
        public List<Motherboard> Motherboards { get; set; }
        public OperatingSystem OS { get; set; }
        public MemoryStatus MemoryStatus { get; set; }

    }
}
