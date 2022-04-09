using System;
using HDWallet.BIP32.Ed25519;
using HDWallet.Core;

namespace HDWallet.Ed25519
{
    public abstract class HdWalletEd25519<TWallet> : HdWalletBase, IHDWallet<TWallet> where TWallet : IWallet, new()
    {
        readonly ExtKey _masterKey;

        protected HdWalletEd25519(string seed, CoinPath path) : base(seed)
        {
            ExtKey extKey = new ExtKey(seed: this.BIP39Seed);
            _masterKey = extKey.DerivePath(path.ToString());
        }

        protected HdWalletEd25519(string mnemonic, string passphrase, CoinPath path) : base(mnemonic, passphrase)
        {
            ExtKey extKey = new ExtKey(seed: this.BIP39Seed);
            _masterKey = extKey.DerivePath(path.ToString());
        }

        /// <summary>
        /// Returns the [accountIndex]th account to access wallets at m/purpose'/coin_type'/{accountIndex}'/[0/1]
        /// </summary>
        /// <param name="accountIndex"></param>
        /// <returns></returns>
        IAccount<TWallet> IHDWallet<TWallet>.GetAccount(uint accountIndex)
        {
            var masterKey = _masterKey.Derive(accountIndex);
            return new Account<TWallet>(masterKey);
        }

        /// <summary>
        /// Generates an account from master for Mainnet.
        /// </summary>
        /// <param name="accountMasterKey">Used to generate wallet</param>
        /// <returns></returns>
        public static IAccount<TWallet> GetAccountFromMasterKey(string accountMasterKey)
        {
            IAccount<TWallet> accountHDWallet = new Account<TWallet>(accountMasterKey, NBitcoin.Network.Main);
            return accountHDWallet;
        }

        public TWallet GetWalletFromPath(string path)
        {
            ExtKey extKey = new ExtKey(seed: this.BIP39Seed);
            ExtKey derivedKey = extKey.DerivePath(path);

            return new TWallet() {
                PrivateKeyBytes = derivedKey.Key.PrivateKey
            };
        }

        TWallet IHDWallet<TWallet>.GetMasterWallet()
        {
            return new TWallet() {
                PrivateKeyBytes = _masterKey.Key.PrivateKey
            };
        }

        TWallet IHDWallet<TWallet>.GetAccountWallet(uint accountIndex)
        {
            var derivedKey = _masterKey.Derive(accountIndex);

            return new TWallet() {
                PrivateKeyBytes = derivedKey.Key.PrivateKey
            };
        }
    }
}