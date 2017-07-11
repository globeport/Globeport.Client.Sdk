using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetModels : DataRequest
    {
        public string ClassId { get; set; }
        public List<string> Models { get; set; }

        public GetModels()
        {
        }

        public GetModels(DataCursor cursor, string classId)
            : base(cursor)
        {
            ClassId = classId;   
        }

        public GetModels(IEnumerable<string> models)
        {
            Models = models.ToList();
        }

        public override string GetPath()
        {
            if (Models != null)
            {
                return $"models/{string.Join(",", Models)}";
            }
            else
            {
                return $"models?{GetQuery()}&classid={ClassId}";
            }
        }

        public override string GetLogContent()
        {
            return new { ClassId = ClassId, Models = Models, Cursor = Cursor }.Serialize();
        }
    }

    public class GetModelsResponse : DataResponse
    {
        public List<Model> Models { get; set; }

        public GetModelsResponse()  
        {
        }

        public GetModelsResponse(IEnumerable<Model> models)
        {
            Models = models.ToList();
        }
    }
}
