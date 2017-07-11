using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class MediaUpload
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public List<MediaFileUpload> Files { get; set; }
        public string KeyId { get; set; }
        public byte[] Key { get; set; }
        [IgnoreDataMember]
        public string FileId { get; set; }
        [IgnoreDataMember]
        public double AspectRatio { get; set; }
        [IgnoreDataMember]
        public bool IsValid { get; set; }

        public MediaUpload()
        {
        }

        public MediaUpload(string id, string type, IEnumerable<MediaFileUpload> files)
        {
            Id = id;
            Type = type;
            Files = files.ToList();
        }

        public Media GetMedia()
        {
            var mediaFiles = Files.Select(i => new MediaFile(i.Size, i.Signature)).ToList();

            return new Media(Id, FileId, mediaFiles, Type, KeyId, null);
        }
    }
}
