using HDWallet.Core;
using Neo.Cryptography.ECC;
using System;

namespace HDWallet.Secp256r1
{
    public abstract class Wallet : IWallet
    {
        public byte[] PrivateKeyBytes { get; set; }
        public ECPoint PublicKey;
        public uint Index { get; set; }

        public string Address => AddressGenerator.GenerateAddress(PublicKey.EncodePoint(false));

        internal IAddressGenerator AddressGenerator { get; private set; }

        public Wallet()
        {
            AddressGenerator = GetAddressGenerator();
        }

        protected abstract IAddressGenerator GetAddressGenerator();
 
        public Wallet(string privateKey) : this()
        {
            var privateKeyToByte = privateKey.HexToBytes();
            PrivateKeyBytes = privateKeyToByte;

            if (privateKeyToByte.Length != 32 && privateKeyToByte.Length != 96 && privateKeyToByte.Length != 104)
                throw new NotSupportedException();

            privateKeyToByte = privateKeyToByte[^32..];
            
            if (privateKeyToByte.Length == 32)
            {
                PublicKey = ECCurve.Secp256r1.G * privateKeyToByte;
            }
            else
            {
                PublicKey = ECPoint.FromBytes(privateKeyToByte, ECCurve.Secp256r1);
            }
        }

        public Signature Sign(byte[] message)
        {
            throw new NotImplementedException();
        }
    }
}