using HDWallet.Core;
using NBitcoin;
using NBitcoin.DataEncoders;
using NBitcoin.Secp256k1;
using System;

namespace HDWallet.Secp256k1
{
    public abstract class Wallet : IWallet
    {
        public Key PrivateKey;
        public PubKey PublicKey => PrivateKey.PubKey;

        public uint Index { get; set; }
        public string Address => AddressGenerator.GenerateAddress(PublicKey.ToBytes());

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


        internal IAddressGenerator AddressGenerator { get; private set; }

        public Wallet()
        {
            AddressGenerator = GetAddressGenerator();
        }

        protected abstract IAddressGenerator GetAddressGenerator();

        public Wallet(string privateKey) : this()
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
            if (message.Length != 32) throw new ArgumentException(paramName: nameof(message), message: "Message should be 32 bytes");

            NBitcoin.Secp256k1.ECPrivKey privKey = Context.Instance.CreateECPrivKey(new Scalar(PrivateKey.ToBytes()));
            if (!privKey.TrySignRecoverable(message, out SecpRecoverableECDSASignature sigRec))
            {
                throw new InvalidOperationException();
            }

            var (r, s, recId) = sigRec;

            return new Signature()
            {
                R = r.ToBytes(),
                S = s.ToBytes(),
                RecId = recId
            };
        }
    }
}