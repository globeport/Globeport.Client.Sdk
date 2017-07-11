using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ApiModel
{
    public class PutFeedback : ApiRequest
    {
        public string FeedbackId { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Response { get; set; }

        public PutFeedback()
        {
        }

        public PutFeedback(string feedbackId, string label, string description, string status = null, string response = null)
        {
            FeedbackId = feedbackId;
            Label = label;
            Description = description;
            Status = status;
            Response = response;
        }

        public override string GetPath()
        {
            return $"feedback/{FeedbackId}";
        }

        public override string GetLogContent()
        {
            return new { FeedbackId = FeedbackId, Label = Label, Description = Description, Status = Status, Response = Response }.Serialize();
        }
    }

    public class PutFeedbackResponse : ApiResponse
    {
        public Feedback Feedback { get; set; }

        public PutFeedbackResponse()
        {
        }

        public PutFeedbackResponse(Feedback feedback)
        {
            Feedback = feedback;
        }
    }
}
