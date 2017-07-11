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
    public class GetContacts : DataRequest
    {
        public List<string> Contacts { get; set; }
        public string Username { get; set; }
        public string PortalId { get; set; }
        public string Mode { get; set; }

        public GetContacts()
        {
        }

        public GetContacts(DataCursor cursor, string portalId, string mode)
            : base(cursor)
        {
            PortalId = portalId;
            Mode = mode;
        }

        public GetContacts(IEnumerable<string> contacts)
        {
            Contacts = contacts.ToList();
        }

        public GetContacts(string username)
        {
            Username = username;
        }

        public override string GetPath()
        {
            if (Contacts != null)
            {
                return $"contacts/{string.Join(",", Contacts)}";
            }
            else if (Username!=null)
            {
                return $"contacts?username={Username}";
            }
            else
            {
                return $"contacts?{GetQuery()}";
            }
        }

        public override string GetQuery()
        {
            return $"{base.GetQuery()}&portalId={PortalId}&mode={Mode}";
        }

        public override string GetLogContent()
        {
            return new { Contacts = Contacts, Username = Username, PortalId = PortalId, Mode = Mode, Cursor = Cursor }.Serialize();
        }
    }

    public class GetContactsResponse : DataResponse
    {
        public List<Contact> Contacts { get; set; }

        public GetContactsResponse()
        {
        }

        public GetContactsResponse(IEnumerable<Contact> contacts) 
        {
            Contacts = contacts.ToList();
        }
    }
}
