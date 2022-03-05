using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Ethereum
{
    public class EthereumHDWallet : HDWallet<EthereumWallet>, IHDWallet<EthereumWallet>
    {
        private static readonly HDWallet.Core.CoinPath _path = M.BIP44.Ethereum;

        public EthereumHDWallet(string seed) : base(seed, _path)
        {
        }

        public EthereumHDWallet(string mnemonic, string passphrase) : base(mnemonic, passphrase, _path)
        {
        }
    }
}