using HDWallet.Core;

namespace HDWallet.Secp256k1.Sample
{
    public class SampleSecp256k1PublicWallet : PublicWallet, IPublicWallet
    {
        public SampleSecp256k1PublicWallet(){}
        
        public SampleSecp256k1PublicWallet(byte[] publicKeyBytes) : base(publicKeyBytes) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new NullAddressGenerator();
        }
    }
}