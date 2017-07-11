using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IResourceLoader
    {
        string GetString(string resource);
    }
}
