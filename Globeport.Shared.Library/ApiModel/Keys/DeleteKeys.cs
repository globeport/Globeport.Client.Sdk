using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class DeleteKeys : ApiRequest
    {
        public List<string> Keys { get; set; }

        public DeleteKeys()
        {
        }

        public DeleteKeys(IEnumerable<string> keys)
        {
            Keys = keys.ToList();
        }

        public override string GetPath()
        {
            return $"keys/{string.Join(",", Keys)}";
        }

        public override string GetLogContent()
        {
            return new { Keys = Keys}.Serialize();
        }
    }

    public class DeleteKeysResponse : ApiResponse
    {
        public DeleteKeysResponse()
        {
        }
    }
}
