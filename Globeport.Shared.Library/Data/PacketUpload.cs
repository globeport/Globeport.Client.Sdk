using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public class PacketUpload
    {
        public string PacketId { get; set; }
        public string ContainerId { get; set; }
        public string SenderId { get; set; }
        public byte[] Data { get; set; }

        public PacketUpload()
        {
        }

        public PacketUpload(string packetId, string containerId, string senderId, byte[] data)
        {
            PacketId = packetId;
            ContainerId = containerId;
            SenderId = senderId;
            Data = data;
        }
    }
}
