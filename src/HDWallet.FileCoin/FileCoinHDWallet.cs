using System;
using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.FileCoin
{
    public class FileCoinHDWallet : HDWallet<FileCoinWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.FileCoin);

        public FileCoinHDWallet(string mnemonic, string passphrase = "") : base(mnemonic, passphrase, _path) {}
    }
}
