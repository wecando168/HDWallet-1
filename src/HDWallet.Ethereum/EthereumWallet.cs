using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Ethereum
{
    public class EthereumWallet : Wallet, IWallet
    {
        public EthereumWallet() {}

        public EthereumWallet(string privateKey) : base(privateKey) {}

        public new EthereumSignature Sign(byte[] message)
        {
            return new EthereumSignature(base.Sign(message));
        }

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }
    }
}