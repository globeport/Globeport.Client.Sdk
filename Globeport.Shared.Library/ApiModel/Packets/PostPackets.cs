using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ApiModel
{
    public class PostPackets : ApiRequest
    {
        public List<PacketUpload> Packets { get; set; }

        public PostPackets()
        {
        }

        public PostPackets(IEnumerable<PacketUpload> packets)
        {
            Packets = packets.ToList();
        }

        public override string GetPath()
        {
            return "packets";
        }
    }

    public class PostPacketsResponse : ApiResponse
    {
        public PostPacketsResponse()
        {
        }
    }
}
