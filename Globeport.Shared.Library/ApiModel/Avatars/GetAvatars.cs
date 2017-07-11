using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetAvatars : DataRequest
    {
        public List<string> Avatars { get; set; }
        public string AccountId { get; set; }
        public string Search { get; set; }
        public string PortalId { get; set; }

        public GetAvatars()
        {
        }

        public GetAvatars(DataCursor cursor, string search, string accountId, string portalId)
            : base(cursor)
        {
            AccountId = accountId;
            Search = search;
            PortalId = portalId;
        }

        public GetAvatars(IEnumerable<string> avatars)
        {
            Avatars = avatars.ToList();
        }

        public override string GetPath()
        {
            if (Avatars != null)
            {
                return $"avatars/{string.Join(",", Avatars)}";
            }
            else
            {
                return $"avatars?{GetQuery()}&accountId={AccountId}&search={Search}&portalId={PortalId}";
            }
        }

        public override string GetLogContent()
        {
            return new { Avatar = Avatars, AccountId = AccountId, Search = Search, Cursor = Cursor, PortalId = PortalId }.Serialize();
        }
    }

    public class GetAvatarsResponse : DataResponse
    {
        public List<Avatar> Avatars { get; set; }

        public GetAvatarsResponse() { }

        public GetAvatarsResponse(IEnumerable<Avatar> avatars)
        {
            Avatars = avatars.ToList();
        }
    }
}