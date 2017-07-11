using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetKeys : ApiRequest
    {
        public List<string> Keys { get; set; }

        public GetKeys()
        {
        }

        public GetKeys(IEnumerable<string> keys)
        {
            Keys = keys.ToList();
        }

        public override string GetPath()
        {
            return $"keys/{string.Join(",", Keys)}";
        }

        public override string GetLogContent()
        {
            return new { Keys = Keys }.Serialize();
        }
    }

    public class GetKeysResponse : DataResponse
    {
        public List<Key> Keys { get; set; }

        public GetKeysResponse()
        {
        }

        public GetKeysResponse(IEnumerable<Key> keys)
        {
            Keys = keys.ToList();
        }
    }
}
