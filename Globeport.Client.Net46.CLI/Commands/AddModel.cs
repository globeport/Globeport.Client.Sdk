using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Adds a new model")]
    class AddModel : Command
    {
        [Argument("The model name")]
        public string ModelName { get; set; }

        [Argument("The model label")]
        public string Label { get; set; }

        [Argument("The mode image - path to an image")]
        public string Image { get; set; }

        [Argument("The model color")]
        public string Color { get; set; }

        [Argument("Set true to enable interactions")]
        public bool IsInteractive { get; set; }

        protected async override Task<object> Execute()
        {
            var upload = GetImageUpload(Image);

            if (upload == null)
            {
                WriteError("The image couldn't be found");
                return null;
            }

            if (upload.AspectRatio != 1)
            {
                WriteError("The image should have an aspect ratio of 1");
                return null;
            }

            return await Api.Client.PostModel(ModelName, Label, Color, IsInteractive, upload);
        }
    }
}
