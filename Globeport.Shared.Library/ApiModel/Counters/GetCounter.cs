using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Globeport.Shared.Library.ClientModel;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetCounter : ApiRequest
    {
        public string Id { get; set; }

        public GetCounter()
        {
        }

        public GetCounter(string id)
        {
            Id = id;
        }

        public override string GetPath()
        {
            return $"counters/{Id}";
        }
    }

    public class GetCounterResponse : ApiResponse
    {
        public Counter Counter { get; set; }

        public GetCounterResponse() { }

        public GetCounterResponse(Counter counter)
        {
            Counter = counter;
        }
    }
}
