namespace MyPC.Models
{
    class MemoryStatus : ViewModels.BaseVM
    {
        public ulong TotalPhysical { get; set; }
        public ulong AvailablePhysical { get; set; }
        public ulong TotalPageFile { get; set; }
        public ulong AvailablePageFile { get; set; }
        public ulong TotalVirtual { get; set; }
        public ulong AvailableVirtual { get; set; }
        public ulong AvailableExtendedVirtual { get; set; }
    }
}
