using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Globeport.Shared.Library.ClientModel;

namespace Globeport.Shared.Library.ApiModel
{
    public class ResetCounter : ApiRequest
    {
        public string Id { get; set; }

        public ResetCounter()
        {
        }

        public ResetCounter(string id)
        {
            Id = id;
        }

        public override string GetPath()
        {
            return $"counters/{Id}";
        }
    }

    public class ResetCounterResponse : ApiResponse
    {
        public Counter Counter { get; set; }

        public ResetCounterResponse()
        {
        }

        public ResetCounterResponse(Counter counter)
        {
            Counter = counter;
        }
    }
}
