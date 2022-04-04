using HDWallet.Core;
using HDWallet.Secp256;
using NBitcoin;

namespace HDWallet.Secp256k1
{
    public class PublicAccount<TWallet> : PublicAccountSecpBase<TWallet>, IPublicAccount<TWallet> where TWallet : PublicWallet, new()
    {
        public PublicAccount(string accountMasterPublicKey, Network network): base(accountMasterPublicKey, network) {}
    }
}