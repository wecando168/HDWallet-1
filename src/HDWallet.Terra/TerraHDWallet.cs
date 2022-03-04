using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Terra
{
    public class TerraHDWallet : HDWallet<TerraWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.Terra);

        public TerraHDWallet(string words, string seedPassword = "") : base(words, seedPassword, _path) {}

        /// <summary>
        /// Generates Account from master. Doesn't derive new path by accountIndexInfo
        /// </summary>
        /// <param name="accountMasterKey">Used to generate wallet</param>
        /// <param name="accountIndexInfo">Used only to store information</param>
        /// <returns></returns>
        public static IAccount<TerraWallet> GetAccountFromMasterKey(string accountMasterKey, uint accountIndexInfo)
        {
            IAccountHDWallet<TerraWallet> accountHDWallet = new AccountHDWallet<TerraWallet>(accountMasterKey, accountIndexInfo);
            return accountHDWallet.Account;
        }
    }
}