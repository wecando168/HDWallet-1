using HDWallet.Core;

namespace HDWallet.Neo
{
    public class NeoSignature : Signature
    {
        public byte[] SignatureBytes => Helper.Concat(this.R, this.S);
        public string SignatureHex => Helper.ToHexString(this.SignatureBytes);

        public NeoSignature(Signature signature)
        {
            this.R = signature.R;
            this.S = signature.S;
            this.RecId= signature.RecId;
        }
    }
}