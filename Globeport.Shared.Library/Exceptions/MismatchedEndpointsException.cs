using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Globeport.Shared.Library;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.ClientModel;

namespace Globeport.Shared.Library.Exceptions
{
    public class MismatchedEndpointsException : ApiException
    {
        public MismatchedEndpointsException(IEnumerable<SignalEndpoint> endpoints, IEnumerable<SignalEndpoint> extraEndpoints, IEnumerable<Portal> missingPortals, IEnumerable<string> extraPortals) 
            : base(Resources.GetString("MismatchedEndpoints"))
        {
            Content = new MismatchedEndpoints(endpoints.ToList(), extraEndpoints.ToList(), missingPortals.ToList(), extraPortals.ToList()).Serialize();
        }

        public MismatchedEndpointsException(string content)
            : base(Resources.GetString("MismatchedEndpoints"), content)
        {
        }

        public MismatchedEndpoints GetEndpoints()
        {
            return Content.Deserialize<MismatchedEndpoints>();
        }
    }
}
