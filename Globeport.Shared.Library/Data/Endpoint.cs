using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public class Endpoint
    {
        public string AccountId { get; set; }
        public long DeviceId { get; set; }
        public string PortalId { get; set; }
        public string PushUri { get; set; }
        public string Platform { get; set; }

        public Endpoint()
        {
        }

        public Endpoint(string address)
        {
            var parts = address.Split('.');
            if (parts.Length == 3)
            {
                AccountId = parts[0];
                DeviceId = long.Parse(parts[1]);
                PortalId = parts[2];
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public Endpoint(string accountId, long deviceId, string portalId, string platform, string pushUri)
        {
            AccountId = accountId;
            DeviceId = deviceId;
            PortalId = portalId;
            Platform = platform;
            PushUri = pushUri;
        }

        public virtual string GetAddress()
        {
            return $"{AccountId}.{DeviceId}.{PortalId}";
        }

        public SignalEndpoint ToSignalEndpoint()
        {
            return new SignalEndpoint(AccountId, DeviceId, PortalId);
        }
    }
}
