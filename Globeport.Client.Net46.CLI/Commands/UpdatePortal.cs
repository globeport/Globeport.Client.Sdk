using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Validation;
using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Updates an existing portal")]
    class UpdatePortal : Command
    {
        [Argument("The portal Id")]
        public string PortalId { get; set; }

        [Argument("The portal name")]
        public string PortalName { get; set; }

        [Argument("The portal Description")]
        public string Description { get; set; }

        [Argument("The portal color")]
        public string Color { get; set; }

        [Argument("The portal image - path to an image", true)]
        public string Image { get; set; }

        protected async override Task<object> Execute()
        {
            if (Image != null)
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

                return await Api.Client.PutPortal(PortalId, PortalName, Description, Color, upload);
            }
            else
            {
                return await Api.Client.PutPortal(PortalId, PortalName, Description, Color, null);
            }
        }
    }
}
