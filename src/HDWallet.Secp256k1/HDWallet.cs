using HDWallet.Core;
using HDWallet.Secp256;

namespace HDWallet.Secp256k1
{
    public abstract class HDWallet<TWallet> : HDWalletSecpBase<TWallet>, IHDWallet<TWallet> where TWallet : Wallet, new()
    {
        public HDWallet(string words, string seedPassword, CoinPath path) : base(words, seedPassword, path)
        {

        }

        public HDWallet(string seed, CoinPath path) : base(seed, path)
        {

        }
    }
}