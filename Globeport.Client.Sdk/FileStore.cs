using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Data;

namespace Globeport.Client.Sdk
{
    public class FileStore
    {
        public ApiClient ApiClient { get; }

        public FileStore(ApiClient apiClient)
        {
            ApiClient = apiClient;
        }

        public async virtual Task<string> GetSchema(string modelId)
        {
            var response = await ApiClient.GetModel(modelId).ConfigureAwait(false);

            var model = response.Models.First();

            var fileId = model.Dependencies.First(i => i.Type == ResourceTypes.Schema).FileId;

            var url = $"{ApiClient.WebClient.StorageUri}/files/{fileId}";

            var file = await ApiClient.WebClient.GetMedia(url).ConfigureAwait(false);

            return file.FromBytes();
        }
    }
}
