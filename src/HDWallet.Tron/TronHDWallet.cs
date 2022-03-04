using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Tron
{
    public class TronHDWallet : HDWallet<TronWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.Tron);

        public TronHDWallet(string mnemonic, string passphrase = "") : base(mnemonic, passphrase, _path) {}

        /// <summary>
        /// Generates Account from master. Doesn't derive new path by accountIndexInfo
        /// </summary>
        /// <param name="accountMasterKey">Used to generate wallet</param>
        /// <param name="accountIndexInfo">Used only to store information</param>
        /// <returns></returns>
        public static IAccount<TronWallet> GetAccountFromMasterKey(string accountMasterKey, uint accountIndexInfo)
        {
            IAccountHDWallet<TronWallet> accountHDWallet = new AccountHDWallet<TronWallet>(accountMasterKey, accountIndexInfo);
            return accountHDWallet.Account;
        }
    }
}