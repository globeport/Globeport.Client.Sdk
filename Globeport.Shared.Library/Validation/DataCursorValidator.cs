using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class DataCursorValidator : AbstractValidator<DataCursor>
    {
        public DataCursorValidator(Func<string[], bool> positionValidator)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Direction).IsInEnum();
            RuleFor(i => i.Position).Must(positionValidator).When(i => i.Position != null);
            RuleFor(i => i.PageSize).NotNull().GreaterThanOrEqualTo(Globals.MinPageSize).LessThanOrEqualTo(Globals.MaxPageSize);
            RuleFor(i => i.Order).IsInEnum();
        }
    }
}
