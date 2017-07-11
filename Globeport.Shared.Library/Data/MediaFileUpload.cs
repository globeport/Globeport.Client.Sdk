using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Globeport.Shared.Library.Data
{
    public class MediaFileUpload
    {
        public int Size { get; set; }
        public byte[] Data { get; set; }
        public byte[] Signature { get; set; }

        public MediaFileUpload()
        {
        }

        public MediaFileUpload(int size, byte[] data, byte[] signature)
        {
            Size = size;
            Data = data;
            Signature = signature;
        }
    }
}
