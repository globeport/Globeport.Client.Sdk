using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public static class Activities
    {
        public const string SignedUp = nameof(SignedUp);
        public const string LoggedIn = nameof(LoggedIn);
        public const string LoggedOut = nameof(LoggedOut);
        public const string ChangedPassword = nameof(ChangedPassword);
        public const string AddedContact = nameof(AddedContact);
        public const string AddedGroupMember = nameof(AddedGroupMember);
        public const string RemovedGroupMember = nameof(RemovedGroupMember);
        public const string SentForm = nameof(SentForm);
        public const string CompletedForm = nameof(CompletedForm);
        public const string CreatedList = nameof(CreatedList);
        public const string CreatedGroup = nameof(CreatedGroup);
        public const string JoinedGroup = nameof(JoinedGroup);
        public const string LeftGroup = nameof(LeftGroup);

        public static string[] PortalActivities = new[] { AddedGroupMember, RemovedGroupMember, CreatedList, CreatedGroup, JoinedGroup };
        public static string[] FormActivities = new[] { SentForm, CompletedForm };
    }
}
