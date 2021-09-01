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
        public Account(uint accountIndex, ExtKey externalChain, ExtKey internalChain) : base(accountIndex, externalChain, internalChain)
        {

        }
    }
}