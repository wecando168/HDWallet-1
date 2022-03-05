using System;
using HDWallet.Core;
using HDWallet.Ed25519;

namespace HDWallet.Solana
{
    public class SolanaHdWallet : HdWalletEd25519<SolanaWallet>, IHDWallet<SolanaWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.Solana);

        public SolanaHdWallet(string seed) : base(seed, _path) { }
        public SolanaHdWallet(string mnemonic, string passphrase) : base(mnemonic, passphrase, _path) { }
    }
}