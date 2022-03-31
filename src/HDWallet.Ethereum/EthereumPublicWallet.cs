using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Ethereum
{
    public class EthereumPublicWallet : PublicWallet, IPublicWallet
    {
        public EthereumPublicWallet() {}
        public EthereumPublicWallet(string publicKey) : base(publicKey: publicKey) {}
        public EthereumPublicWallet(byte[] publicKeyBytes) : base(publicKeyBytes: publicKeyBytes) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }
    }
}