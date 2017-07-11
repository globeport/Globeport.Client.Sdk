using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

using libsignal.ecc;
using libsignal.util;

using PCLCrypto;

using Org.BouncyCastle.Security;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Agreement.Srp;
using Org.BouncyCastle.Crypto.Digests;

using Globeport.Client.Sdk.Crypto;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;
using Globeport.Client.Sdk;

namespace Globeport.Client.Net46.Sdk.Crypto
{
    public class Net46CryptoClient : CryptoClient
    {
        public const int TIME_COST = 3;
        public const int MEMORY_COST = 16777216;
        public static readonly BigInteger N2048Bit = new BigInteger(Convert.FromBase64String(Globals.SrpPrime));
        public static readonly BigInteger g2048Bit = BigInteger.ValueOf(Globals.SrpGenerator);

        Srp6Client SrpClient = new Srp6Client();
        Srp6VerifierGenerator VertifierGenerator = new Srp6VerifierGenerator();
        RandomGenerator RandomGenerator = new RandomGenerator();
        public Net46CryptoClient()
        {
            SrpClient.Init(N2048Bit, g2048Bit, new Sha256Digest(), new SecureRandom(RandomGenerator));
            VertifierGenerator.Init(N2048Bit, g2048Bit, new Sha256Digest());
        }

        public override byte[] GenerateHashKey()
        {
            return Sodium.GenericHash.GenerateKey();
        }

        public override string Hash(string message, string key)
        {
            return Sodium.GenericHash.Hash(message, key.FromBase64(), 64).ToBase64();
        }

        public override byte[] Hash(byte[] message, byte[] key)
        {
            return Sodium.GenericHash.Hash(message, key, 64);
        }

        public override byte[] GenerateShortHashKey()
        {
            return Sodium.ShortHash.GenerateKey();
        }

        public override string ShortHash(string message, string key)
        {
            return Sodium.ShortHash.Hash(message, key.FromBase64()).ToBase64();
        }

        public override byte[] ShortHash(byte[] message, byte[] key)
        {
            return Sodium.ShortHash.Hash(message, key);
        }

        public override byte[] GenerateSecretKey()
        {
            return Sodium.SecretBox.GenerateKey();
        }

        public override byte[] Encrypt(byte[] message, byte[] key)
        {
            var nonce = Sodium.SecretBox.GenerateNonce();
            var output = Sodium.SecretBox.Create(message, nonce, key);
            return ByteUtil.combine(nonce, output);
        }

        public override byte[] Decrypt(byte[] message, byte[] key)
        {
            var split = ByteUtil.split(message, 24, message.Length - 24);
            return Sodium.SecretBox.Open(split[1], split[0], key);
        }

        public override KeyPair GenerateKeyPair()
        {
            var keyPair = Curve.generateKeyPair();
            return new KeyPair(keyPair.getPrivateKey().serialize(), keyPair.getPublicKey().serialize());
        }

        public override byte[] Sign(byte[] privateKey, byte[] message)
        {
            var timestamp = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ssZ").ToBytes();
            message = ByteUtil.combine(message, timestamp);
            var signature = Curve.calculateSignature(Curve.decodePrivatePoint(privateKey), message);
            return signature.Combine(timestamp);
        }

        public override bool Verify(byte[] publicKey, byte[] message, byte[] signature)
        {
            var parts = ByteUtil.split(signature, 64, 64);
            signature = parts[0];
            var timestamp = parts[1];
            message = ByteUtil.combine(message, timestamp);
            return Curve.verifySignature(Curve.decodePoint(publicKey, 0), message, signature);
        }

        public override KeyPair[] GeneratePreKeys(int count)
        {
            var preKeys = Enumerable.Range(1, 100).Select(i => Curve.generateKeyPair());
            return preKeys.Select(i => new KeyPair(i.getPrivateKey().serialize(), i.getPublicKey().serialize())).ToArray();
        }

        public override KeyPair GenerateSignedPreKey(byte[] privateKey)
        {
            var preKey = Curve.generateKeyPair();
            byte[] signature = Curve.calculateSignature(Curve.decodePrivatePoint(privateKey), preKey.getPublicKey().serialize());
            return new KeyPair(preKey.getPrivateKey().serialize(), preKey.getPublicKey().serialize(), signature);
        }

        public override byte[] GeneratePasswordSalt()
        {
            return Sodium.SodiumCore.GetRandomBytes(16);
        }

        public override byte[] GeneratePasswordKey(string password, byte[] salt)
        {
            return Sodium.PasswordHash.ArgonHashBinary(password.ToBytes(), salt, TIME_COST, MEMORY_COST, 32);
        }

        public override byte[] GetMD5Hash(byte[] input)
        {
            return WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Md5).HashData(input);
        }

        public override Task<byte[]> Protect(byte[] data)
        {
            return Task.FromResult(data);
        }

        public override Task<byte[]> Unprotect(byte[] data)
        {
            return Task.FromResult(data);
        }

        public override string GeneratePrime(int bits)
        {
            var number = new BigInteger(bits, new SecureRandom(RandomGenerator));

            while (!number.IsProbablePrime(1))
            {
                number = number.Subtract(new BigInteger("1"));
            }

            return Convert.ToBase64String(number.ToByteArray());
        }

        public override byte[] GenerateClientEvidence(byte[] serverCredentials)
        {
            SrpClient.CalculateSecret(new BigInteger(serverCredentials));
            return SrpClient.CalculateClientEvidenceMessage().ToByteArray();
        }

        public override byte[] VerifyServerEvidence(byte[] serverEvidence)
        {
            var result = SrpClient.VerifyServerEvidenceMessage(new BigInteger(serverEvidence));
            return result ? SrpClient.CalculateSessionKey().ToByteArray() : null;
        }

        public override byte[] GenerateVerifier(string username, byte[] password, byte[] salt)
        {
            return VertifierGenerator.GenerateVerifier(salt, username.ToUpperInvariant().ToBytes(), password).ToByteArray();
        }

        public override byte[] GenerateClientCredentials(string username, byte[] password, byte[] salt)
        {
            return SrpClient.GenerateClientCredentials(salt, username.ToUpperInvariant().ToBytes(), password).ToByteArray();
        }

        public override void GetRandomBytes(byte[] buffer)
        {
            RandomGenerator.GetRandomBytes(buffer);
        }

        public override byte[] GetRandomBytes(int length)
        {
            return RandomGenerator.GetRandomBytes(length);
        }

        public override ISignalCipher GetSignalCipher(ApiSession session)
        {
            return new SignalCipher(session);
        }
    }
}
