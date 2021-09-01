using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Secp256
{
    public class AccountSecpBase<TWallet> : IAccount<TWallet> where TWallet : IWallet, new()
    {
        public uint AccountIndex { get; set; }
        private ExtKey ExternalChain { get; set; }
        private ExtKey InternalChain { get; set; }


        public AccountSecpBase(uint accountIndex, ExtKey externalChain, ExtKey internalChain)
        {
            ExternalChain = externalChain;
            InternalChain = internalChain;
            AccountIndex = accountIndex;
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

        TWallet IAccount<TWallet>.GetInternalWallet(uint addressIndex)
        {
            return GetWallet(addressIndex, isInternal: true);
        }

        TWallet IAccount<TWallet>.GetExternalWallet(uint addressIndex)
        {
            return GetWallet(addressIndex, isInternal: false);
        }
    }
}