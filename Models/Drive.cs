using System.Collections.Generic;

namespace MyPC.Models
{
    class Drive : ViewModels.BaseVM
    {
        public List<Partition> PartitionList { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public ulong Size { get; set; }

    }
}
