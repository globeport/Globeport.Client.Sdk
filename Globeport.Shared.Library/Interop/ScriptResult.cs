using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Interop
{
    public class ScriptResult
    {
        public Metadata Metadata { get; set; }
        public PushPayload[] PushPayloads { get; set; }
        public string[] DeletedInteractions { get; set; }

        public ScriptResult(Metadata metadata, PushPayload[] pushPayloads, string[] deletedInteractions)
        {
            Metadata = metadata;
            PushPayloads = pushPayloads;
            DeletedInteractions = deletedInteractions;
        }
    }
}
