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
    public class GetKeysValidator: AbstractValidator<GetKeys>
    {
        public GetKeysValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Keys).NotNull().Must((i,j) => j.Count() <= Globals.MaxGetCount && j.All(k=>KeyUploadValidator.IsValidKeyId(k)));
        }
    }
}
