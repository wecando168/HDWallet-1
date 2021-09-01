using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Secp256
{
    public class AccountHDWalletSecpBase<TWallet> : IAccountHDWallet<TWallet> where TWallet : IWallet, new()
    {
        ExtKey _masterKey;
        uint _accountIndex;

        public AccountHDWalletSecpBase(string accountMasterKey, uint accountIndex)
        {
            BitcoinExtKey bitcoinExtKey = new BitcoinExtKey(accountMasterKey);
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