using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Globeport.Shared.Library.ClientModel
{
    public class PortalEntity : ClientObject
    {
        public string PortalId { get; set; }
        public string EntityId { get; set; }

        public PortalEntity()
        {
        }

        public PortalEntity(string portalId, string entityId)
            : base(GetId(portalId, entityId))
        {
            EntityId = entityId;
            PortalId = portalId;
        }

        public static string GetId(string portalId, string entityId)
        {
            return $"{portalId}.{entityId}";
        }
    }
}
