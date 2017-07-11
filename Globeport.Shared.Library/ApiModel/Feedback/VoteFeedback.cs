using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class VoteFeedback : ApiRequest
    {
        public string FeedbackId { get; set; }

        public VoteFeedback()
        {
        }

        public VoteFeedback(string feedbackId)
        {
            FeedbackId = feedbackId;
        }

        public override string GetPath()
        {
            return "feedback/vote";
        }

        public override string GetLogContent()
        {
            return new { FeebackId = FeedbackId }.Serialize();
        }
    }

    public class VoteFeedbackResponse : ApiResponse
    {
        public Feedback Feedback { get; set; }

        public VoteFeedbackResponse()
        {
        }

        public VoteFeedbackResponse(Feedback feedback)
        {
            Feedback = feedback;
        }
    }
}
