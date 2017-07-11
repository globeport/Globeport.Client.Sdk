using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetEntities : DataRequest
    {
        public List<string> Entities { get; set; }
        public string PortalId { get; set; }
        public string ModelId { get; set; }
        public bool Dependencies { get; set; }

        public GetEntities()
        {
        }

        public GetEntities(IEnumerable<string> entities, bool dependencies)
        {
            Entities = entities.ToList();
            Dependencies = dependencies;
        }

        public GetEntities(DataCursor cursor, string portalId, string modelId, bool dependencies)
            : base(cursor)
        {
            PortalId = portalId;
            ModelId = modelId;
            Dependencies = dependencies;
        }

        public override string GetPath()
        {
            if (Entities != null)
            {
                return $"entities/{string.Join(",", Entities)}?dependencies={Dependencies}";
            }
            else
            {
                return $"entities?{GetQuery()}&portalId={PortalId}&modelId={ModelId}&dependencies={Dependencies}";
            }
        }

        public override string GetLogContent()
        {
            return new { Entities = Entities, PortalId = PortalId, ModelId = ModelId, Dependencies = Dependencies, Cursor = Cursor }.Serialize();
        }
    }

    public class GetEntitiesResponse : DataResponse
    {
        public List<Entity> Entities { get; set; }
        public Dictionary<string, DateTimeOffset> Classes { get; set; }
        public Dictionary<string, DateTimeOffset> Models { get; set; }
        public Dictionary<string, DateTimeOffset> Contacts { get; set; }

        public GetEntitiesResponse()
        {
        }

        public GetEntitiesResponse(IEnumerable<Entity> entities)
        {
            Entities = entities.ToList();
        }

        public GetEntitiesResponse(IEnumerable<Entity> entities, IEnumerable<KeyValuePair<string, DateTimeOffset>> classes, IEnumerable<KeyValuePair<string, DateTimeOffset>> models, IEnumerable<KeyValuePair<string, DateTimeOffset>> contacts) 
        {
            Entities = entities.ToList();
            Classes = classes.ToDictionary(i=>i.Key, i=>i.Value);
            Models = models.ToDictionary(i => i.Key, i => i.Value);
            Contacts = contacts.ToDictionary(i => i.Key, i => i.Value);
        }
    }
}
