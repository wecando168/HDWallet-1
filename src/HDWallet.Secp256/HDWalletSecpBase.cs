using System;
using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Secp256
{
    /// <summary>
    /// Base class for Secp based HD wallets
    /// </summary>
    /// <typeparam name="TWallet"></typeparam>
    public abstract class HDWalletSecpBase<TWallet> : HdWalletBase, IHDWallet<TWallet> where TWallet : IWallet, new()
    {
        ExtKey _masterKey;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="words">BIP39 Mnemonic</param>
        /// <param name="passphrase">BIP39 Passphrase (optional)</param>
        /// <param name="path">Derivation Path (e.g. m/44'/0')</param>
        /// <returns></returns>
        public HDWalletSecpBase(string words, string passphrase, CoinPath path) : base(words, passphrase)
        {
            var masterKeyPath = new KeyPath(path.ToString());
            _masterKey = new ExtKey(base.BIP39Seed).Derive(masterKeyPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seed">BIP39 Seed</param>
        /// <param name="path">Derivation Path (e.g. m/44'/0')</param>
        /// <returns></returns>
        public HDWalletSecpBase(string seed, CoinPath path) : base(seed)
        {
            var masterKeyPath = new KeyPath(path.ToString());
            _masterKey = new ExtKey(base.BIP39Seed).Derive(masterKeyPath);
        }

        /// <summary>
        /// Returns a wallet from only seed, no derivation 
        /// </summary>
        /// <returns></returns>
        TWallet IHDWallet<TWallet>.GetMasterWallet()
        {
            var masterKey = new ExtKey(base.BIP39Seed);

            var privateKey = masterKey.PrivateKey.ToBytes();
            return new TWallet()
            {
                PrivateKeyBytes = privateKey
            };
        }

        /// <summary>
        /// Returns the wallet at m/purpose'/coin_type'/{accountIndex}' (hardened)
        /// </summary>
        /// <param name="accountIndex"></param>
        /// <returns></returns>
        TWallet IHDWallet<TWallet>.GetAccountWallet(uint accountIndex)
        {
            var externalKeyPath = new KeyPath($"{accountIndex}'");
            var externalMasterKey = _masterKey.Derive(externalKeyPath);

            return new TWallet()
            {
                PrivateKeyBytes = externalMasterKey.PrivateKey.ToBytes(),
                Index = accountIndex
            };
        }

        /// <summary>
        /// Returns the [accountIndex]th account to access wallets at m/purpose'/coin_type'/{accountIndex}'/[0/1]
        /// </summary>
        /// <param name="accountIndex"></param>
        /// <returns></returns>
        IAccount<TWallet> IHDWallet<TWallet>.GetAccount(uint accountIndex)
        {
            var externalKeyPath = new KeyPath($"{accountIndex}'/0");
            var externalMasterKey = _masterKey.Derive(externalKeyPath);

            var internalKeyPath = new KeyPath($"{accountIndex}'/1");
            var internalMasterKey = _masterKey.Derive(internalKeyPath);

            return new AccountSecpBase<TWallet>(externalChain: externalMasterKey, internalChain: internalMasterKey);
        }

        /// <summary>
        /// Generates an account from master. Doesn't derive new path by accountIndexInfo
        /// </summary>
        /// <param name="accountMasterKey">Used to generate wallet</param>
        /// <param name="accountIndexInfo">Used only to store information</param>
        /// <returns></returns>
        [Obsolete("'accountIndexInfo' is not being used, used the overloads.")]
        public static IAccount<TWallet> GetAccountFromMasterKey(string accountMasterKey, uint accountIndexInfo)
        {
            IAccountHDWallet<TWallet> accountHDWallet = new AccountHDWalletSecpBase<TWallet>(accountMasterKey, accountIndexInfo);
            return accountHDWallet.Account;
        }

        /// <summary>
        /// Generates an account from master for Mainnet.
        /// </summary>
        /// <param name="accountMasterKey">Used to generate wallet</param>
        /// <returns></returns>
        public static IAccount<TWallet> GetAccountFromMasterKey(string accountMasterKey)
        {
            IAccountHDWallet<TWallet> accountHDWallet = new AccountHDWalletSecpBase<TWallet>(accountMasterKey, Network.Main);
            return accountHDWallet.Account;
        }

        /// <summary>
        /// Generates an account from master for network: [network].
        /// </summary>
        /// <param name="accountMasterKey">Used to generate wallet</param>
        /// <param name="network"></param>
        /// <returns></returns>
        public static IAccount<TWallet> GetAccountFromMasterKey(string accountMasterKey, Network network)
        {
            IAccountHDWallet<TWallet> accountHDWallet = new AccountHDWalletSecpBase<TWallet>(accountMasterKey, network);
            return accountHDWallet.Account;
        }
    }
}