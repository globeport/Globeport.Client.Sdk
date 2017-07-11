using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;


using Globeport.Shared.Library.Data;
using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Create a new account")]
    class SignUp : Command
    {
        [Argument("a valid username")]
        public string Username { get; set; }

        [Argument("a valid password")]
        public string Password { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.SignUp(Username, Password, FingerPrint.Value, Platforms.Windows);
        }
    }
}
