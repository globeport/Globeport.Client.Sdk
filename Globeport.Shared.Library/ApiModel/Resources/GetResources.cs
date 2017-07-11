using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetResources : DataRequest
    {
        public string Type { get; set; }
        public List<string> Resources { get; set; }
        public bool Dependencies { get; set; }

        public GetResources()
        {
        }

        public GetResources(DataCursor cursor, string type, bool dependencies = true)
            : base(cursor)
        {
            Type = type;
            Dependencies = dependencies;
        }

        public GetResources(IEnumerable<string> resources, bool dependencies = true)
        {
            Resources = resources.ToList();
            Dependencies = dependencies;
        }

        public override string GetPath()
        {
            if (Resources!=null)
            {
                return $"resources/{string.Join(",", Resources)}?dependencies={Dependencies}";
            }
            else
            {
                return $"resources?{GetQuery()}&Type={Type}&dependencies={Dependencies}";
            }
        }

        public override string GetLogContent()
        {
            return new { Resources = Resources, Cursor = Cursor, Type = Type, Dependencies = Dependencies }.Serialize();
        }
    }

    public class GetResourcesResponse : DataResponse
    {
        public List<Resource> Resources { get; set; }
        public Dictionary<string, DateTimeOffset> Contacts { get; set; }

        public GetResourcesResponse()  
        {
        }

        public GetResourcesResponse(IEnumerable<Resource> resources)
        {
            Resources = resources.ToList();
        }

        public GetResourcesResponse(IEnumerable<Resource> resources, IEnumerable<KeyValuePair<string, DateTimeOffset>> contacts) 
        {
            Resources = resources.ToList();
            Contacts = contacts.ToDictionary(i => i.Key, i => i.Value);
        }
    }
}
