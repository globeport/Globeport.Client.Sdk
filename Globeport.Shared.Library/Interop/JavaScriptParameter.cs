using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Interop
{
    public class JavaScriptParameter
    {
        public string Expression { get; }

        public JavaScriptParameter(string expression)
        {
            Expression = expression;
        }
    }
}
