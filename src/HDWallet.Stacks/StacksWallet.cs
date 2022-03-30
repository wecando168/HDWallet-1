using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Stacks
{
    public class StacksWallet : Wallet, IWallet
    {
        public StacksWallet(){}

        public StacksWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }

        public string GetAddress(NetworkVersion networkVersion = NetworkVersion.Mainnet)
        {
            return new AddressGenerator().GenerateAddress(base.PublicKey.ToBytes(), networkVersion);
        }
    }
}