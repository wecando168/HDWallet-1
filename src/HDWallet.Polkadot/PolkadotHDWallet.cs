using System;
using HDWallet.Core;
using HDWallet.Ed25519;

namespace HDWallet.Polkadot
{
    public class KusamaHDWallet : HdWalletEd25519<PolkadotWallet>, IHDWallet<PolkadotWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.Kusama);

        public KusamaHDWallet(string seed) : base(seed, _path) {}
        public KusamaHDWallet(string mnemonic, string passphrase) : base(mnemonic, passphrase, _path) {}
    }

    public class PolkadotHDWallet : HdWalletEd25519<PolkadotWallet>, IHDWallet<PolkadotWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.Polkadot);

        public PolkadotHDWallet(string seed) : base(seed, _path) {}
        public PolkadotHDWallet(string mnemonic, string passphrase) : base(mnemonic, passphrase, _path) {}
    }
}