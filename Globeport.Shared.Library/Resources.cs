using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Resources;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library
{
    public static class Resources
    {
        public static IResourceLoader Loader { get; set; } = new ResourceLoader();

        public static string GetString(string name)
        {
            return Loader.GetString(name);
        }
    }

    public class ResourceLoader : IResourceLoader
    {
        ResourceManager ResourceManager = new ResourceManager("Globeport.Shared.Library.Strings", typeof(ResourceLoader).GetTypeInfo().Assembly);

        public string GetString(string name)
        {
            return ResourceManager.GetString(name);
        }
    }
}
