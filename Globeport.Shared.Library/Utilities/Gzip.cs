using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Utilities
{
    public static class Gzip
    {
        public static async Task<byte[]> Compress(byte[] bytes)
        {
            if (bytes == null) return null;
            using (var memory = new MemoryStream())
            {
                using (var gzip = new GZipStream(memory, CompressionMode.Compress, true))
                {
                    await gzip.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
                }
                return memory.ToArray();
            }
        }

        public static async Task<byte[]> Decompress(byte[] bytes)
        {
            if (bytes == null) return null;
            using (var stream = new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = await stream.ReadAsync(buffer, 0, size).ConfigureAwait(false);
                        if (count > 0)
                        {
                            await memory.WriteAsync(buffer, 0, count).ConfigureAwait(false);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }
    }
}
