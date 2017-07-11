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
    public class GetContactsValidator: AbstractValidator<GetContacts>
    {
        DataCursorValidator DataCursorValidator { get; } = new DataCursorValidator(IsValidIndex);
        public GetContactsValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Cursor).NotNull().SetValidator(DataCursorValidator).DependentRules(i =>
            {
                i.RuleFor(j => j.PortalId).Must(Validators.IsValidId).When(j => j.PortalId != null);
                i.RuleFor(j => j.Mode).NotNull().Must(j=>typeof(ResultSetMode).GetConstants().ContainsKey(j)).When(j => j.PortalId != null);
            }).When(i => i.Contacts == null && i.Username == null);

            RuleFor(i => i.Contacts).NotNull().Must(i => i.Count() <= Globals.MaxGetCount && i.All(Validators.IsValidId)).DependentRules(i =>
            {
                i.RuleFor(j => j.PortalId).Null();
            }).When(i => i.Cursor == null && i.Username == null);

            RuleFor(i => i.Username).NotNull().Must(Validators.IsValidUsername).DependentRules(i =>
            {
                i.RuleFor(j => j.PortalId).Null();
            }).When(i => i.Contacts == null && i.Cursor == null);
        }

        public static bool IsValidIndex(string[] index)
        {
            if (index == null) return true;
            if (index.Length != 2) return false;
            if (!Validators.IsValidShortName(index[0])) return false;
            if (!Validators.IsValidId(index[1])) return false;
            return true;
        }
    }
}
