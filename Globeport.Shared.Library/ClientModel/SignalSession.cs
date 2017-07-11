using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Globeport.Shared.Library.ClientModel
{
    public class SignalSession : ClientObject
    {
        public string ContactId { get; set; }
        public long DeviceId { get; set; }
        public byte[] Data { get; set; }

        public SignalSession()
        {
        }

        public SignalSession(string contactId, long deviceId)
            : base(GetId(contactId, deviceId))
        {
            ContactId = contactId;
            DeviceId = deviceId;
        }

        public bool IsEmpty()
        {
            return Data == null;
        }

        public static string GetId(string contactId, long deviceId)
        {
            return $"{contactId}.{deviceId}";
        }
    }
}
