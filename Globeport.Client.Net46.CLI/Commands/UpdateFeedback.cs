using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Shared.Library.Data;
using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Updates an existing feedback item")]
    class UpdateFeedback : Command
    {
        [Argument("The feedback item Id")]
        public string FeedbackId { get; set; }

        [Argument("The feedback title")]
        public string Title { get; set; }

        [Argument("The portal Description")]
        public string Description { get; set; }

        [Argument("The feedback status", true)]
        public string Status { get; set; }

        [Argument("The feedback response", true)]
        public string Response { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.PutFeedback(FeedbackId, Title, Description, Status, Response);
        }
    }
}
