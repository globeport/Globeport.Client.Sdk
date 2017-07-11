using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Attributes;

namespace Globeport.Shared.Library.ClientModel
{
    public class Packet : ClientObject
    {
        public string PacketId { get; set; }
        public string ContainerId { get; set; }
        public string SenderId { get; set; }
        public byte[] Data { get; set; }
        [Ignore]
        public bool IsEncrypted { get; set; }

        public Packet()
        {
        }

        public Packet(string packetId, string containerId, string senderId, byte[] data, bool isEncrypted = false)
            : base(GetId(containerId, senderId, packetId))
        {
            PacketId = packetId;
            ContainerId = containerId;
            SenderId = senderId;
            Data = data;
            IsEncrypted = isEncrypted;
        }

        public static string GetId(string containerId, string senderId, string packetId)
        {
            return $"{containerId}.{senderId}.{packetId}";
        }

        public PacketUpload GetUpload()
        {
            return new PacketUpload(PacketId, ContainerId, SenderId, Data);
        }
    }
}
