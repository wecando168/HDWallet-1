using HDWallet.Core;
using NBitcoin.Crypto;

namespace HDWallet.Secp256k1
{
    public class Secp256k1Signature : Signature
    {
        public bool TryGetECDSASignature(out ECDSASignature signature) => ECDSASignature.TryParseFromCompact(this.ToCompact(), out signature);

        public Secp256k1Signature(Signature signature)
        {
            this.R = signature.R;
            this.S = signature.S;
            this.RecId= signature.RecId;
        }
    }
}