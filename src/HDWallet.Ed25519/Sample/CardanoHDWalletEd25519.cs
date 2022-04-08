using HDWallet.Core;

namespace HDWallet.Ed25519.Sample
{
    public class CardanoHDWalletEd25519 : HdWalletEd25519<CardanoSampleWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.CIP1852).CreateCoinPath(CoinType.Cardano);
        public CardanoHDWalletEd25519(string seed) : base(seed, _path) {}
        public CardanoHDWalletEd25519(string mnemonic, string passphrase) : base(mnemonic, passphrase, _path) {}
    }
}