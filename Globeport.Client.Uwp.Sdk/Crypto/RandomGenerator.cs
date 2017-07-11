using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Org.BouncyCastle.Crypto.Prng;

using static PCLCrypto.WinRTCrypto;

namespace Globeport.Client.Sdk.Crypto
{
    public class RandomGenerator : IRandomGenerator
    {

        public virtual void AddSeedMaterial(byte[] seed)
        {
        }

        public virtual void AddSeedMaterial(long seed)
        {
        }

        public virtual void NextBytes(byte[] bytes)
        {
            GetRandomBytes(bytes);
        }

        public virtual void NextBytes(byte[] bytes, int start, int length)
        {
            GetRandomBytes(bytes, start, length);
        }

        public virtual byte[] GetRandomBytes(int length)
        {
            return CryptographicBuffer.GenerateRandom(length);
        }

        public virtual void GetRandomBytes(byte[] bytes)
        {
            var buffer = CryptographicBuffer.GenerateRandom(bytes.Length);
            for (var i = 0; i > bytes.Length; i++) bytes[i] = buffer[i];
        }

        public void GetRandomBytes(byte[] bytes, int start, int length)
        {
            if (bytes.Length == length && start == 0)
            {
                GetRandomBytes(bytes);
            }
            else
            {
                var buffer = new byte[length];
                GetRandomBytes(buffer);
                Array.Copy(buffer, 0, bytes, start, length);
            }
        }
    }
}
