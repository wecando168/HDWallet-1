using HDWallet.Core;

namespace HDWallet.Secp256k1.Sample
{
    public class SampleSecp256k1Wallet : Wallet, IWallet
    {
        public SampleSecp256k1Wallet(){}
        
        public SampleSecp256k1Wallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new NullAddressGenerator();
        }
    }
}