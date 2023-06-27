namespace MyPC.Models
{
    class Partition : ViewModels.BaseVM
    {
        public string Name { get; set; }
        public ulong Size { get; set; }
    }
}
