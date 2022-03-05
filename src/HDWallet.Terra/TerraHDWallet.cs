using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Terra
{
    public class TerraHDWallet : HDWallet<TerraWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.Terra);

        public TerraHDWallet(string mnemonic, string passphrase = "") : base(mnemonic, passphrase, _path) {}
    }
}