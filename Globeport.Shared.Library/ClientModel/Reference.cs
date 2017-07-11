using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ClientModel
{
    public class Reference : ClientObject, IReference
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string FileId { get; set; }
        public bool IsDirect { get; set; }
        public List<string> Dependencies { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public Reference()
        {
        }

        public Reference(string id, string type, string name, string fileId, bool isDirect, IEnumerable<string> dependencies, DateTimeOffset timestamp) 
            : base(id)
        {
            Type = type;
            Name = name;
            FileId = fileId;
            IsDirect = isDirect;
            Dependencies = dependencies?.ToList();
            Timestamp = timestamp;
        }

        public string GetColor()
        {
            return ResourceTypes.GetColor(Type);
        }

        public string GetImage()
        {
            return ResourceTypes.GetImage(Type);
        }

        public string GetFilename(string type = null)
        {
            return ResourceTypes.GetFilename(FileId, type ?? Type);
        }

        public bool IsHidden()
        {
            return ResourceTypes.IsHidden(Type, Name);
        }
    }
}
