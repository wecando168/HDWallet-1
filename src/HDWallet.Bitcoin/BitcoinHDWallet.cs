using System;
using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Bitcoin
{
    public class BitcoinHDWallet : HDWallet<BitcoinWallet>, IHDWallet<BitcoinWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP84.Bitcoin;

        public BitcoinHDWallet(string seed) : base(seed, _path) {}
        public BitcoinHDWallet(string mnemonic, string passphrase) : base(mnemonic, passphrase, _path) {}
    }
}