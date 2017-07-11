using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Vote on an existing feedback item")]
    class VoteFeedback : Command
    {
        [Argument("The feedback item Id")]
        public string FeedbackId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.VoteFeedback(FeedbackId);
        }
    }
}
