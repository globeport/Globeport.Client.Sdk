using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Components;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IEntityRequest
    {
        DataObject Data { get; set; }
        List<MediaUpload> MediaUploads { get; set; }
    }
}
