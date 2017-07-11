using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class GetCounterValidator: AbstractValidator<GetCounter>
    {
        public GetCounterValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Id).NotNull().Must(IsValidCounterId);
        }

        public bool IsValidCounterId(string id)
        {
            if (id.Length == 0) return false;
            var parts = id.Split(".");
            switch(parts[0])
            {
                case Counters.Groups:
                case Counters.Lists:
                case Counters.Notifications:
                case Counters.Users:
                    return true;
                case Counters.ModelEntities:
                    return parts.Length > 1 && Validators.IsValidId(parts[1]);
                default:
                    return false;
            }
        }
    }
}
