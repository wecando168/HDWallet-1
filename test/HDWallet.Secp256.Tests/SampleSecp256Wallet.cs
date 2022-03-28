using HDWallet.Core;
using NBitcoin;
using NBitcoin.DataEncoders;
using System;

namespace HDWallet.Secp256.Tests
{
    public class SampleSecp256Wallet : IWallet
    {
        public Key PrivateKey;
        public PubKey PublicKey => PrivateKey.PubKey;

        public uint Index { get; set; }
        public string Address { get; }

        private byte[] privateKeyBytes;

        public byte[] PrivateKeyBytes
        {
            get
            {
                return privateKeyBytes;
            }
            set
            {
                var hexEncodeData = Encoders.Hex.EncodeData(value);
                PrivateKey = PrivateKeyParse(hexEncodeData);
                privateKeyBytes = value;
            }
        }

        public byte[] PublicKeyBytes => this.PublicKey.ToBytes();

        internal IAddressGenerator AddressGenerator { get; private set; }

        public SampleSecp256Wallet()
        {
            AddressGenerator = GetAddressGenerator();
        }

        protected IAddressGenerator GetAddressGenerator()
        {
            return new NullAddressGenerator();
        }

        public SampleSecp256Wallet(string privateKey) : this() 
        {
            PrivateKey = PrivateKeyParse(privateKey);
            PrivateKeyBytes = PrivateKey.ToBytes();
        }

        private Key PrivateKeyParse(string privateKey)
        {
            byte[] privKeyPrefix = new byte[] { (128) };
            byte[] prefixedPrivKey = Helper.Concat(privKeyPrefix, Encoders.Hex.DecodeData(privateKey));

            byte[] privKeySuffix = new byte[] { (1) };
            byte[] suffixedPrivKey = Helper.Concat(prefixedPrivKey, privKeySuffix);

            Base58CheckEncoder base58Check = new Base58CheckEncoder();
            string privKeyEncoded = base58Check.EncodeData(suffixedPrivKey);
            return Key.Parse(privKeyEncoded, Network.Main);
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