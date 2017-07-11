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
    public class GetModelsValidator: AbstractValidator<GetModels>
    {
        DataCursorValidator DataCursorValidator { get; } = new DataCursorValidator(IsValidIndex);

        public GetModelsValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Cursor).NotNull().SetValidator(DataCursorValidator).DependentRules(i =>
            {
                i.RuleFor(j => j.ClassId).Must(Validators.IsValidId).When(j => j.ClassId != null);
            }).When(i => i.Models == null);

            RuleFor(i => i.Models).NotNull().Must(i => i.Count() <= Globals.MaxGetCount && i.All(Validators.IsValidId)).DependentRules(i =>
            {
                i.RuleFor(j => j.ClassId).Null();
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
