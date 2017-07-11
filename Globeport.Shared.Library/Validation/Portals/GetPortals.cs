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
    public class GetPortalsValidator: AbstractValidator<GetPortals>
    {
        public DataCursorValidator DataCursorValidator { get; } = new DataCursorValidator(IsValidIndex);

        public GetPortalsValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Cursor).NotNull().SetValidator(DataCursorValidator).DependentRules(i =>
            {
                i.RuleFor(j => j.ContactId).Null().When(j => j.EntityId != null);
                i.RuleFor(j => j.ContactId).Must(Validators.IsValidId).When(j => j.ContactId != null);
                i.RuleFor(j => j.EntityId).Null().When(j => j.ContactId != null);
                i.RuleFor(j => j.EntityId).Must(Validators.IsValidId).When(j => j.EntityId != null);

                i.RuleFor(j => j.Types).Must(j => j.Distinct().Count() == j.Count).When(j => j.Types != null);
                i.RuleFor(j => j.States).Must(AreValidStates).When(j => j.Types != null);

                i.RuleFor(j => j.Mode).NotNull().Must(j=>typeof(ResultSetMode).GetConstants().ContainsKey(j)).When(j => j.ContactId != null || j.EntityId != null);
            }).When(i => i.Portals == null);

            RuleFor(i => i.Portals).NotNull().Must(i => i.Count() <= Globals.MaxGetCount && i.All(Validators.IsValidId)).DependentRules(i =>
            {
                i.RuleFor(j => j.ContactId).Null();
                i.RuleFor(j => j.EntityId).Null();
                i.RuleFor(j => j.Types).Null();
                i.RuleFor(j => j.States).Null();
            }).When(i => i.Cursor == null);
        }

        public bool AreValidStates(List<string> states)
        {
            if (states.Distinct().Count() != states.Count()) return false;
            var validStates = typeof(PortalStates).GetConstants();
            return states.All(i => validStates.ContainsKey(i));
        }

        public static bool IsValidIndex(string[] index)
        {
            if (index == null) return true;
            if (index.Length != 3) return false;
            if (!typeof(PortalType).GetConstants().ContainsKey(index[0])) return false;
            if (!Validators.IsValidShortName(index[1])) return false;
            if (!Validators.IsValidId(index[2])) return false;
            return true;
        }
    }
}
