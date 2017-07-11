using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Updates an existing contact")]
    class UpdateContact : Command
    {
        [Argument("The contact Id")]
        public string ContactId { get; set; }

        [Argument("The shared avatar Id", true)]
        public string AvatarId { get; set; }

        [Argument("A list portal Ids to add (JSON array)", true)]
        public string[] AddPortals { get; set; }

        [Argument("A list of portal Ids to remove (JSON array)", true)]
        public string[] RemovePortals { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.PutContact(ContactId, AvatarId, AddPortals, RemovePortals);
        }
    }
}
