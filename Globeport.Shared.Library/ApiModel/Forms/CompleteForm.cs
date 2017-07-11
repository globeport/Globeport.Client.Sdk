using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class CompleteForm : ApiRequest
    {
        public string FormId { get; set; }
        public List<AnswerUpload> Answers { get; set; }
        public List<SignalMessageUpload> Messages { get; set; }

        public CompleteForm()
        {
        }

        public CompleteForm(string formId, IEnumerable<AnswerUpload> answers, IEnumerable<SignalMessageUpload> messages)
        {
            FormId = formId;
            Answers = answers.ToList();
            Messages = messages.ToList();
        }

        public override string GetPath()
        {
            return $"forms/{FormId}/complete";
        }

        public override string GetLogContent()
        {
            return new { FormId = FormId, Answers = Answers }.Serialize();
        }
    }

    public class CompleteFormResponse : ApiResponse
    {
        public Form Form { get; set; }

        public CompleteFormResponse()
        {
        }

        public CompleteFormResponse(Form form)  
        {
            Form = form;
        }
    }
}
