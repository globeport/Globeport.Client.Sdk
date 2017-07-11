using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace Globeport.Shared.Library.ApiModel
{
    public class Suspend : ApiRequest
    {
        public Suspend()
        {
        }

        public override string GetPath()
        {
            return "admin/suspend";
        }
    }

    public class SuspendResponse : ApiResponse
    {
        public SuspendResponse()
        {
        }
    }
}
