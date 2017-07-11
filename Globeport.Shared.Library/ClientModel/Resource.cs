using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Reflection;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ClientModel
{
    public class Resource : ClientObject
    {
        public string AccountId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string FileId { get; set; }
        public long References { get; set; }
        public List<Reference> Dependencies { get; set; }
        public bool IsHidden { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public Resource()
        {
        }

        public Resource(string type)
        {
            Type = type;
            Created = DateTimeOffset.UtcNow;
            Updated = DateTimeOffset.UtcNow;
            Timestamp = DateTimeOffset.UtcNow;
        }

        public Resource(string id, string accountId, string type, string name, string label, string fileId, long references, List<Reference> dependencies, DateTimeOffset created, DateTimeOffset updated, DateTimeOffset timestamp)
            : base(id)
        {
            AccountId = accountId;
            Type = type;
            Name = name;
            Label = label;
            FileId = fileId;
            References = references;
            Dependencies = dependencies;
            IsHidden = ResourceTypes.IsHidden(type, name);
            Created = created;
            Updated = updated;
            Timestamp = timestamp;
        }

        public override object Clone()
        {
            var clone = (Resource) base.Clone();
            clone.Dependencies = Dependencies.Select(i => (Reference)i.Clone()).ToList();
            return clone;
        }

        public override DateTimeOffset GetTimestamp()
        {
            return Timestamp;
        }

        public string GetColor()
        {
            return ResourceTypes.GetColor(Type);
        }

        public string GetImage()
        {
            return ResourceTypes.GetImage(Type);
        }
    }
}
