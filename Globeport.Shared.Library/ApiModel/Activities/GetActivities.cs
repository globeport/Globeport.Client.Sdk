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
    public class GetActivities : DataRequest
    {
        public bool Dependencies { get; set; }

        public GetActivities(DataCursor cursor, bool dependencies = true)
            : base(cursor)
        {
            Dependencies = dependencies;
        }

        public override string GetPath()
        {
            return $"activities?{GetQuery()}&dependencies={Dependencies}";
        }

        public override string GetLogContent()
        {
            return new { Cursor = Cursor, Dependencies = Dependencies }.Serialize();
        }
    }

    public class GetActivitiesResponse : DataResponse
    {
        public List<Activity> Activities { get; set; }
        public Dictionary<string, DateTimeOffset> Contacts { get; set; }
        public Dictionary<string, DateTimeOffset> Portals { get; set; }
        public Dictionary<string, DateTimeOffset> Forms { get; set; }
        public Dictionary<string, DateTimeOffset> Models { get; set; }
        public Dictionary<string, DateTimeOffset> Classes { get; set; }

        public GetActivitiesResponse()
        {
        }

        public GetActivitiesResponse(IEnumerable<Activity> activities)
        {
            Activities = activities.ToList();
        }

        public GetActivitiesResponse(IEnumerable<Activity> activities, Dictionary<string, DateTimeOffset> contacts, Dictionary<string, DateTimeOffset> portals, Dictionary<string, DateTimeOffset> forms, Dictionary<string, DateTimeOffset> models, Dictionary<string, DateTimeOffset> classes)
        {
            Activities = activities.ToList();
            Contacts = contacts;
            Portals = portals;
            Forms = forms;
            Models = models;
            Classes = classes;
        }
    }
}
