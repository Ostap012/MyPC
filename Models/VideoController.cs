namespace MyPC.Models
{
    class VideoController : ViewModels.BaseVM
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string VideoProcessor { get; set; }
        public ulong AdapterRAM { get; set; }
    }
}
