using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Terra
{
    public class TerraWallet : Wallet, IWallet
    {
        /// <summary>
        /// Returns address for MainnetSingleSig by default
        /// Use "GetAddress" for TestnetSingleSig address
        /// </summary>
        // public new string Address => base.Address;
        public new string Address => base.Address;

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