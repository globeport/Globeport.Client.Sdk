namespace Globeport.Shared.Library.Data
{
    public class KeyPair
    {
        public byte[] PrivateKey { get; set; }
        public byte[] PublicKey { get; set; }
        public byte[] Signature { get; set; }

        public KeyPair(byte[] privateKey, byte[] publicKey, byte[] signature = null)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
            Signature = signature;
        }
    }
}
