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
    public class GetNotifications : DataRequest
    {
        public bool Dependencies { get; set; }

        public GetNotifications(DataCursor cursor, bool dependencies = true)
            : base(cursor)
        {
            Dependencies = dependencies;
        }

        public override string GetPath()
        {
            return $"notifications?{GetQuery()}&dependencies={Dependencies}";
        }

        public override string GetLogContent()
        {
            return new { Dependencies = Dependencies, Cursor = Cursor }.Serialize();
        }
    }

    public class GetNotificationsResponse : DataResponse
    {
        public List<Notification> Notifications { get; set; }
        public Dictionary<string, DateTimeOffset> Contacts { get; set; }
        public Dictionary<string, DateTimeOffset> Portals { get; set; }
        public Dictionary<string, DateTimeOffset> Forms { get; set; }
        public Dictionary<string, DateTimeOffset> Models { get; set; }
        public Dictionary<string, DateTimeOffset> Classes { get; set; }

        public GetNotificationsResponse()
        {
        }

        public GetNotificationsResponse(IEnumerable<Notification> notifications)
        {
            Notifications = notifications.ToList();
        }

        public GetNotificationsResponse(IEnumerable<Notification> notifications, Dictionary<string, DateTimeOffset> contacts, Dictionary<string, DateTimeOffset> portals, Dictionary<string, DateTimeOffset> forms, Dictionary<string, DateTimeOffset> models, Dictionary<string, DateTimeOffset> classes) 
        {
            Notifications = notifications.ToList();
            Contacts = contacts;
            Portals = portals;
            Forms = forms;
            Models = models;
            Classes = classes;
        }
    }
}
