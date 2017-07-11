using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.ClientModel;

namespace Globeport.Shared.Library.Data
{
    public class MismatchedEndpoints
    {
        public List<SignalEndpoint> MissingEndpoints { get; set; }
        public List<SignalEndpoint> ExtraEndpoints { get; set; }
        public List<Portal> MissingPortals { get; set; }
        public List<string> ExtraPortals { get; set; }

        public MismatchedEndpoints(List<SignalEndpoint> missingEndpoints, List<SignalEndpoint> extraEndpoints, List<Portal> missingPortals, List<string> extraPortals)
        {
            MissingEndpoints = missingEndpoints;
            ExtraEndpoints = extraEndpoints;
            MissingPortals = missingPortals;
            ExtraPortals = extraPortals;
        }
    }
}
