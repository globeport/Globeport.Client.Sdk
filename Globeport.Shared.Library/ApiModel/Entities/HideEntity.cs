using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class HideEntity : ApiRequest
    {
        public string EntityId { get; set; }

        public HideEntity()
        {
        }

        public HideEntity(string entityId)
        {
            EntityId = entityId;
        }

        public override string GetPath()
        {
            return $"entities/{EntityId}/hide";
        }

        public override string GetLogContent()
        {
            return new { EntityId = EntityId }.Serialize();
        }
    }

    public class HideEntityResponse : ApiResponse
    {
        public Entity Entity { get; set; }

        public HideEntityResponse()  
        {
        }

        public HideEntityResponse(Entity entity)
        {
            Entity = entity;
        }
    }
}
