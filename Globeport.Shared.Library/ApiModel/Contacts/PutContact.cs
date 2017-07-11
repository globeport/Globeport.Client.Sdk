using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class PutContact : ApiRequest
    {
        public string ContactId { get; set; }
        public string AvatarId { get; set; }
        public List<string> AddPortals { get; set; }
        public List<string> RemovePortals { get; set; }

        public PutContact()
        {
        }

        public PutContact(string contactId, string avatarId, IEnumerable<string> addPortals, IEnumerable<string> removePortals) 
        {
            ContactId = contactId;
            AvatarId = avatarId;
            AddPortals = addPortals?.ToList();
            RemovePortals = removePortals?.ToList();
        }

        public override string GetPath()
        {
            return $"contacts/{ContactId}";
        }

        public override string GetLogContent()
        {
            return new { ContactId = ContactId, AvatarId = AvatarId, AddPortals = AddPortals, RemovePortals = RemovePortals }.Serialize();
        }
    }

    public class PutContactResponse : ApiResponse
    {
        public Contact Contact { get; set; }

        public PutContactResponse()  
        {
        }

        public PutContactResponse(Contact contact)
        {
            Contact = contact;
        }
    }
}
