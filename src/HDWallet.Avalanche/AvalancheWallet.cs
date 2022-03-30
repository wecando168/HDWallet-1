using System;
using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Avalanche
{
    public class AvalancheWallet : Wallet, IWallet
    {
        public AvalancheWallet(){}

        public AvalancheWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }

        public string GetAddress(Networks network = Networks.Mainnet, Chain chain = Chain.X)
        {
            return new AddressGenerator().GenerateAddress(base.PublicKey.ToBytes(), network, chain);
        }
    }
}