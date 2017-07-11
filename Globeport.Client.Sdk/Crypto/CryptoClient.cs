using System;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Globeport.Shared.Library.Data;

namespace Globeport.Client.Sdk.Crypto
{
    public abstract class CryptoClient
    {
        public CryptoClient()
        {
        }

        public abstract void GetRandomBytes(byte[] buffer);

        public abstract byte[] GetRandomBytes(int length);

        public abstract byte[] GenerateSecretKey();

        public abstract byte[] GenerateHashKey();

        public abstract byte[] GenerateShortHashKey();

        public abstract KeyPair GenerateKeyPair();

        public abstract byte[] Hash(byte[] message, byte[] key);

        public abstract string Hash(string message, string key);

        public abstract byte[] ShortHash(byte[] message, byte[] key);

        public abstract string ShortHash(string message, string key);

        public abstract byte[] Sign(byte[] privateKey, byte[] data);

        public abstract bool Verify(byte[] publicKey, byte[] data, byte[] signature);

        public abstract byte[] Encrypt(byte[] message, byte[] key);

        public abstract byte[] Decrypt(byte[] message, byte[] key);

        public abstract byte[] GetMD5Hash(byte[] input);

        public abstract Task<byte[]> Protect(byte[] data);

        public abstract Task<byte[]> Unprotect(byte[] data);

        public abstract KeyPair[] GeneratePreKeys(int count);

        public abstract KeyPair GenerateSignedPreKey(byte[] privateKey);

        public abstract byte[] GeneratePasswordSalt();

        public abstract byte[] GeneratePasswordKey(string password, byte[] salt);

        public abstract string GeneratePrime(int bits);

        public abstract byte[] GenerateClientEvidence(byte[] serverCredentials);

        public abstract byte[] VerifyServerEvidence(byte[] serverEvidence);

        public abstract byte[] GenerateVerifier(string username, byte[] password, byte[] salt);

        public abstract byte[] GenerateClientCredentials(string username, byte[] password, byte[] salt);

        public abstract ISignalCipher GetSignalCipher(ApiSession session);

        public string GenerateId()
        {
            return new Guid(GetRandomBytes(16)).ToString("N");
        }

        public virtual string GeneratePassword(int length, int complexity)
        {
            char[][] classes =
            {
                @"abcdefghijklmnopqrstuvwxyz".ToCharArray(),
                @"ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),
                @"0123456789".ToCharArray(),
                @" !""#$%&'()*+,./:;<>?@[\]^_{|}~".ToCharArray(),
            };

            complexity = Math.Max(1, Math.Min(classes.Length, complexity));
            if (length < complexity)
                throw new ArgumentOutOfRangeException("length");

            char[] allchars = classes.Take(complexity).SelectMany(c => c).ToArray();
            byte[] bytes = new byte[allchars.Length];
            GetRandomBytes(bytes);
            for (int i = 0; i < allchars.Length; i++)
            {
                char tmp = allchars[i];
                allchars[i] = allchars[bytes[i] % allchars.Length];
                allchars[bytes[i] % allchars.Length] = tmp;
            }

            Array.Resize(ref bytes, length);
            char[] result = new char[length];

            while (true)
            {
                GetRandomBytes(bytes);

                for (int i = 0; i < length; i++)
                    result[i] = allchars[bytes[i] % allchars.Length];

                if (Char.IsWhiteSpace(result[0]) || Char.IsWhiteSpace(result[(length - 1) % length]))
                    continue;

                string testResult = new string(result);

                if (0 != classes.Take(complexity).Count(c => testResult.IndexOfAny(c) < 0))
                    continue;

                return testResult;
            }
        }
    }
}
