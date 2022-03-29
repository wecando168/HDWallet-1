using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Ethereum
{
    public class EthereumPublicWallet : PublicWallet, IPublicWallet
    {
        public EthereumPublicWallet() {}
        public EthereumPublicWallet(string privateKey) : base(privateKey) {}
        public EthereumPublicWallet(byte[] publicKeyBytes) : base(publicKeyBytes) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }
    }
}