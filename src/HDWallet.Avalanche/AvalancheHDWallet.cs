using System;
using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Avalanche
{
    public class AvalancheHDWallet : HDWallet<AvalancheWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.Avalanche);

        public AvalancheHDWallet(string mnemonic, string passphrase = "") : base(mnemonic, passphrase, _path) {}
    }
}
