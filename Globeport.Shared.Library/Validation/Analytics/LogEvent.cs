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
    public class LogEventValidator: AbstractValidator<LogEvent>
    {
        const int MaxDataLength = 1024;
        const int MaxMessageLength = 1024;

        public LogEventValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.EventType).NotNull().Must(i => (typeof(EventTypes).GetConstants().ContainsKey(i)));
            RuleFor(i => i.AppName).NotNull().Must(i => (typeof(AppNames).GetConstants().ContainsKey(i)));
            RuleFor(i => i.Message).NotNull().Length(1, MaxMessageLength);
            RuleFor(i => i.Data).Length(1, MaxDataLength).When(i => i.Data != null);
        }
    }
}
