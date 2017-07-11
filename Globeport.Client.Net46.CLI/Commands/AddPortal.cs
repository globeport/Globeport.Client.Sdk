using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Data;
using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Adds a new portal")]
    class AddPortal : Command
    {
        [Argument("The portal type")]
        public string Type { get; set; }

        [Argument("The portal name")]
        public string PortalName { get; set; }

        [Argument("The portal Description")]
        public string Description { get; set; }

        [Argument("The portal image - path to an image")]
        public string Image { get; set; }

        [Argument("The portal color")]
        public string Color { get; set; }

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

            return await Api.Client.PostPortal(Type, PortalName, Description, Color, upload);
        }
    }
}
