using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class Authenticate : ApiRequest
    {
        public byte[] Credentials { get; set; }
        public byte[] Evidence { get; set; }
        public string HardwareId { get; set; }
        public string Platform { get; set; }
        public string Culture { get; set; }

        public Authenticate()
        {
        }

        public Authenticate(byte[] credentials, byte[] evidence, string hardwareId, string platform, string culture)
        {
            Credentials = credentials;
            Evidence = evidence;
            HardwareId = hardwareId;
            Platform = platform;
            Culture = culture;
        }

        public override string GetPath()
        {
            return "authenticate";
        }

        public override string GetLogContent()
        {
            return new { Platform = Platform, Culture = Culture, HardwareId = HardwareId }.Serialize();
        }
    }

    public class AuthenticateResponse : ApiResponse
    {
        public byte[] Evidence { get; set; }
        public Account Account { get; set; }
        public List<Key> Keys { get; set; }
        public List<Class> Classes { get; set; }
        public List<Model> Models { get; set; }
        public List<Portal> Portals { get; set; }

        public AuthenticateResponse() { }

        public AuthenticateResponse(byte[] evidence, Account account, IEnumerable<Key> keys, IEnumerable<Class> classes, IEnumerable<Model> models, IEnumerable<Portal> portals)
        {
            Evidence = evidence;
            Account = account;
            Keys = keys.ToList();
            Classes = classes.ToList();
            Models = models.ToList();
            Portals = portals.ToList();
        }
    }
}
