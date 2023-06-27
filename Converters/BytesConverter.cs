using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPC.Converters
{
    class BytesConverter : Converter
    {

        private uint _gigabyte = 1024 * 1024 * 1024;
        private uint _megabyte = 1024 * 1024;
        private uint _kilobyte = 1024;
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ulong bytes)) return null;
            if (bytes > _gigabyte)
                return bytes / _gigabyte + " GB";
            else if (bytes > _megabyte)
                return bytes / _megabyte + " MB";
            else
                return bytes / _kilobyte + " KB";

        }
    }
}
