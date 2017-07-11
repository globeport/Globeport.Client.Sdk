using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using FluentValidation;
using MoreLinq;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.ClientModel;

namespace Globeport.Shared.Library.Validation
{
    public class PostPacketsValidator: AbstractValidator<PostPackets>
    {
        PacketUploadValidator PacketValidator { get; } = new PacketUploadValidator();

        public PostPacketsValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Packets).NotNull().Must(AreValidPackets).SetCollectionValidator(PacketValidator);
        }

        bool AreValidPackets(List<PacketUpload> packets)
        {
            if (packets.Count == 0) return false;
            if (packets.Count > Globals.MessageBatchSize) return false;
            if (packets.DistinctBy(i=>Packet.GetId(i.ContainerId, i.SenderId, i.PacketId)).Count() != packets.Count) return false;
            return true;
        }
    }
}
