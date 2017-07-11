using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class PostFeedback : ApiRequest
    {
        public string AppName { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }

        public PostFeedback()
        {
        }

        public PostFeedback(string appName, string type, string label, string description)
        {
            AppName = appName;
            Type = type;
            Label = label;
            Description = description;
        }

        public override string GetPath()
        {
            return "feedback";
        }

        public override string GetLogContent()
        {
            return new { AppName = AppName, Type = Type, Label = Label, Description = Description }.Serialize();
        }
    }

    public class PostFeedbackResponse : ApiResponse
    {
        public Feedback Feedback { get; set; }

        public PostFeedbackResponse()
        {
        }

        public PostFeedbackResponse(Feedback feedback)
        {
            Feedback = feedback;
        }
    }
}
