using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Interop
{
    public class HostState : IHostState
    {
        public Entity Entity { get; set; }
        public Account Account { get; set; }

        public HostState(Account account, Entity entity)
        {
            Account = account;
            Entity = entity;
        }
    }
}
