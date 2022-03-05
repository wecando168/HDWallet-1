using System;
using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Secp256
{
    public class AccountSecpBase<TWallet> : IAccount<TWallet> where TWallet : IWallet, new()
    {
        private ExtKey _masterKey;
        private ExtKey ExternalChain { get; set; }
        private ExtKey InternalChain { get; set; }

        /// <summary>
        /// Generate for 'Main' network by default. 
        /// If you want to generate for testnet or regtest, use the overload
        /// </summary>
        /// <param name="accountMasterKey"></param>
        /// <param name="accountIndex">Only for information, isn't being used for derivation</param>
        [Obsolete("Use 'AccountHDWalletSecpBase(string accountMasterKey, uint accountIndex, Network network)'")]
        public AccountSecpBase(string accountMasterKey, uint accountIndex)
        {
            BitcoinExtKey bitcoinExtKey = new BitcoinExtKey(accountMasterKey, Network.Main);
            _masterKey = bitcoinExtKey.ExtKey;

            var externalKeyPath = new KeyPath("0");
            ExternalChain = _masterKey.Derive(externalKeyPath);

            var internalKeyPath = new KeyPath("1");
            InternalChain = _masterKey.Derive(internalKeyPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountMasterKey"></param>
        /// <param name="network"></param>
        public AccountSecpBase(string accountMasterKey, Network network)
        {
            BitcoinExtKey bitcoinExtKey = new BitcoinExtKey(accountMasterKey, network);
            _masterKey = bitcoinExtKey.ExtKey;

            var externalKeyPath = new KeyPath("0");
            ExternalChain = _masterKey.Derive(externalKeyPath);

            var internalKeyPath = new KeyPath("1");
            InternalChain = _masterKey.Derive(internalKeyPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountMasterKey"></param>
        public AccountSecpBase(ExtKey accountMasterKey)
        {
            _masterKey = accountMasterKey;

            var externalKeyPath = new KeyPath("0");
            ExternalChain = _masterKey.Derive(externalKeyPath);

            var internalKeyPath = new KeyPath("1");
            InternalChain = _masterKey.Derive(internalKeyPath);
        }

        TWallet IAccount<TWallet>.GetInternalWallet(uint addressIndex)
        {
            return GetWallet(addressIndex, isInternal: true);
        }

        TWallet IAccount<TWallet>.GetExternalWallet(uint addressIndex)
        {
            return GetWallet(addressIndex, isInternal: false);
        }

        private TWallet GetWallet(uint addressIndex, bool isInternal)
        {
            var extKey = isInternal ? InternalChain.Derive(addressIndex) : ExternalChain.Derive(addressIndex);

            return new TWallet()
            {
                PrivateKeyBytes = extKey.PrivateKey.ToBytes(),
                Index = addressIndex
            };
        }
    }
}