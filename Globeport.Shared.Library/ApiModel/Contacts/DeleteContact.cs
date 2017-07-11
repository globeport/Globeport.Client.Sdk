using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class DeleteContact : ApiRequest
    {
        public string ContactId { get; set; }

        public DeleteContact()
        {
        }

        public DeleteContact(string contactId)
        {
            ContactId = contactId;
        }

        public override string GetPath()
        {
            return $"contacts/{ContactId}";
        }

        public override string GetLogContent()
        {
            return new { ContactId = ContactId }.Serialize();
        }
    }

    public class DeleteContactResponse : ApiResponse
    {
        public Contact Contact { get; set; }

        public DeleteContactResponse()
        {
        }

        public DeleteContactResponse(Contact contact)
        {
            Contact = contact;
        }
    }
}
