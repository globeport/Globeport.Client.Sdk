using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class GetEntitiesValidator: AbstractValidator<GetEntities>
    {
        public DataCursorValidator DataCursorValidator { get; } = new DataCursorValidator(IsValidIndex);

        public GetEntitiesValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Cursor).NotNull().SetValidator(DataCursorValidator).DependentRules(i =>
            {
                i.RuleFor(j => j.PortalId).Must(Validators.IsValidId).When(j => j.PortalId != null);
                i.RuleFor(j => j.ModelId).Must(Validators.IsValidId).When(j => j.ModelId != null);
            }).When(i => i.Entities == null);

            RuleFor(i => i.Entities).NotNull().Must(i => i.Count() <= Globals.MaxGetCount && i.All(Validators.IsValidId)).DependentRules(i =>
            {
                i.RuleFor(j => j.PortalId).Null();
                i.RuleFor(j => j.ModelId).Null();
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
