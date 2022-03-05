using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Stacks
{
    public class StacksHDWallet : HDWallet<StacksWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.Blockstack);

        public StacksHDWallet(string mnemonic, string passphrase = "") : base(mnemonic, passphrase, _path) {}
    }
}