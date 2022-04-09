using System;
using HDWallet.BIP32.Ed25519;
using HDWallet.Core;

namespace HDWallet.Ed25519
{
    /// <summary>
    /// Account generated with Edwards Curve
    /// </summary>
    /// <typeparam name="TWallet"></typeparam>
    public class Account<TWallet> : IAccount<TWallet> where TWallet : IWallet, new()
    {
        private readonly ExtKey _masterKey;
        private readonly ExtKey _externalChain;
        private readonly ExtKey _internalChain;

        public Account(ExtKey accountMasterKey)
        {
            _masterKey = accountMasterKey;
            _externalChain = _masterKey.Derive(0);
            _internalChain = _masterKey.Derive(1);
        }

        public Account(string accountMasterKey)
        {
            _masterKey = ExtKey.CreateFromWif(accountMasterKey, NBitcoin.Network.Main);
            _externalChain = _masterKey.Derive(0);
            _internalChain = _masterKey.Derive(1);
        }

        public Account(string accountMasterKey, NBitcoin.Network network)
        {
            _masterKey = ExtKey.CreateFromWif(accountMasterKey, network);
            _externalChain = _masterKey.Derive(0);
            _internalChain = _masterKey.Derive(1);
        }

        TWallet IAccount<TWallet>.GetInternalWallet(uint addressIndex)
        {
            var extKey = _internalChain.Derive(addressIndex);
            return new TWallet()
            {
                PrivateKeyBytes = extKey.Key.PrivateKey
            };
        }

        TWallet IAccount<TWallet>.GetExternalWallet(uint addressIndex)
        {
            var extKey = _externalChain.Derive(addressIndex);
            return new TWallet()
            {
                PrivateKeyBytes = extKey.Key.PrivateKey
            };
        }

        string IAccount<TWallet>.GetWif()
        {
            return this._masterKey.GetWif();
        }
    }
}