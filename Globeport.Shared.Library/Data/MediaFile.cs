using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Globeport.Shared.Library.Data
{
    public class MediaFile
    {
        public int Size { get; set; }
        public byte[] Signature { get; set; }

        public MediaFile()
        {
        }

        public MediaFile(int size, byte[] signature)
        {
            Size = size;
            Signature = signature;
        }
    }
}
