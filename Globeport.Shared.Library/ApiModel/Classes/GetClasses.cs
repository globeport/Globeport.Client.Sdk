using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetClasses : DataRequest
    {
        public bool IncludePost { get; set; }
        public string PortalId { get; set; }
        public List<string> Classes { get; set; }
        public bool Dependencies { get; set; }

        public GetClasses()
        {
        }

        public GetClasses(IEnumerable<string> classes, bool dependencies = true)
        {
            Classes = classes.ToList();
            Dependencies = dependencies;
        }

        public GetClasses(DataCursor cursor, string portalId, bool dependencies = true)
            : base(cursor)
        {
            PortalId = portalId;
            Dependencies = dependencies;
        }

        public override string GetPath()
        {
            if (Classes != null)
            {
                return $"classes/{string.Join(",", Classes)}?dependencies={Dependencies}";
            }
            else
            {
                return $"classes?{GetQuery()}&portalId={PortalId}&includePost={IncludePost}&dependencies={Dependencies}";
            }
        }

        public override string GetLogContent()
        {
            return new { PortalId = PortalId, Classes = Classes, Dependencies = Dependencies, Cursor = Cursor }.Serialize();
        }
    }

    public class GetClassesResponse : DataResponse
    {
        public List<Class> Classes { get; set; }
        public Dictionary<string, DateTimeOffset> Models { get; set; }

        public GetClassesResponse()  
        {
        }

        public GetClassesResponse(IEnumerable<Class> classes)
        {
            Classes = classes.ToList();
        }

        public GetClassesResponse(IEnumerable<Class> classes, Dictionary<string, DateTimeOffset> models) 
        {
            Classes = classes.ToList();
            Models = models;
        }
    }
}
