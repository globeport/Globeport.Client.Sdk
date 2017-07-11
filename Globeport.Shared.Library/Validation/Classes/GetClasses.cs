using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class GetClassesValidator: AbstractValidator<GetClasses>
    {
        DataCursorValidator DataCursorValidator { get; } = new DataCursorValidator(IsValidIndex);

        public GetClassesValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Cursor).NotNull().SetValidator(DataCursorValidator).DependentRules(i =>
            {
                i.RuleFor(j => j.PortalId).Must(Validators.IsValidId).When(j => j.PortalId != null);
            }).When(i => i.Classes == null);

            RuleFor(i => i.Classes).NotNull().Must(i => i.Count() <= Globals.MaxGetCount && i.All(Validators.IsValidId)).DependentRules(i =>
            {
                i.RuleFor(j => j.PortalId).Null();
            }).When(i => i.Cursor == null);
        }

        public static bool IsValidIndex(string[] index)
        {
            if (index == null) return true;
            if (index.Length != 2) return false;
            if (!Validators.IsValidShortName(index[0])) return false;
            if (!Validators.IsValidId(index[1])) return false;
            return true;
        }
    }
}
