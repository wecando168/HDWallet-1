using HDWallet.Core;
using HDWallet.Secp256r1;

namespace HDWallet.Neo
{
    public class NeoHdWallet : HDWallet<NeoWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = Purpose.Create(PurposeNumber.BIP44).Coin(CoinType.Neo);

        public NeoHdWallet(string words, string seedPassword = "") : base(words, seedPassword, _path) { }

        /// <summary>
        /// Generates Account from master. Doesn't derive new path by accountIndexInfo
        /// </summary>
        /// <param name="accountMasterKey">Used to generate wallet</param>
        /// <param name="accountIndexInfo">Used only to store information</param>
        /// <returns></returns>
        public static IAccount<NeoWallet> GetAccountFromMasterKey(string accountMasterKey, uint accountIndexInfo)
        {
            IAccountHDWallet<NeoWallet> accountHDWallet = new AccountHDWallet<NeoWallet>(accountMasterKey, accountIndexInfo);
            return accountHDWallet.Account;
        }
    }
}