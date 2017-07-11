using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class PostContact : ApiRequest
    {
        public string ContactId { get; set; }
        public string AvatarId { get; set; }
        public List<string> Portals { get; set; }
       
        public PostContact()
        {
        }

        public PostContact(string contactId, string avatarId, IEnumerable<string> portals)
        {
            ContactId = contactId;
            AvatarId = avatarId;
            Portals = portals?.ToList();
        }

        public override string GetPath()
        {
            return "contacts";
        }

        public override string GetLogContent()
        {
            return new { ContactId = ContactId, AvatarId = AvatarId, Portals = Portals}.Serialize();
        }
    }

    public class PostContactResponse : ApiResponse
    {
        public Contact Contact { get; set; }

        public PostContactResponse()  
        {
        }

        public PostContactResponse(Contact contact)
        {
            Contact = contact;
        }
    }
}
