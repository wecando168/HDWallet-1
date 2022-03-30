using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Terra
{
    public class TerraWallet : Wallet, IWallet
    {
        public TerraWallet(){}

        public TerraWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }

        public string GetAddress()
        {
            return new AddressGenerator().GenerateAddress(base.PublicKey.ToBytes());
        }
    }
}