using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
using System.IO;

using Globeport.Client.Sdk;
using Globeport.Client.Net46.Sdk.Crypto;
using Globeport.Client.Net46.Sdk;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Data;

namespace Globeport.Client.Net46.CLI
{
    public static class Api
    {
        public static ApiClient Client { get; }
        public static AppSettings AppSettings { get;}

        static Api()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Globeport.Client.Net46.CLI.appsettings.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                AppSettings = reader.ReadToEnd().Deserialize<AppSettings>();
            }

            var settings = new ApiSettings
            {
                ApiUri = AppSettings.GetEnvironmentSettings()["ApiUri"],
                StorageUri = AppSettings.GetEnvironmentSettings()["StorageUri"],
                WebUri = AppSettings.GetEnvironmentSettings()["WebUri"]
            };

            var cryptoClient = new Net46CryptoClient();

            var webClient = new Net46WebClient(settings, cryptoClient);

            Client = new ApiClient(webClient, cryptoClient);
        }
    }
}
