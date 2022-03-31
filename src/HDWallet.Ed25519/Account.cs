using System;
using HDWallet.Core;

namespace HDWallet.Ed25519
{
    /// <summary>
    /// Account generated with Edwards Curve
    /// </summary>
    /// <typeparam name="TWallet"></typeparam>
    public class Account<TWallet> : IAccount<TWallet> where TWallet : Wallet, IWallet, new()
    {
        readonly uint _accountIndex;
        readonly Func<string, TWallet> _deriveFunction;

        internal Account(uint accountIndex, Func<string, TWallet> deriveFunction)
        {
            _accountIndex = accountIndex;
            _deriveFunction = deriveFunction;
        }

        TWallet IAccount<TWallet>.GetInternalWallet(uint addressIndex)
        {
            var internalKeyPath = $"{_accountIndex}'/1'/{addressIndex}'";
            var internalWallet = _deriveFunction(internalKeyPath);

            return internalWallet;
        }

        TWallet IAccount<TWallet>.GetExternalWallet(uint addressIndex)
        {
            var externalKeyPath = $"{_accountIndex}'/0'/{addressIndex}'";
            var externalWallet = _deriveFunction(externalKeyPath);

            return externalWallet;
        }
    }
}