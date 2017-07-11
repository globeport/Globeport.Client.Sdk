using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Globeport.Shared.Library.Extensions; 
using Globeport.Shared.Library.ClientModel;

namespace Globeport.Client.Sdk.Crypto
{
    public class PacketStore
    {
        public ApiClient ApiClient { get; }
        public Dictionary<string, Packet> AddedPackets { get; private set; }
        public HashSet<string> DeletedPackets { get; private set; }
        public HashSet<string> DeletedContainers { get; private set; }
        public Dictionary<string, Packet> StoredPackets { get; set; } = new Dictionary<string, Packet>();

        public PacketStore(ApiClient apiClient)
        {
            ApiClient = apiClient;
            Reset();
        }

        public void Add(Packet packet)
        {
            DeletedPackets.Remove(packet.Id);
            AddedPackets[packet.Id] = packet;
        }

        public void Delete(string id)
        {
            AddedPackets.Remove(id);
            DeletedPackets.Add(id);
        }

        public void DeleteByContainer(string containerId)
        {
            DeletedContainers.Add(containerId);
        }

        public async Task<Packet> GetPacket(string id)
        {
            var packet = AddedPackets.GetValue(id);
            if (packet == null)
            {
                packet = await GetStoredPacket(id).ConfigureAwait(false);
                if (packet == null)
                {
                    var response = await ApiClient.GetPacket(id).ConfigureAwait(false);
                    packet = response.Packets.FirstOrDefault();
                }
            }
            return packet;
        }

        protected virtual Task<Packet> GetStoredPacket(string id)
        {
            return Task.FromResult(StoredPackets.GetValue(id));
        }

        public void Reset()
        {
            AddedPackets = new Dictionary<string, Packet>();
            DeletedPackets = new HashSet<string>();
            DeletedContainers = new HashSet<string>();
        }
    }
}
