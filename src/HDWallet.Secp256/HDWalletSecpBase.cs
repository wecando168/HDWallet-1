using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Secp256
{
    public abstract class HDWalletSecpBase<TWallet> : HdWalletBase, IHDWallet<TWallet> where TWallet : IWallet, new()
    {
        ExtKey _masterKey;

        public HDWalletSecpBase(string words, string seedPassword, CoinPath path) : base(words, seedPassword)
        {
            var masterKeyPath = new KeyPath(path.ToString());
            _masterKey = new ExtKey(base.BIP39Seed).Derive(masterKeyPath);
        }

        public HDWalletSecpBase(string seed, CoinPath path) : base(seed)
        {
            var masterKeyPath = new KeyPath(path.ToString());
            _masterKey = new ExtKey(base.BIP39Seed).Derive(masterKeyPath);
        }

        TWallet IHDWallet<TWallet>.GetMasterWallet()
        {
            var masterKey = new ExtKey(base.BIP39Seed);

            var privateKey = masterKey.PrivateKey.ToBytes();
            return new TWallet()
            {
                PrivateKeyBytes = privateKey
            };
        }

        TWallet IHDWallet<TWallet>.GetAccountWallet(uint accountIndex)
        {
            var externalKeyPath = new KeyPath($"{accountIndex}'");
            var externalMasterKey = _masterKey.Derive(externalKeyPath);

            return new TWallet()
            {
                PrivateKeyBytes = _masterKey.PrivateKey.ToBytes(),
                Index = accountIndex
            };
        }

        IAccount<TWallet> IHDWallet<TWallet>.GetAccount(uint accountIndex)
        {
            var externalKeyPath = new KeyPath($"{accountIndex}'/0");
            var externalMasterKey = _masterKey.Derive(externalKeyPath);

            var internalKeyPath = new KeyPath($"{accountIndex}'/1");
            var internalMasterKey = _masterKey.Derive(internalKeyPath);

            return new AccountSecpBase<TWallet>(accountIndex, externalChain: externalMasterKey, internalChain: internalMasterKey);
        }

        /// <summary>
        /// Generates Account from master. Doesn't derive new path by accountIndexInfo
        /// </summary>
        /// <param name="accountMasterKey">Used to generate wallet</param>
        /// <param name="accountIndexInfo">Used only to store information</param>
        /// <returns></returns>
        public static IAccount<TWallet> GetAccountFromMasterKey(string accountMasterKey, uint accountIndexInfo)
        {
            IAccountHDWallet<TWallet> accountHDWallet = new AccountHDWalletSecpBase<TWallet>(accountMasterKey, accountIndexInfo);
            return accountHDWallet.Account;
        }
    }
}