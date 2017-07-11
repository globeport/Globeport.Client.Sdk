using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class PostContactValidator: AbstractValidator<PostContact>
    {
        public const int MaxPortalCount = 10;
        
        public PostContactValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.ContactId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.AvatarId).Must(Validators.IsValidId).When(i => i.AvatarId != null);
            RuleFor(i => i.Portals).Must(AreValidPortals).When(i => i.Portals != null);
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
