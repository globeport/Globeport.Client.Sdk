using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class DeleteFeedback : ApiRequest
    {
        public string FeedbackId { get; set; }

        public DeleteFeedback()
        {
        }

        public DeleteFeedback(string feedbackId)
        {
            FeedbackId = feedbackId;
        }

        public override string GetPath()
        {
            return $"feedback/{FeedbackId}";
        }

        public override string GetLogContent()
        {
            return new { FeebackId = FeedbackId }.Serialize();
        }
    }

    public class DeleteFeedbackResponse : ApiResponse
    {
        public DeleteFeedbackResponse()
        {
        }
    }
}
