using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Validation
{
    public class SignalMessageUploadValidator: AbstractValidator<SignalMessageUpload>
    {
        public const int MaxAddressLength = 38;

        public SignalMessageUploadValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Address).NotNull().Must(IsValidAddress);

            RuleFor(i => i.MessageType).NotNull().Must(i => typeof(SignalMessageType).GetConstants().ContainsKey(i) && i != SignalMessageType.System);

            RuleFor(i => i.ContentType).NotNull().Must(i => typeof(SignalContentType).GetConstants().ContainsKey(i) && i != SignalContentType.Command);
        }

        bool IsValidAddress(SignalMessageUpload request, string address)
        {
            if (address.Length > MaxAddressLength) return false;
            switch (request.MessageType)
            {
                case SignalMessageType.PreKey:
                    var parts = address.Split('.');
                    switch (request.ContentType)
                    {
                        case SignalContentType.Entity:
                        case SignalContentType.Interaction:
                            if (parts.Length != 2) return false;
                            if (!Validators.IsValidId(parts[0])) return false;
                            if (!parts[1].IsInteger()) return false;
                            break;
                        case SignalContentType.SenderKey:
                            if (parts.Length != 3) return false;
                            if (!Validators.IsValidId(parts[0])) return false;
                            if (!parts[1].IsInteger()) return false;
                            if (!Validators.IsValidId(parts[2])) return false;
                            break;
                    }

                    break;
                case SignalMessageType.SenderKey:
                    if (!Validators.IsValidId(address)) return false;
                    break;
            }
            return true;
        }
    }
}
