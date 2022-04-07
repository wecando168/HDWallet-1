using HDWallet.Core;

namespace HDWallet.Ed25519.Sample
{
    public class TestHDWalletEd25519 : HdWalletEd25519<SampleWallet>
    {
        private static readonly CoinPath _path = Purpose.Create(PurposeNumber.PURPOSE0).CreateCoinPath(CoinType.Polkadot);

        public TestHDWalletEd25519(string seed) : base(seed, _path) {}
        public TestHDWalletEd25519(string mnemonic, string passphrase) : base(mnemonic, passphrase, _path) {}
    }
}