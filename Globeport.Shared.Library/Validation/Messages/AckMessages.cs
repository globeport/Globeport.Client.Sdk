using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class AckMessagesValidator: AbstractValidator<AckMessages>
    {
        public AckMessagesValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.LastMessageId).NotNull().Must(Validators.IsValidId);
        }
    }
}
