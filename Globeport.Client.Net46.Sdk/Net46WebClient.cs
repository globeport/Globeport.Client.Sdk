using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Exceptions;
using Globeport.Shared.Library.Extensions;
using Globeport.Client.Sdk;
using Globeport.Client.Sdk.Crypto;
using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Components;
using Globeport.Shared.Library.Data;

namespace Globeport.Client.Net46.Sdk
{
    public class Net46WebClient : WebClient
    {
        ObjectPool<HttpClient> ApiClients { get; }
        ObjectPool<HttpClient> Clients { get; }

        public Net46WebClient(ApiSettings settings, CryptoClient cryptoClient) 
            : base(settings, cryptoClient)
        {
            Clients = new ObjectPool<HttpClient>(6, () => Task.FromResult(CreateHttpClient()));
            ApiClients = new ObjectPool<HttpClient>(6, () => Task.FromResult(CreateHttpClient()));
        }

        public async override Task<TResponse> Get<TResponse>(ApiRequest request, ISession session = null, CancellationToken token = default(CancellationToken))
        {
            var client = await GetApiClient().ConfigureAwait(false);

            try
            {
                var path = $"v1.0/{request.GetPath()}";

                var body = string.Empty;

                PrepareClient(client, session, path, body);

                var response = await client.GetAsync(new Uri($"{ApiUri}/{path}"));

                return await ProcessResponse<TResponse>(response, session, token).ConfigureAwait(false);
            }
            catch (Exception e) when (!(e is ApiException || e is OperationCanceledException))
            {
                throw new ConnectionErrorException();
            }
            finally
            {
                ReleaseApiClient(client);
            }
        }

        public async override Task<TResponse> Post<TResponse>(ApiRequest request, ISession session = null, CancellationToken token = default(CancellationToken))
        {
            var client = await GetApiClient().ConfigureAwait(false);

            try
            {
                var path = $"v1.0/{request.GetPath()}";

                var body = request.Serialize();

                PrepareClient(client, session, path, body);

                var response = await client.PostAsync(new Uri($"{ApiUri}/{path}"), new StringContent(body)).ConfigureAwait(false);

                return await ProcessResponse<TResponse>(response, session, token).ConfigureAwait(false);
            }
            catch(Exception e) when (!(e is ApiException || e is OperationCanceledException))
            {
                throw new ConnectionErrorException();
            }
            finally
            {
                ReleaseApiClient(client);
            }
        }

        public async override Task<TResponse> Put<TResponse>(ApiRequest request, ISession session = null, CancellationToken token = default(CancellationToken))
        {
            var client = await GetApiClient().ConfigureAwait(false);

            try
            {
                var path = $"v1.0/{request.GetPath()}";

                var body = request.Serialize();

                PrepareClient(client, session, path, body);

                var response = await client.PutAsync(new Uri($"{ApiUri}/{path}"), new StringContent(body)).ConfigureAwait(false);

                return await ProcessResponse<TResponse>(response, session, token).ConfigureAwait(false);
            }
            catch (Exception e) when (!(e is ApiException || e is OperationCanceledException))
            {
                throw new ConnectionErrorException();
            }
            finally
            {
                ReleaseApiClient(client);
            }
        }

        public async override Task<TResponse> Delete<TResponse>(ApiRequest request, ISession session = null, CancellationToken token = default(CancellationToken))
        {
            var client = await GetApiClient().ConfigureAwait(false);

            try
            {
                var path = $"v1.0/{request.GetPath()}";

                var body = string.Empty;

                PrepareClient(client, session, path, body);

                var response = await client.DeleteAsync(new Uri($"{ApiUri}/{path}")).ConfigureAwait(false);

                return await ProcessResponse<TResponse>(response, session, token).ConfigureAwait(false);
            }
            catch (Exception e) when (!(e is ApiException || e is OperationCanceledException))
            {
                throw new ConnectionErrorException();
            }
            finally
            {
                ReleaseApiClient(client);
            }
        }

        public async override Task<byte[]> GetMedia(string url, CancellationToken token = default(CancellationToken))
        {
            var client = await GetClient().ConfigureAwait(false);

            try
            {
                var response = await client.GetAsync(new Uri(url)).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode || token.IsCancellationRequested) return null;
                return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                ReleaseClient(client);
            }
        }

        public async override Task<string> GetString(string url, CancellationToken token = default(CancellationToken))
        {
            var client = await GetClient().ConfigureAwait(false);

            try
            { 
                var uri = new UriBuilder(url).Uri;
                var content = await client.GetStringAsync(uri).ConfigureAwait(false);
                if (token.IsCancellationRequested) return null;
                return content;
            }
            catch
            {
                return null;
            }
            finally
            {
                ReleaseClient(client);
            }
        }

        public async override Task<Headers> GetHeaders(string url, CancellationToken token = default(CancellationToken))
        {
            var client = await GetClient().ConfigureAwait(false);

            try
            {
                var uri = new UriBuilder(url).Uri;
                var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                if (token.IsCancellationRequested) return null;
                return new Headers(response.Content.Headers.ToDictionary(i=>i.Key.ToLowerInvariant(), i=>i.Value.First()));
            }
            catch
            {
                return null;
            }
            finally
            {
                ReleaseClient(client);
            }
        }

        void PrepareClient(HttpClient client, ISession session, string path, string body)
        {
            if (session != null)
            {
                client.DefaultRequestHeaders.Add("SessionId", session.SessionId);
                if (session.IsAuthenticated)
                {
                    var jwt = CreateJWT(path, body, session);
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
                }
            }
        }

        async Task<T> ProcessResponse<T>(HttpResponseMessage message, ISession session, CancellationToken token = default(CancellationToken)) where T : ApiResponse
        {
            if (!message.IsSuccessStatusCode)
            {
                switch (message.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        session.IsAuthenticated = false;
                        throw new UnauthorisedException();
                    default:
                        throw new ConnectionErrorException();
                }
            }

            token.ThrowIfCancellationRequested();

            var json = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

            token.ThrowIfCancellationRequested();

            var apiResponse = json.Deserialize<T>();

            apiResponse.ThrowOnError();

            return apiResponse;
        }

        HttpClient CreateHttpClient()
        {
            var filter = new HttpClientHandler {  AutomaticDecompression = System.Net.DecompressionMethods.GZip };

            var client = new HttpClient(filter);

            client.DefaultRequestHeaders.Add("User-Agent", USER_AGENT);

            return client;
        }

        async Task<HttpClient> GetApiClient()
        {
            var client = await ApiClients.GetObject().ConfigureAwait(false);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        void ReleaseApiClient(HttpClient client)
        {
            ApiClients.PutObject(client);
        }

        Task<HttpClient> GetClient()
        {
            return Clients.GetObject();
        }

        void ReleaseClient(HttpClient client)
        {
            Clients.PutObject(client);
        }
    }
}