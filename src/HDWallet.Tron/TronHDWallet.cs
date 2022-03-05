using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Tron
{
    public class TronHDWallet : HDWallet<TronWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.Tron);

        public TronHDWallet(string mnemonic, string passphrase = "") : base(mnemonic, passphrase, _path) {}
    }
}