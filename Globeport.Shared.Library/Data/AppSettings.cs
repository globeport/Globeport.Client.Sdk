using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public class AppSettings
    {
        public string Environment { get; set; }

        public Dictionary<string, string> Development { get; set; }
        public Dictionary<string, string> Test { get; set; }
        public Dictionary<string, string> Production { get; set; }

        public Dictionary<string,string> GetEnvironmentSettings()
        {
            switch(Environment)
            {
                case nameof(Development):
                    return Development;
                case nameof(Test):
                    return Test;
                case nameof(Production):
                    return Production;
            }
            throw new NotSupportedException();
        }
    }
}
