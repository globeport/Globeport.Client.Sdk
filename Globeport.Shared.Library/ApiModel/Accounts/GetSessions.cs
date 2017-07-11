using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Globeport.Shared.Library.ClientModel;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetSessions : ApiRequest
    {
        public GetSessions()
        {
        }

        public override string GetPath()
        {
            return "sessions";
        }
    }

    public class GetSessionsResponse : DataResponse
    {
        public List<Session> Sessions { get; set; }

        public GetSessionsResponse()
        {
        }

        public GetSessionsResponse(IEnumerable<Session> sessions) 
        {
            Sessions = sessions.ToList();
        }
    }
}
