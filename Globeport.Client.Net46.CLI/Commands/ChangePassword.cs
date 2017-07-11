using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Changes the current password")]
    class ChangePassword : Command
    {
        [Argument("The current password")]
        public string OldPassword { get; set; }

        [Argument("A new password")]
        public string NewPassword { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.ChangePassword(OldPassword, NewPassword);
        }
    }
}
