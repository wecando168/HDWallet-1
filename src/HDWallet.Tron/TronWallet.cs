using System.Text;
using HDWallet.Core;
using HDWallet.Secp256k1;
using NBitcoin;
using NBitcoin.DataEncoders;

namespace HDWallet.Tron
{
    public class TronWallet : Wallet, IWallet
    {
        public TronWallet(){}

        public TronWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }
    }

    public class TronPublicWallet : PublicWallet, IPublicWallet
    {
        public TronPublicWallet(){}

        public TronPublicWallet(string publicKey) : base(publicKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }
    }
}