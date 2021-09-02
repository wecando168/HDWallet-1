using HDWallet.Core;
using HDWallet.Secp256r1;
using Neo;

namespace HDWallet.Neo
{
    public class NeoWallet : Wallet, IWallet
    {
        public NeoWallet() { }

        public NeoWallet(string privateKey) : base(privateKey) { }

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new NeoAddressGenerator();
        }

        public string GetAddress()
        {
            return new NeoAddressGenerator().GenerateAddress(base.PublicKey, ProtocolSettings.Default.AddressVersion);
        }
    }
}