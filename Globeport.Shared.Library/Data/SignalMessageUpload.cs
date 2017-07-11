using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public class SignalMessageUpload
    {
        public string Address { get; set; }
        public string MessageType { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        public SignalMessageUpload()
        {
        }

        public SignalMessageUpload(string address, string messageType, string contentType, byte[] content)
        {
            Address = address;
            MessageType = messageType;
            ContentType = contentType;
            Content = content;
        }
        public string GetContactId()
        {
            return Address.Split('.')[0];
        }

        public long GetDeviceId()
        {
            return long.Parse(Address.Split('.')[1]);
        }

        public string GetPortalId()
        {
            var parts = Address.Split('.');
            if (parts.Length== 1)
            {
                return Address.Split('.')[0];
            }
            else
            {
                return Address.Split('.')[2];
            }
        }
    }
}
