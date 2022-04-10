using System;
using HDWallet.Core;
using HDWallet.Ed25519;

namespace HDWallet.Cardano
{
    public class CardanoHDWallet : HdWalletEd25519<CardanoWallet>
    {
        private static readonly CoinPath _path = Purpose.Create(PurposeNumber.CIP1852).CreateCoinPath(CoinType.Cardano);

        internal CardanoHDWallet(string seed) : base(seed, _path) {}
        public CardanoHDWallet(string mnemonic, string passphrase) : base(mnemonic, passphrase, _path) {}
    }
}