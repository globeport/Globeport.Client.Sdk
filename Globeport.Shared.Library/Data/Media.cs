using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace Globeport.Shared.Library.Data
{
    public class Media
    {
        public string Id { get; set; }
        public string FileId { get; set; }
        public List<MediaFile> Files { get; set; }
        //to do: remove this when cassandra supports it
        [IgnoreDataMember]
        public IEnumerable<MediaFile> FilesEnumerable
        {
            get { return Files; }
            set { Files = value?.ToList(); }
        }
        public string Type { get; set; }
        public string KeyId { get; set; }
        public byte[] Key { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        [IgnoreDataMember]
        public bool IsValid { get; set; }

        public Media()
        {
        }

        public Media(string id, string fileId, List<MediaFile> files, string type, string keyId, byte[] key)
        {
            Id = id;
            FileId = fileId;
            Files = files;
            Type = type;
            KeyId = keyId;
            Key = key;
            Timestamp = DateTimeOffset.UtcNow;
        }
    }
}
