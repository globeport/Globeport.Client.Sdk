using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Data
{
    public class AutoLinkOptions : IAutoLinkOptions
    {
        public string mention { get; set; }
    }
}
