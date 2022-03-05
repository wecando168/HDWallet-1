using HDWallet.Core;
using HDWallet.Secp256;

namespace HDWallet.Secp256.Tests
{
    public class SampleSecp256HDWallet : HDWalletSecpBase<SampleSecp256Wallet>, IHDWallet<SampleSecp256Wallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.Bitcoin);

        public SampleSecp256HDWallet(string mnemonic, string passphrase) : base(mnemonic, passphrase, _path) {}

        public SampleSecp256HDWallet(string seed) : base(seed, _path) {}
    }
}