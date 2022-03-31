using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Secp256
{
    // TODO: Add unit tests
    public class PublicAccountSecpBase<TWallet> : IPublicAccount<TWallet> where TWallet : IPublicWallet, new()
    {
        readonly BitcoinExtPubKey _bitcoinExtPubKey;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountMasterPublicKey"></param>
        /// <param name="network"></param>
        public PublicAccountSecpBase(string accountMasterPublicKey, Network network)
        {
            _bitcoinExtPubKey = new BitcoinExtPubKey(accountMasterPublicKey, network);
        }

        TWallet IPublicAccount<TWallet>.GetInternalPublicWallet(uint addressIndex)
        {
            ExtPubKey extKey = _bitcoinExtPubKey.ExtPubKey.Derive(new KeyPath($"1/{addressIndex}"));

            return new TWallet()
            {
                PublicKeyBytes = extKey.PubKey.ToBytes()
            };
        }

        TWallet IPublicAccount<TWallet>.GetExternalPublicWallet(uint addressIndex)
        {
            ExtPubKey extKey = _bitcoinExtPubKey.ExtPubKey.Derive(new KeyPath($"0/{addressIndex}"));

            return new TWallet()
            {
                PublicKeyBytes = extKey.PubKey.ToBytes()
            };
        }
    }
}