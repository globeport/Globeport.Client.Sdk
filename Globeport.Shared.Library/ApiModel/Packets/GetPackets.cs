using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetPackets : ApiRequest
    {
        public List<string> Packets { get; set; }

        public GetPackets()
        {
        }

        public GetPackets(IEnumerable<string> packets)
        {
            Packets = packets.ToList();
        }

        public override string GetPath()
        {
            return $"packets/{string.Join(",", Packets)}";
        }

        public override string GetLogContent()
        {
            return new { Packets = Packets }.Serialize();
        }
    }

    public class GetPacketsResponse : DataResponse
    {
        public List<Packet> Packets { get; set; }

        public GetPacketsResponse()
        {
        }

        public GetPacketsResponse(IEnumerable<Packet> packets)
        {
            Packets = packets.ToList();
        }
    }
}
