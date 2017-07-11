using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetPortals : DataRequest
    {
        public List<string> Portals { get; set; }
        public string ContactId { get; set; }
        public string EntityId { get; set; }
        public List<string> Types { get; set; }
        public List<string> States { get; set; }
        public string Mode { get; set; }

        public GetPortals()
        {
        }

        public GetPortals(DataCursor cursor, string contactId, string entityId, IEnumerable<string> types, IEnumerable<string> states, string mode)
            : base(cursor)
        {
            ContactId = contactId;
            EntityId = entityId;
            Types = types?.ToList();
            States = states?.ToList();
            Mode = mode;
        }

        public GetPortals(IEnumerable<string> portals)
        {
            Portals = portals.ToList();
        }

        public override string GetPath()
        {
            if (Portals != null)
            {
                return $"portals/{string.Join(",", Portals)}";
            }
            else
            {
                return $"portals?{GetQuery()}";
            }
        }

        public override string GetQuery()
        {
            var query = $"{base.GetQuery()}&contactId={ContactId}&entityId={EntityId}&mode={Mode}";
            if (Types!=null)
            {
                query = $"{query}&types={string.Join(",", Types)}";
            }
            if (States != null)
            {
                query = $"{query}&states={string.Join(",", States)}";
            }
            return query;
        }

        public override string GetLogContent()
        {
            return new { Types = Types, ContactId = ContactId, EntityId = EntityId, Mode = Mode, Cursor = Cursor }.Serialize();
        }
    }

    public class GetPortalsResponse : DataResponse
    {
        public List<Portal> Portals { get; set; }
        
        public GetPortalsResponse()
        {
        }
        public GetPortalsResponse(IEnumerable<Portal> portals) 
        {
            Portals = portals.ToList();
        }        
    }
}
