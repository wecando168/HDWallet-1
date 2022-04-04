using System;
using HDWallet.Core;
using HDWallet.Ed25519;

namespace HDWallet.Algorand
{
    public class AlgorandHDWallet : HdWalletEd25519<AlgorandWallet>, IHDWallet<AlgorandWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.Algorand);

        public AlgorandHDWallet(string seed) : base(seed, _path) {}
        public AlgorandHDWallet(string mnemonic, string passphrase) : base(mnemonic, passphrase, _path) {}
    }
}