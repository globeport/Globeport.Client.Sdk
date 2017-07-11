using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Interfaces
{
    public interface ISession
    {
        string SessionId { get; }
        byte[] SessionKey { get; }
        bool IsAuthenticated { get; set; }
    }
}
