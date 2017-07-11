using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ClientModel
{
    public class SignalMessage : ClientObject
    {
        public string Address { get; set; }
        public string MessageType { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public bool IsFloating { get; set; }

        public SignalMessage()
        {
        }

        public SignalMessage(string address, string messageType, string contentType, byte[] content)
        {
            Address = address;
            MessageType = messageType;
            ContentType = contentType;
            Content = content;
        }

        public SignalMessage(string id, string address, string messageType, string contentType, byte[] content, bool isFloating)
            : base(id)
        {
            Address = address;
            MessageType = messageType;
            ContentType = contentType;
            Content = content;
            IsFloating = isFloating;
        }

        public SignalMessageUpload GetUpload()
        {
            return new SignalMessageUpload(Address, MessageType, ContentType, Content);
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
            return Address.Split('.')[2];
        }

        public static string GetAddress(string contactId, long deviceId, string portalId)
        {
            return $"{contactId}.{deviceId}.{portalId}";
        }
    }
}
