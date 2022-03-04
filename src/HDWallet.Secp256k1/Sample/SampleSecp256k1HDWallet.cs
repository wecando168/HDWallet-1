using HDWallet.Core;

namespace HDWallet.Secp256k1.Sample
{
    public class SampleSecp256k1HDWallet : HDWallet<SampleSecp256k1Wallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.Bitcoin);

        public SampleSecp256k1HDWallet(string mnemonic, string passphrase) : base(mnemonic, passphrase, _path) {}
        public SampleSecp256k1HDWallet(string seed) : base(seed, _path) {}
    }
}