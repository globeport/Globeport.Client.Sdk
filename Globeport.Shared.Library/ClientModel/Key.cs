using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Attributes;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ClientModel
{
    public class Key : ClientObject
    {
        public string KeyId { get; set; }
        public string Type { get; set; }
        public byte[] Value { get; set; }
        public byte[] Signature { get; set; }
        [Ignore]
        public bool IsEncrypted { get; set; }

        public Key()
        {
        }

        public Key(string id) 
            : base(id)
        {
        }

        public Key(string keyId, string type, byte[] value, byte[] signature = null, bool isEncrypted = false)
            : base(GetId(type, keyId))
        {
            KeyId = keyId;
            Type = type;
            Value = value;
            Signature = signature;
            IsEncrypted = isEncrypted;
        }

        public string GetId()
        {
            return GetId(Type, KeyId);
        }

        public static string GetId(string type, string keyId)
        {
            return $"{type}.{keyId}";
        }

        public KeyUpload GetUpload()
        {
            return new KeyUpload(KeyId, Type, Value, Signature);
        }
    }
}
