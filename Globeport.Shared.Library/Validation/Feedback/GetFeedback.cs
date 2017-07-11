using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Validation
{
    public class GetFeedbackValidator: AbstractValidator<GetFeedback>
    {
        public DataCursorValidator DataCursorValidator { get; } = new DataCursorValidator(IsValidIndex);

        public GetFeedbackValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.AppName).NotNull().Must(i => typeof(AppNames).GetConstants().ContainsKey(i));
            RuleFor(i => i.Type).NotNull().Must(i => typeof(FeedbackType).GetConstants().ContainsKey(i));
            RuleFor(i => i.Cursor).NotNull().SetValidator(DataCursorValidator);
        }

        public static bool IsValidIndex(string[] index)
        {
            if (index == null) return true;
            if (index.Length != 2) return false;
            long votes;
            if (!long.TryParse(index[0], out votes)) return false;
            if (!Validators.IsValidId(index[1])) return false;
            return true;
        }
    }
}
