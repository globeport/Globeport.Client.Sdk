using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Org.BouncyCastle.Crypto.Prng;

using System.Security.Cryptography;

namespace Globeport.Client.Sdk.Crypto
{
    public class RandomGenerator : IRandomGenerator
    {
        RNGCryptoServiceProvider Provider = new RNGCryptoServiceProvider();

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
            var bytes = new byte[length];
            GetRandomBytes(bytes);
            return bytes;
        }

        public virtual void GetRandomBytes(byte[] bytes)
        {
            Provider.GetBytes(bytes);
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
