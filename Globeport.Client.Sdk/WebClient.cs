using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Compression;

using JWT;
using JWT.DNX.Json.Net;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Encoding;
using Globeport.Shared.Library.Interfaces;
using Globeport.Client.Sdk.Crypto;

namespace Globeport.Client.Sdk
{
    public abstract class WebClient
    {
        protected const string USER_AGENT = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
        public string WebUri { get; }
        public string ApiUri { get; }
        public string StorageUri { get; }

        DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        CryptoClient CryptoClient;

        static WebClient()
        {
            JsonWebToken.JsonSerializer = new JsonNetJWTSerializer();
        }
        public WebClient(ApiSettings settings, CryptoClient cryptoClient)
        {
            CryptoClient = cryptoClient;
            ApiUri = settings.ApiUri;
            StorageUri = settings.StorageUri;
            WebUri = settings.WebUri;
        }

        public abstract Task<TResponse> Get<TResponse>(ApiRequest request, ISession session = null, CancellationToken token = default(CancellationToken))
            where TResponse : ApiResponse, new();

        public abstract Task<TResponse> Post<TResponse>(ApiRequest request, ISession session = null, CancellationToken token = default(CancellationToken))
            where TResponse : ApiResponse, new();

        public abstract Task<TResponse> Put<TResponse>(ApiRequest request, ISession session = null, CancellationToken token = default(CancellationToken))
            where TResponse : ApiResponse, new();

        public abstract Task<TResponse> Delete<TResponse>(ApiRequest request, ISession session = null, CancellationToken token = default(CancellationToken))
            where TResponse : ApiResponse, new();

        public abstract Task<byte[]> GetMedia(string url, CancellationToken token = default(CancellationToken));
        public abstract Task<string> GetString(string url, CancellationToken token = default(CancellationToken));
        public abstract Task<Headers> GetHeaders(string url, CancellationToken token = default(CancellationToken));

        public async Task<HtmlGraph> GetGraph(string url, CancellationToken token = default(CancellationToken))
        {
            var headers = await GetHeaders(url, token).ConfigureAwait(false);

            var type = headers?.FirstOrDefault(i=>i.Key.ToLower() == "content-type").Value?.ToLower();

            if (type?.Contains("text/html") == true)
            {
                var content = await GetString(url, token).ConfigureAwait(false);

                return HtmlGraph.ParseHtml(url, content);
            }
            else if (type?.Contains("image/") == true)
            {
                return new HtmlGraph(url, "image");
            }
            else
            {
                return new HtmlGraph(url);
            }
        }

        protected string CreateJWT(string path, string body, ISession session)
        {
            var issued = Math.Round((DateTimeOffset.UtcNow - Epoch).TotalSeconds);
            var jti = CryptoClient.GenerateId();
            var hash = Base64.ToBase64Url(CryptoClient.GetMD5Hash($"/{path}".ToBytes().Combine(body.ToBytes())));

            var jwt = new Dictionary<string, object>
                    {
                        { "iss", session.SessionId },
                        { "iat", issued },
                        { "jti", jti },
                        {  "hash", hash }
                    };

            return JsonWebToken.Encode(jwt, session.SessionKey, JwtHashAlgorithm.HS256);
        }
    }
}