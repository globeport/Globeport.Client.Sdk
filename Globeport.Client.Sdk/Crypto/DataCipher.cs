using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;

using Globeport.Shared.Library.Components;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Client.Sdk.Crypto
{
    public class DataCipher
    {
        public DataObject PrivateData { get; private set; }
        public DataObject PublicData { get; private set; }
        public List<string> Media { get; private set; }
        public JSchema Schema { get; }
        public List<MediaUpload> Uploads { get; }
        public DataObject Data { get; }

        CipherValidator Validator;

        public DataCipher(CryptoClient cryptoClient, byte[] privateIdentityKey, DataObject data, string schema, List<MediaUpload> uploads)
        {
            Data = data;
            Uploads = uploads ?? new List<MediaUpload>();
            Validator = new CipherValidator(data, Uploads, cryptoClient, privateIdentityKey);
            Schema = JSchema.Parse(schema, new JSchemaReaderSettings { Validators = new List<JsonValidator> { Validator } });
        }

        public DataCipher Process()
        {
            foreach (var upload in Uploads)
            {
                upload.IsValid = false;
            }
            var token = JToken.FromObject(Data);
            token.Validate(Schema);
            PublicData = Validator.PublicData.ToObject<DataObject>();
            PrivateData = Validator.PrivateData.ToObject<DataObject>();
            Media = Validator.Media;
            return this;
        }
    }

    public class CipherValidator : JsonValidator
    {
        List<MediaUpload> Uploads { get; }
        CryptoClient CryptoClient { get; }
        byte[] PrivateIdentityKey { get; }
        public JObject PrivateData { get; } = new JObject();
        public JObject PublicData { get; } = new JObject();
        public List<string> Media { get; } = new List<string>();

        public CipherValidator(DataObject data, List<MediaUpload> uploads, CryptoClient cryptoClient, byte[] privateIdentityKey)
        {
            PrivateData = JObject.FromObject(data);
            PublicData = JObject.FromObject(data);
            Uploads = uploads;
            CryptoClient = cryptoClient;
            PrivateIdentityKey = privateIdentityKey;
        }

        public override bool CanValidate(JSchema schema)
        {
            return true;
        }

        public override void Validate(JToken token, JsonValidatorContext context)
        {
            if (token.Type.In(JTokenType.String, JTokenType.Float, JTokenType.Integer, JTokenType.Date, JTokenType.Boolean, JTokenType.Guid, JTokenType.TimeSpan, JTokenType.Uri, JTokenType.Bytes))
            {
                var encrypt = (bool?)context.Schema.ExtensionData.GetValue("encrypt") ?? true;

                switch (context.Schema.Format)
                {
                    case "image":
                    case "ink":
                        var value = token.ToString();
                        var upload = Uploads?.FirstOrDefault(i => i.Id == value);
                        if (upload != null)
                        {
                            if (encrypt)
                            {
                                upload.KeyId = CryptoClient.GenerateId();
                                upload.Key = CryptoClient.GenerateSecretKey();
                                foreach (var file in upload.Files)
                                {
                                    file.Data = CryptoClient.Encrypt(file.Data, upload.Key);
                                }
                            }
                            upload.IsValid = true;
                        }
                        PrivateData.SelectToken(token.Path).Replace(null);
                        Media.Add(value);
                        return;
                    default:
                        if (!encrypt) 
                        {
                            PrivateData.SelectToken(token.Path).Replace(null);
                            return;
                        }
                        break;
                }

                PublicData.SelectToken(token.Path).Replace(null);
            }
        }
    }
}
