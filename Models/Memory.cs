using Hardware.Info;

namespace MyPC.Models
{
    class Memory : ViewModels.BaseVM
    {
        public string BankLabel { get; set; }
        public ulong Capacity { get; set; }
        public FormFactor FormFactor { get; set; }
        public string Manufacturer { get; set; }
        public string PartNumber { get; set; }
        public string SerialNumber { get; set; }
        public uint Speed { get; set; }
    }
}
