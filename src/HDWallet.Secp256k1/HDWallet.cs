using HDWallet.Core;
using HDWallet.Secp256;

namespace HDWallet.Secp256k1
{
    public abstract class HDWallet<TWallet> : HDWalletSecpBase<TWallet>, IHDWallet<TWallet> where TWallet : Wallet, new()
    {
        public HDWallet(string mnemonic, string passphrase, CoinPath path) : base(mnemonic, passphrase, path)
        {

        }

        public HDWallet(string seed, CoinPath path) : base(seed, path)
        {

        }
    }
}