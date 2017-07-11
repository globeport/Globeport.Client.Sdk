using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class PacketUploadValidator: AbstractValidator<PacketUpload>
    {
        public const int MaxDataLength = 32768;
        public PacketUploadValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.PacketId).NotNull().Must(Validators.IsGuid);
            RuleFor(i => i.ContainerId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.SenderId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.Data).NotNull().Must(IsValidData);
        }

        public static bool IsValidData(byte[] data)
        {
            return data.Length < MaxDataLength;
        }

        public static bool IsValidPacketId(string id)
        {
            var parts = id.Split('.');
            if (parts.Length != 3) return false;
            if (!Validators.IsValidId(parts[0])) return false;
            if (!Validators.IsValidId(parts[1])) return false;
            if (!Validators.IsGuid(parts[2])) return false;
            return true;
        }
    }
}

