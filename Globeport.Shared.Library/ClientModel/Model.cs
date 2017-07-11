using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.ClientModel
{
    public class Model : ClientObject, IModel
    {
        public string AccountId { get; set; }
        public string ClassId { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string ImageId { get; set; }
        public string Color { get; set; }
        public bool IsInteractive { get; set; }
        public List<Reference> Dependencies { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public Model()
        {
        }

        public Model(string id, string accountId, string classId, string name, string label, string imageId, string color, bool isInteractive, List<Reference> dependencies, DateTimeOffset created, DateTimeOffset updated, DateTimeOffset timestamp)
            : base(id)
        {
            AccountId = accountId;
            ClassId = classId;
            Name = name;
            Label = label;
            ImageId = imageId;
            Color = color;
            IsInteractive = isInteractive;
            Dependencies = dependencies;
            Created = created;
            Updated = updated;
            Timestamp = timestamp;
        }

        public override DateTimeOffset GetTimestamp()
        {
            return Timestamp;
        }

        public bool IsAvatar()
        {
            return IsAvatar(Id);
        }

        public static bool IsAvatar(string id)
        {
            return id == SystemModels.Avatar;
        }

        public Reference GetXamlReference(string name)
        {
            return Dependencies.FirstOrDefault(i => i.Name == name && i.Type == ResourceTypes.Xaml);
        }

        public Reference GetScriptReference(string name)
        {
            return Dependencies.FirstOrDefault(i => i.Name == name && i.Type == ResourceTypes.Script);
        }

        public Reference GetTableReference(string name)
        {
            return Dependencies.FirstOrDefault(i => i.Name == name && i.Type == ResourceTypes.Table);
        }

        public Reference GetSchemaReference(string name)
        {
            return Dependencies.FirstOrDefault(i => i.Name == name && i.Type == ResourceTypes.Schema);
        }

        public override object Clone()
        {
            var clone = (Model)base.Clone();
            clone.Dependencies = Dependencies.Select(i=>(Reference)i.Clone()).ToList();
            return clone;
        }
    }
}
