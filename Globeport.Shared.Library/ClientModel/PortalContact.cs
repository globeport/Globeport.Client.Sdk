using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Globeport.Shared.Library.ClientModel
{
    public class PortalContact : ClientObject
    {
        public string PortalId { get; set; }
        public string ContactId { get; set; }

        public PortalContact()
        {
        }

        public PortalContact(string portalId, string contactId)
            : base(GetId(portalId, contactId))
        {
            PortalId = portalId;
            ContactId = contactId;
        }

        public static string GetId(string contactId, string portalId)
        {
            return $"{portalId}.{contactId}";
        }
    }
}
