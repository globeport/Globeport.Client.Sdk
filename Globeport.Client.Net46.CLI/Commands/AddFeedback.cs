using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Data;
using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Adds a new feedback item")]
    class AddFeedback : Command
    {
        [Argument("The app name")]
        public string AppName { get; set; }

        [Argument("The feedback type")]
        public string Type { get; set; }

        [Argument("The feedback title")]
        public string Title { get; set; }

        [Argument("The portal Description")]
        public string Description { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.PostFeedback(AppName, Type, Title, Description);
        }
    }
}
