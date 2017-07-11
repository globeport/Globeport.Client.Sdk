using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public class SignalEndpoint
    {
        public string AccountId { get; set; }
        public long DeviceId { get; set; }
        public string PortalId { get; set; }

        public SignalEndpoint()
        {
        }

        public SignalEndpoint(string accountId, long deviceId, string portalId)
        {
            AccountId = accountId;
            DeviceId = deviceId;
            PortalId = portalId;
        }

        public virtual string GetAddress()
        {
            return $"{PortalId}.{AccountId}.{DeviceId}";
        }
    }
}
