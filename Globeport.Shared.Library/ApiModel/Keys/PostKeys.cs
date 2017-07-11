using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ApiModel
{
    public class PostKeys : ApiRequest
    {
        public List<KeyUpload> Keys { get; set; }

        public PostKeys()
        {
        }

        public PostKeys(IEnumerable<KeyUpload> keys)
        {
            Keys = keys.ToList();
        }

        public override string GetPath()
        {
            return "keys";
        }
    }

    public class PostKeysResponse : ApiResponse
    {
        public PostKeysResponse()
        {
        }
    }
}
