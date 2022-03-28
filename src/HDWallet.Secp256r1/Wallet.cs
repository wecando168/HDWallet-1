using HDWallet.Core;
using Neo.Cryptography.ECC;
using System;

namespace HDWallet.Secp256r1
{
    public abstract class Wallet : IWallet
    {
        public ECPoint PublicKey;
        public uint Index { get; set; }

        public string Address => AddressGenerator.GenerateAddress(PublicKey.ToString().FromHexToByteArray());

        internal IAddressGenerator AddressGenerator { get; private set; }

        public Wallet()
        {
            AddressGenerator = GetAddressGenerator();
        }

        protected abstract IAddressGenerator GetAddressGenerator();

        private byte[] privateKeyBytes;

        public byte[] PrivateKeyBytes
        {
            get
            {
                return privateKeyBytes;
            }
            set
            {
                PublicKey = PublicKeyParse(value);
                privateKeyBytes = value;
            }
        }

        public byte[] PublicKeyBytes => PublicKey.ToString().FromHexToByteArray();

        public Wallet(string privateKey) : this()
        {
            var privateKeyToByte = privateKey.HexToBytes();
            PrivateKeyBytes = privateKeyToByte;
            PublicKey = PublicKeyParse(privateKeyToByte);
        }

        private ECPoint PublicKeyParse(byte[] privateKeyToByte)
        {
            if (privateKeyToByte.Length != 32 && privateKeyToByte.Length != 96 && privateKeyToByte.Length != 104)
                throw new NotSupportedException();

            privateKeyToByte = privateKeyToByte[^32..];

            if (privateKeyToByte.Length == 32)
            {
                return ECCurve.Secp256r1.G * privateKeyToByte;
            }
            else
            {
                return ECPoint.FromBytes(privateKeyToByte, ECCurve.Secp256r1);
            }
        }

        public Signature Sign(byte[] message)
        {
            throw new NotImplementedException();
        }

        public bool Verify(byte[] message, Signature sig)
        {
            throw new NotImplementedException();
        }
    }
}