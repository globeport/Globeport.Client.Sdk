using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public struct Size
    {
        double width, height;

        public Size(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        public override bool Equals(Object obj)
        {
            return obj is Size && this == (Size)obj;
        }
        public override int GetHashCode()
        {
            return width.GetHashCode() ^ height.GetHashCode();
        }
        public static bool operator ==(Size x, Size y)
        {
            return x.width == y.width && x.height == y.height;
        }
        public static bool operator !=(Size x, Size y)
        {
            return !(x == y);
        }

        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
    }
}
