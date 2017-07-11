using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class DeclineForm : ApiRequest
    {
        public string FormId { get; set; }

        public DeclineForm()
        {
        }

        public DeclineForm(string formId)
        {
            FormId = formId;
        }

        public override string GetPath()
        {
            return $"forms/{FormId}/decline";
        }

        public override string GetLogContent()
        {
            return new { FormId = FormId }.Serialize();
        }
    }

    public class DeclineFormResponse : ApiResponse
    {
        public Form Form { get; set; }

        public DeclineFormResponse()
        {
        }

        public DeclineFormResponse(Form form)  
        {
            Form = form;
        }
    }
}
