using HDWallet.Core;
using HDWallet.Secp256;
using NBitcoin;

namespace HDWallet.Secp256k1
{
    /// <summary>
    /// Account generated with Elliptic Curve
    /// </summary>
    /// <typeparam name="TWallet"></typeparam>
    public class Account<TWallet> : AccountSecpBase<TWallet>, IAccount<TWallet> where TWallet : Wallet, new()
    {
        public Account(string accountMasterKey, Network network): base(accountMasterKey, network) {}

        public Account(ExtKey accountMasterKey) : base(accountMasterKey){}
    }
}