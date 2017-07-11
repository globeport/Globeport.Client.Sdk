using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public static class Notifications
    {
        public const string AddedToContacts = nameof(AddedToContacts);
        public const string AddedToGroup = nameof(AddedToGroup);
        public const string RemovedFromGroup = nameof(RemovedFromGroup);
        public const string Announcement = nameof(Announcement);
        public const string SentForm = nameof(SentForm);
        public const string CompletedForm = nameof(CompletedForm);
        public const string NewSignalMessages = nameof(NewSignalMessages);
        public const string NewInteraction = nameof(NewInteraction);
    }
}
