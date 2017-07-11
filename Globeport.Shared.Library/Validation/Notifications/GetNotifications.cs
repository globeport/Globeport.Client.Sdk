using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class GetNotificationsValidator: AbstractValidator<GetNotifications>
    {
        public DataCursorValidator DataCursorValidator { get; } = new DataCursorValidator(IsValidIndex);

        public GetNotificationsValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Cursor).NotNull().SetValidator(DataCursorValidator);
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
