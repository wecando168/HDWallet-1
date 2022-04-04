using HDWallet.Core;

namespace HDWallet.Algorand
{
    public class AlgorandSignature : Signature
    {
        public byte[] SignatureBytes => Helper.Concat(this.R, this.S);
        public string SignatureHex => Helper.ToHexString(this.SignatureBytes);

        public AlgorandSignature(Signature signature)
        {
            this.R = signature.R;
            this.S = signature.S;
            this.RecId= signature.RecId;
        }
    }
}