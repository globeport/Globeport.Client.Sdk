using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class DeleteEntity : ApiRequest
    {
        public string EntityId { get; set; }

        public DeleteEntity()
        {
        }

        public DeleteEntity(string entityId)
        {
            EntityId = entityId;
        }

        public override string GetPath()
        {
            return $"entities/{EntityId}";
        }

        public override string GetLogContent()
        {
            return new { EntityId = EntityId }.Serialize();
        }
    }

    public class DeleteEntityResponse : ApiResponse
    {
        public DeleteEntityResponse()  
        {
        }
    }
}
