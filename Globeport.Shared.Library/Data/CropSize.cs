using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public class CropSize
    {
        public CropSize()
        {
        }

        public static CropSize Parse(string cropSize)
        {
            if (cropSize == null) return null;
            var values = cropSize.Split(ValueSeparators.Comma);
            return new CropSize(double.Parse(values[0]), double.Parse(values[1]), double.Parse(values[2]),double.Parse(values[3]));
        }

        public CropSize(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public override string ToString()
        {
            return string.Join(",", new object[] { X, Y, Width, Height });
        }
    }
}
