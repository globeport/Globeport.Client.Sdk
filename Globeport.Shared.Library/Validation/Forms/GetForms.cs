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
    public class GetFormsValidator: AbstractValidator<GetForms>
    {
        DataCursorValidator DataCursorValidator { get; } = new DataCursorValidator(IsValidIndex);

        public GetFormsValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Forms).NotNull().Must(i => i.Count() <= Globals.MaxGetCount && i.All(Validators.IsValidId));
        }

        public static bool IsValidIndex(string[] index)
        {
            if (index == null) return true;
            if (index.Length != 1) return false;
            if (!Validators.IsValidId(index[0])) return false;
            return true;
        }
    }
}
