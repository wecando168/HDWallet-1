using HDWallet.Core;

namespace HDWallet.Secp256r1.Sample
{
    public class Secp256r1Wallet : Wallet, IWallet
    {
        public Secp256r1Wallet(){}
        
        public Secp256r1Wallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new NullAddressGenerator();
        }
    }
}