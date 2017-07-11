using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetFeedback : DataRequest
    {
        public string AppName { get; set; }
        public string Type { get; set; }
        public bool Dependencies { get; set; }

        public GetFeedback(DataCursor cursor, string appName, string type, bool dependencies = true)
            : base(cursor)
        {
            AppName = appName;
            Type = type;
            Dependencies = dependencies;
        }

        public override string GetPath()
        {
            return $"feedback?{GetQuery()}&AppName={AppName}&Type={Type}&dependencies={Dependencies}";
        }

        public override string GetLogContent()
        {
            return new { AppName = AppName, Type = Type, Dependencies = Dependencies, Cursor = Cursor }.Serialize();
        }
    }

    public class GetFeedbackResponse : DataResponse
    {
        public List<Feedback> Feedback { get; set; }
        public Dictionary<string, DateTimeOffset> Contacts { get; set; }

        public GetFeedbackResponse()
        {
        }

        public GetFeedbackResponse(IEnumerable<Feedback> feedback)
        {
            Feedback = feedback.ToList();
        }

        public GetFeedbackResponse(IEnumerable<Feedback> feedback, Dictionary<string, DateTimeOffset> contacts) 
        {
            Feedback = feedback.ToList();
            Contacts = contacts;
        }
    }
}
