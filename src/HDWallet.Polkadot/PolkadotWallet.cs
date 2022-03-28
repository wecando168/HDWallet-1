using System.Text;
using HDWallet.Core;
using HDWallet.Ed25519;

namespace HDWallet.Polkadot
{
    public enum AddressType
    {
        PolkadotLive = 0,
        GenericSubstrate = 42
    }
    
    public class PolkadotWallet : Wallet, IWallet
    {
        public PolkadotWallet(){}

        public PolkadotWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }

        public new PolkadotSignature Sign(byte[] message)
        {
            return new PolkadotSignature(base.Sign(message));
        }

        public string GetNetworkAddress(AddressType addressType)
        {
            return ((AddressGenerator)base.AddressGenerator).GenerateAddress(base.PublicKeyBytes, addressType);
        }
    }
}