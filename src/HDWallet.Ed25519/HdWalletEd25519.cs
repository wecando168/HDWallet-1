using System;
using dotnetstandard_bip32;
using HDWallet.Core;

namespace HDWallet.Ed25519
{
    public abstract class HdWalletEd25519Base : HdWalletBase
    {
        protected HdWalletEd25519Base(string seed) : base(seed){}
        protected HdWalletEd25519Base(string mnemonic, string passphrase) : base(mnemonic, passphrase){}

        public TWallet GetWalletFromPath<TWallet>(string path) where TWallet : Wallet, new()
        {
            ExtKey extKey = new ExtKey(seed: this.BIP39Seed);
            ExtKey derivedKey = extKey.DerivePath(path);

            return new TWallet() {
                Path = path,
                PrivateKeyBytes = derivedKey.Key.PrivateKey
            };
        }

    }

    public abstract class HdWalletEd25519<TWallet> : HdWalletEd25519Base, IHDWallet<TWallet> where TWallet : Wallet, IWallet, new()
    {
        readonly TWallet _coinTypeWallet;
        readonly string _path;

        [Obsolete("Only for testing")]
        protected HdWalletEd25519(string seed) : base(seed) {}

        protected HdWalletEd25519(string seed, string path) : base(seed)
        {
            _path = path;

            ExtKey extKey = new ExtKey(seed: this.BIP39Seed);
            ExtKey derivedKey = extKey.DerivePath(path);

            _coinTypeWallet = new TWallet() {
                PrivateKeyBytes = derivedKey.Key.PrivateKey
            };
        }
        protected HdWalletEd25519(string seed, CoinPath path) : this(seed, path.ToString()) {}

        protected HdWalletEd25519(string mnemonic, string passphrase, string path) : base(mnemonic, passphrase)
        {
            _path = path;

            ExtKey extKey = new ExtKey(seed: this.BIP39Seed);
            ExtKey derivedKey = extKey.DerivePath(path);

            _coinTypeWallet = new TWallet() {
                PrivateKeyBytes = derivedKey.Key.PrivateKey
            };
        }
        protected HdWalletEd25519(string mnemonic, string passphrase, CoinPath path) : this(mnemonic, passphrase, path.ToString()) {}

        /// <summary>
        /// Returns wallet for m/[PURPOSE]'/[COINTYPE]' for constructor parameter 'path' (CoinPath)
        /// </summary>
        /// <returns>TWallet</returns>
        public TWallet GetCoinTypeWallet()
        {
            if(_coinTypeWallet == null) throw new NullReferenceException(nameof(_coinTypeWallet));
            return _coinTypeWallet;
        }

        public TWallet GetSubWallet(string subPath)
        {
            var keyPath = $"{_path}/{subPath}";
            return GetWalletFromPath<TWallet>(keyPath);
        }

        public TWallet GetMasterWallet()
        {
            return GetWalletFromPath<TWallet>(_path);
        }

        public TWallet GetAccountWallet(uint accountIndex)
        {
            var keyPath = $"{_path}/{accountIndex}'";
            return GetWalletFromPath<TWallet>(keyPath);
        }

        public IAccount<TWallet> GetAccount(uint accountIndex)
        {
            Func<string, TWallet> deriveFunction = GetWalletFromPath<TWallet>;
            return new Account<TWallet>(accountIndex, GetSubWallet);
        }
    }
}