using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace Globeport.Shared.Library.ApiModel
{
    public class Resume : ApiRequest
    {
        public Resume()
        {
        }

        public override string GetPath()
        {
            return "admin/resume";
        }
    }

    public class ResumeResponse : ApiResponse
    {
        public ResumeResponse()
        {
        }
    }
}
