using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

using Globeport.Client.Net46.CLI.Attributes;
using Globeport.Shared.Library.Components;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Validation.Schema;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Adds a new entity")]
    class AddEntity : Command
    {
        [Argument("The entity model Id")]
        public string ModelId { get; set; }

        [Argument("The entity data (JSON object)")]
        public string Data { get; set; }

        [Argument("A list of portal Ids (JSON array)", true)]
        public string[] Portals { get; set; }


        protected async override Task<object> Execute()
        {
            var model = await GetModel(ModelId);

            var resources = await LoadResources(model);

            var imageValidator = new ConsoleImageValidator();

            var settings = new JSchemaReaderSettings { Validators = new List<JsonValidator> { imageValidator, new CellValidator(resources.Item2), new IdValidator(), new ColorValidator() } };

            var schema = JSchema.Parse(resources.Item1, settings);

            var json = JToken.Parse(Data);

            var isValid = json.IsValid(schema);

            if (!isValid)
            {
                WriteError("Private data is invalid against the schema\n\n" + resources.Item1);
                return null;
            }
            else
            {
                foreach(var item in imageValidator.ImagePaths)
                {
                    json.SelectToken(item.Key).Replace(item.Value);
                }
            }

            return await Api.Client.PostEntity(ModelId, json.ToObject<DataObject>(), imageValidator.Uploads, Portals ?? new string[0]);
        }

        async Task<Model> GetModel(string modelId)
        {
            var response = await Api.Client.GetModel(modelId);

            return response.Models.First();
        }

        async Task<Tuple<string, Tables>> LoadResources(Model model)
        {
            var tables = new Tables();

            string schema = null;

            var tasks = model.Dependencies.Select(async dependency =>
            {
                var url = $"{Api.Client.WebClient.StorageUri}/files/{dependency.FileId}";
                var file = await Api.Client.WebClient.GetMedia(url);
                switch (dependency.Type)
                {
                    case ResourceTypes.Schema:
                        schema = file.FromBytes();
                        break;
                    case ResourceTypes.Table:
                        tables.Add(dependency.Name, file.FromBytes().Deserialize<Table>());
                        break;
                }
            });

            await Task.WhenAll(tasks);

            return Tuple.Create(schema, tables);
        }
    }
}
