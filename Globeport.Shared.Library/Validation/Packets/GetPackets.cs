using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class GetPacketsValidator: AbstractValidator<GetPackets>
    {
        public GetPacketsValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Packets).NotNull().Must((i,j) => j.Count() <= Globals.MaxGetCount && j.All(k=>PacketUploadValidator.IsValidPacketId(k)));
        }
    }
}
