using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Globeport.Shared.Library.ClientModel
{
    public class PortalClass : ClientObject
    {
        public string PortalId { get; set; }
        public string ClassId { get; set; }
        public long Count { get; set; }

        public PortalClass()
        {
        }


        public PortalClass(string portalId, string classId, long count)
            : base(GetId(portalId, classId))
        {
            PortalId = portalId;
            ClassId = classId;
            Count = count;
        }

        public static string GetId(string portalId, string classId)
        {
            return $"{portalId}.{classId}";
        }
    }
}
