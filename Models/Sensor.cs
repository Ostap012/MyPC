using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPC.Models
{
    class Sensor : ViewModels.BaseVM
    {
        private string name;
        private float? _value;

        public string Name
        {
            get => name; set { Set(ref name, value); OnPropertyChanged(); }
        }
        public float? Value { get => _value;set { Set(ref _value, value); OnPropertyChanged(); } }
    }
}
