using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Adds a new contact")]
    class AddContact : Command
    {
        [Argument("The contact Id")]
        public string ContactId { get; set; }

        [Argument("The shared avatar Id", true)]
        public string AvatarId { get; set; }

        [Argument("A list of shared portal Ids (JSON array)", true)]
        public string[] Portals { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.PostContact(ContactId, AvatarId, Portals);
        }
    }
}
