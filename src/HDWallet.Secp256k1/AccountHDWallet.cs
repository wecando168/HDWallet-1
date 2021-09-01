using HDWallet.Core;
using HDWallet.Secp256;

namespace HDWallet.Secp256k1
{
    public class AccountHDWallet<TWallet> : AccountHDWalletSecpBase<TWallet>, IAccountHDWallet<TWallet> where TWallet : Wallet, new()
    {
        public AccountHDWallet(string accountMasterKey, uint accountIndex) : base(accountMasterKey, accountIndex)
        {

        }
    }
}