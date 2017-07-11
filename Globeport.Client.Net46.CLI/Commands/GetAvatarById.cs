using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets an avatar by its Id")]
    class GetAvatarById : Command
    {
        [Argument("The avatar Id")]
        public string AvatarId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.GetAvatar(AvatarId);
        }
    }
}
