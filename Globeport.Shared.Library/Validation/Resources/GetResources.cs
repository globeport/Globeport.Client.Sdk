using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Validation
{
    public class GetResourcesValidator: AbstractValidator<GetResources>
    {
        DataCursorValidator DataCursorValidator { get; } = new DataCursorValidator(IsValidIndex);

        public GetResourcesValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Cursor).NotNull().SetValidator(DataCursorValidator).DependentRules(i =>
            {
                i.RuleFor(j => j.Type).Must(j => typeof(ResourceTypes).GetConstants().ContainsKey(j)).When(j => j.Type != null);
            }).When(i => i.Resources == null);

            RuleFor(i => i.Resources).NotNull().Must(i => i.Count() <= Globals.MaxGetCount && i.All(Validators.IsValidId)).DependentRules(i =>
            {
                i.RuleFor(j => j.Type).Null();
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
