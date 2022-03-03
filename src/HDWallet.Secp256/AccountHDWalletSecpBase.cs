using System;
using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Secp256
{
    public class AccountHDWalletSecpBase<TWallet> : IAccountHDWallet<TWallet> where TWallet : IWallet, new()
    {
        ExtKey _masterKey;
        uint _accountIndex;

        /// <summary>
        /// Generate for 'Main' network by default. 
        /// If you want to generate for testnet or regtest, use the overload
        /// </summary>
        /// <param name="accountMasterKey"></param>
        /// <param name="accountIndex">Only for information, isn't being used for derivation</param>
        [Obsolete("Use 'AccountHDWalletSecpBase(string accountMasterKey, uint accountIndex, Network network)'")]
        public AccountHDWalletSecpBase(string accountMasterKey, uint accountIndex)
        {
            BitcoinExtKey bitcoinExtKey = new BitcoinExtKey(accountMasterKey, Network.Main);
            _masterKey = bitcoinExtKey.ExtKey;
            _accountIndex = accountIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountMasterKey"></param>
        /// <param name="accountIndex">Only for information, isn't being used for derivation</param>
        /// <param name="network"></param>
        public AccountHDWalletSecpBase(string accountMasterKey, uint accountIndex, Network network)
        {
            BitcoinExtKey bitcoinExtKey = new BitcoinExtKey(accountMasterKey, network);
            _masterKey = bitcoinExtKey.ExtKey;
            _accountIndex = accountIndex;
        }

        IAccount<TWallet> IAccountHDWallet<TWallet>.Account => GetAccount();
        
        IAccount<TWallet> GetAccount()
        {
            var externalKeyPath = new KeyPath("0");
            var externalMasterKey = _masterKey.Derive(externalKeyPath);

            var internalKeyPath = new KeyPath("1");
            var internalMasterKey = _masterKey.Derive(internalKeyPath);

            return new AccountSecpBase<TWallet>(_accountIndex, externalChain: externalMasterKey, internalChain: internalMasterKey);
        }
    }
}