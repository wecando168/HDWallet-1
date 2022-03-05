using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Tezos
{
    public class TezosHDWalletSecp256k1 : HDWallet<TezosWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.CreateCoinPath(CoinType.Tezos);

        public TezosHDWalletSecp256k1(string mnemonic, string passphrase = "") : base(mnemonic, passphrase, _path) { }
    }
}