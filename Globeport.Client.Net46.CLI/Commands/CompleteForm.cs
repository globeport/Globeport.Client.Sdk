using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Data;
using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Submits a completed form")]
    class CompleteForm : Command
    {
        [Argument("The form Id")]
        public string FormId { get; set; }

        [Argument("A list of answers (JSON array)")]
        public AnswerUpload[] Answers { get; set; }

        protected async override Task<object> Execute()
        {
            var formResponse = await Api.Client.GetForm(FormId, false);

            var form = formResponse.Forms.FirstOrDefault();

            if (form == null)
            {
                WriteError("The form doesn't exist");
                return null;
            }

            var entityIds = Answers.Select(i => i.EntityId).Distinct();

            var entitiesResponse = await Api.Client.GetEntities(entityIds, false);

            var entities = entitiesResponse.Entities.ToDictionary(i => i.Id);

            if (entityIds.Count() != entities.Count)
            {
                WriteError("At least one answer references an entity that doesn't exist");
                return null;
            }

            var answers = Answers.Select(i => new Answer(i.QuestionId, entities[i.EntityId]));

            return await Api.Client.CompleteForm(form.ContactId, form.Id, answers);

            throw new NotImplementedException();
        }
    }
}
