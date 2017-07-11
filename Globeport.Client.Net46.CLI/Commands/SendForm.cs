using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Shared.Library.Data;
using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Sends a new form")]
    class SendForm : Command
    {
        [Argument("A list of quesions (JSON array)")]
        public QuestionUpload[] Questions { get; set; }

        [Argument("A contact Id")]
        public string ContactId { get; set; }

        protected async override Task<object> Execute()
        { 
            return await Api.Client.SendForm(Questions, ContactId);
        }
    }
}
