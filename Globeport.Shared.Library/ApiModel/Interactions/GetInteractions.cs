using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetInteractions : DataRequest
    {
        public List<string> Interactions { get; set; }
        public string AccountId { get; set; }
        public string EntityId { get; set; }
        public string Type { get; set; }
        public string Tag { get; set; }
        public bool Dependencies { get; set; }

        public GetInteractions()
        {
        }

        public GetInteractions(IEnumerable<string> interactions, bool dependencies)
        {
            Interactions = interactions.ToList();
            Dependencies = dependencies;
        }

        public GetInteractions(string entityId, string accountId, string type, string tag, DataCursor cursor, bool dependencies)
        {
            EntityId = entityId;
            AccountId = accountId;
            Type = type;
            Tag = tag;
            Cursor = cursor;
            Dependencies = dependencies;
        }

        public override string GetPath()
        {
            if (Interactions != null)
            {
                return $"interactions/{string.Join(",", Interactions)}?dependencies={Dependencies}";
            }
            else
            {
                return $"entities/{EntityId}/interactions?AccountId={AccountId}&Type={Type}&Tag={Tag}&{GetQuery()}&dependencies={Dependencies}";
            }
        }

        public override string GetLogContent()
        {
            return new { Interactions = Interactions, Cursor = Cursor, EntityId = EntityId, Type = Type, Filter = Tag, Dependencies = Dependencies }.Serialize();
        }
    }

    public class GetInteractionsResponse : DataResponse
    {
        public List<Interaction> Interactions { get; set; }
        public Dictionary<string, DateTimeOffset> Contacts { get; set; }

        public GetInteractionsResponse()
        {
        }

        public GetInteractionsResponse(IEnumerable<Interaction> interactions)
        {
            Interactions = interactions.ToList();
        }

        public GetInteractionsResponse(IEnumerable<Interaction> interactions, Dictionary<string, DateTimeOffset> contacts)
        {
            Interactions = interactions.ToList();
            Contacts = contacts;
        }
    }
}
