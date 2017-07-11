using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Deletes an existing contact")]
    class DeleteContact : Command
    {
        [Argument("The contact Id")]
        public string ContactId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.DeleteContact(ContactId);
        }
    }
}
