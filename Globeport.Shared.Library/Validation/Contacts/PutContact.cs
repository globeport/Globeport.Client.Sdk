using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class PutContactValidator: AbstractValidator<PutContact>
    {
        public const int MaxPortalCount = 10;

        public PutContactValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.ContactId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.AvatarId).Must(Validators.IsValidId).When(i => i.AvatarId != null);
            RuleFor(i => i.AddPortals).Must(AreValidPortals).When(i => i.AddPortals != null);
            RuleFor(i => i.RemovePortals).Must(AreValidPortals).When(i => i.RemovePortals != null);
            RuleFor(i => i.AddPortals.Concat(i.RemovePortals)).Must(i => i.Count() == i.Distinct().Count()).When(i => i.AddPortals != null && i.RemovePortals != null);
        }

        bool AreValidPortals(List<string> portals)
        {
            if (portals.Count > MaxPortalCount) return false;
            if (portals.Distinct().Count() != portals.Count) return false;
            if (portals.Any(j => !Validators.IsValidId(j))) return false;
            return true;
        }
    }
}
