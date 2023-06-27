

namespace MyPC.Models
{
    class CPU : ViewModels.BaseVM
    {
        public uint MaxClockSpeed { get; set; }
        public string SocketDesignation { get; set; }
        public string ProcessorId { get; set; }
        public uint NumberOfLogicalProcessors { get; set; }
        public uint NumberOfCores { get; set; }
        public string Name { get; set; }
        public ulong L3CacheSize { get; set; }
        public ulong L2CacheSize { get; set; }
        public ulong L1DataCacheSize { get; set; }
        public string Caption { get; set; }
    }
}
