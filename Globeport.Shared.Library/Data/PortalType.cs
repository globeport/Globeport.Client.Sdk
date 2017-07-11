using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public static class PortalType
    {
        public const string Profile = nameof(Profile);
        public const string List = nameof(List);
        public const string Group = nameof(Group);
        public const string Contact = nameof(Contact);

        public static int GetIndex(string type)
        {
            switch(type)
            {
                case nameof(Profile):
                    return 1;
                case nameof(List):
                    return 2;
                case nameof(Group):
                    return 3;
                case nameof(Contact):
                    return 4;
            }
            throw new ArgumentException();
        }
    }
}
