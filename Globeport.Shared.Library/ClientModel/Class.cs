using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Attributes;

namespace Globeport.Shared.Library.ClientModel
{
    public class Class : ClientObject
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string PluralLabel { get; set; }
        public string ImageId { get; set; }
        public string Color { get; set; }
        public string ModelId { get; set; }
        public long ModelCount { get; set; }
        [Ignore]
        public long EntityCount { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public DateTimeOffset Timestamp { get; set; }


        public Class()
        {
        }

        public Class(string id, string name, string label, string pluralLabel, string imageId, string color, string modelId, long modelCount, long entityCount, DateTimeOffset created, DateTimeOffset updated, DateTimeOffset timestamp) 
            : base(id)
        {
            Name = name;
            Label = label;
            PluralLabel = pluralLabel;
            ImageId = imageId;
            Color = color;
            ModelId = modelId;
            ModelCount = modelCount;
            EntityCount = entityCount;
            Created = created;
            Updated = updated;
            Timestamp = timestamp;
        }

        public override DateTimeOffset GetTimestamp()
        {
            return Timestamp;
        }
    }
}
