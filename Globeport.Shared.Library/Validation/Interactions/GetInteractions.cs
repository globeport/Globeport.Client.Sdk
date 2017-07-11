using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class GetInteractionsValidator: AbstractValidator<GetInteractions>
    {
        public DataCursorValidator DataCursorValidator { get; } = new DataCursorValidator(IsValidIndex);

        public GetInteractionsValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Cursor).NotNull().SetValidator(DataCursorValidator).DependentRules(i =>
            {
                i.RuleFor(j => j.EntityId).NotNull().Must(Validators.IsValidId);
                i.RuleFor(j => j.AccountId).Must(Validators.IsValidId).When(j => j.AccountId != null);
                i.RuleFor(j => j.Type).Must(Validators.IsValidShortName).When(j => j.Type != null);
                i.RuleFor(j => j.Tag).Must(Validators.IsValidShortName).When(j => j.Tag != null);
            }).When(i => i.Interactions == null);

            RuleFor(i => i.Interactions).NotNull().Must(i => i.Count() <= Globals.MaxGetCount && i.All(Validators.IsValidId)).DependentRules(i =>
            {
                i.RuleFor(j => j.Interactions.Count == 1);
                i.RuleFor(j => j.EntityId).Null();
                i.RuleFor(j => j.AccountId).Null();
                i.RuleFor(j => j.Type).Null();
                i.RuleFor(j => j.Tag).Null();
            }).When(i => i.Cursor == null);
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
