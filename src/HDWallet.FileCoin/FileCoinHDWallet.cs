using System;
using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.FileCoin
{
    public class FileCoinHDWallet : HDWallet<FileCoinWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.FileCoin);

        public FileCoinHDWallet(string words, string seedPassword = "") : base(words, seedPassword, _path) {}
    }
}
