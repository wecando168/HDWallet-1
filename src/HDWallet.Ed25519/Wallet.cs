using System;
using HDWallet.Core;
using NBitcoin.DataEncoders;
using HDWallet.BIP32.Ed25519;
using System.Linq;

namespace HDWallet.Ed25519
{
    public abstract class Wallet : IWallet
    {
        public byte[] PublicKeyBytes { get; private set; }

        byte[] privateKey;
        public byte[] PrivateKeyBytes
        {
            get {
                return privateKey;
            } set{
                if(value.Length != 32)
                {
                    throw new InvalidOperationException();
                }
                privateKey = value;

                PublicKeyBytes = Chaos.NaCl.Ed25519.PublicKeyFromSeed(privateKey);
            }
        }
        public byte[] ExpandedPrivateKey {
            get {
                return GetExpandedPrivateKey(PrivateKeyBytes);
            }
        }

        public byte[] GetExpandedPrivateKey(byte[] privateKey)
        {
            Chaos.NaCl.Ed25519.KeyPairFromSeed(out _, out var expandedPrivateKey, privateKey);

            var zero = new byte[] { 0 };

            var buffer = new BigEndianBuffer();

            buffer.Write(expandedPrivateKey);
            return buffer.ToArray();
        }

        public string Address => AddressGenerator.GenerateAddress(PublicKeyBytes);

        public IAddressGenerator AddressGenerator {get; private set; }
      

        public Wallet(){
            AddressGenerator = GetAddressGenerator();
        }

        public Wallet(byte[] privateKey) : this()
        {
            PrivateKeyBytes = privateKey;
        }

        public Wallet(string privateKeyHex) : this(Encoders.Hex.DecodeData( 
            privateKeyHex.StartsWith("0x") ? 
            privateKeyHex.Substring(2) : 
            privateKeyHex
        ))
        {
        }

        protected abstract IAddressGenerator GetAddressGenerator();

        // TODO: Test it, not tested
        public Signature Sign(byte[] message)
        {
            // if (message.Length != 32) throw new ArgumentException(paramName: nameof(message), message: "Message should be 32 bytes");

            byte[] signature = Chaos.NaCl.Ed25519.Sign(message, Chaos.NaCl.Ed25519.ExpandedPrivateKeyFromSeed(this.PrivateKeyBytes));
            
            var rsigPad = new byte[32];
            Array.Copy(signature.ToArray(), 0, rsigPad, rsigPad.Length - 32, 32);

            var ssigPad = new byte[32];
            Array.Copy(signature.ToArray(), 32, ssigPad, ssigPad.Length - 32, 32);

            return new Signature()
            {
                R = rsigPad,
                S = ssigPad
            };
        }

        // TODO: Implement this
        public bool Verify(byte[] message, Signature sig)
        {
            throw new NotImplementedException();
        }
    }
}