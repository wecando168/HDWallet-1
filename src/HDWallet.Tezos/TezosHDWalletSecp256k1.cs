using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Tezos
{
    public class TezosHDWalletSecp256k1 : HDWallet<TezosWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.Tezos);

        public TezosHDWalletSecp256k1(string mnemonic, string passphrase = "") : base(mnemonic, passphrase, _path) { }

        /// <summary>
        /// Generates Account from master. Doesn't derive new path by accountIndexInfo
        /// </summary>
        /// <param name="accountMasterKey">Used to generate wallet</param>
        /// <param name="accountIndexInfo">Used only to store information</param>
        /// <returns></returns>
        public static IAccount<TezosWallet> GetAccountFromMasterKey(string accountMasterKey, uint accountIndexInfo)
        {
            IAccountHDWallet<TezosWallet> accountHDWallet = new AccountHDWallet<TezosWallet>(accountMasterKey, accountIndexInfo);
            return accountHDWallet.Account;
        }
    }
}