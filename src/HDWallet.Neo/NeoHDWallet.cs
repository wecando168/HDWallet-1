using HDWallet.Core;
using HDWallet.Secp256r1;
using NBitcoin;

namespace HDWallet.Neo
{
    public class NeoHdWallet : HDWallet<NeoWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.Neo);

        public NeoHdWallet(string mnemonic, string passphrase = "") : base(mnemonic, passphrase, _path) { }
    }
}