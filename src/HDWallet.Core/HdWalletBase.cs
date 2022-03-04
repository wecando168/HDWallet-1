using System;
using NBitcoin;
using Nethereum.Hex.HexConvertors.Extensions;

namespace HDWallet.Core
{
    public abstract class HdWalletBase
    {
        public string BIP39Seed { get; private set; }

        public HdWalletBase(string seed)
        {
            if(string.IsNullOrEmpty(seed)) throw new NullReferenceException(nameof(seed));

            BIP39Seed = seed;
        }

        public HdWalletBase(string mnemonic, string passphrase)
        {
            if(string.IsNullOrEmpty(mnemonic)) throw new NullReferenceException(nameof(mnemonic));

            var mneumonic = new Mnemonic(mnemonic);
            BIP39Seed = mneumonic.DeriveSeed(passphrase).ToHex();
        }
    }
}