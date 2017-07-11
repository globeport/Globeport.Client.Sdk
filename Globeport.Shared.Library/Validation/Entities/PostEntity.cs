using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using MoreLinq;
using FluentValidation;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Validation
{
    public class PostEntityValidator : AbstractValidator<PostEntity>
    {
        public const int MaxPortalCount = 10;
        MediaUploadValidator MediaUploadValidator { get; } = new MediaUploadValidator();
        SignalMessageUploadValidator MessageUploadValidator { get; } = new SignalMessageUploadValidator();

        public PostEntityValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.ModelId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.Data).Must(Validators.IsValidEntityData);
            RuleFor(i => i.Signature).NotNull().Must(Validators.IsValidTimestampedSignature);
            RuleFor(i => i.KeyId).NotNull().Must(Validators.IsGuid);
            RuleFor(i => i.PacketId).Must(Validators.IsGuid).When(i => i.PacketId != null);
            RuleFor(i => i.MediaUploads).SetCollectionValidator(MediaUploadValidator).When(i => i.MediaUploads != null);
            RuleFor(i => i.Keys).Must(AreValidKeys).When(i => i.Keys != null);
            RuleFor(i => i.Portals).Must(AreValidPortals).When(i => i.Portals != null);
            RuleFor(i => i.Messages).NotNull().Must(AreValidMessages).SetCollectionValidator(MessageUploadValidator);
        }

        bool AreValidKeys(PostEntity request, Dictionary<string, byte[]> keys)
        {
            if (request.PacketId != null && !keys.ContainsKey(request.PacketId)) return false;
            if (request.Portals.Contains(SystemPortals.Public) && !keys.ContainsKey(request.KeyId)) return false;
            if (keys.Count > Globals.MaxMediaCount + 2) return false;
            if (keys.DistinctBy(i => i.Key).Count() != keys.Count) return false;
            if (keys.Any(i => !Validators.IsGuid(i.Key) || i.Value.Length != KeyUploadValidator.SecretKeyLength)) return false;
            return true;
        }

        bool AreValidMessages(PostEntity request, List<SignalMessageUpload> messages)
        {
            //worst case send to group portals with max contacts, each contact having max number open sessions
            //so messages for each group = 1 senderkey message + 1 senderkeydistribution message for each contact session
            if (messages.Count > request.Portals.Count * (1 + Globals.MaxContacts * (Globals.MaxSessions + 1))) return false;

            //check for invalid message types
            foreach (var message in messages)
            {
                if (message.MessageType == SignalMessageType.SenderKey && message.ContentType != SignalContentType.Entity) return false;
                if (message.MessageType == SignalMessageType.PreKey && message.ContentType != SignalContentType.SenderKey) return false;
                if (message.MessageType == SignalMessageType.System) return false;
            }

            return true;
        }

        bool AreValidPortals(List<string> portals)
        {
            if (portals.Count > MaxPortalCount) return false;
            if (portals.Distinct().Count() != portals.Count) return false;
            if (portals.Any(j => !Validators.IsValidId(j))) return false;
            return true;
        }
    }
}
