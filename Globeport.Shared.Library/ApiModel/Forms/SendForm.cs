using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class SendForm : ApiRequest
    {
        public List<QuestionUpload> Questions { get; set; }
        public string ContactId { get; set; }

        public SendForm()
        {
        }

        public SendForm(string contactId, IEnumerable<QuestionUpload> questions)
        {
            Questions = questions.ToList();
            ContactId = contactId;
        }

        public override string GetPath()
        {
            return "forms";
        }

        public override string GetLogContent()
        {
            return new { Questions = Questions, ContactId = ContactId }.Serialize();
        }
    }

    public class SendFormResponse : ApiResponse
    {
        public Form Form { get; set; }

        public SendFormResponse()
        {
        }

        public SendFormResponse(Form form)  
        {
            Form = form;
        }
    }
}
