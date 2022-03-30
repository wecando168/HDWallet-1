using HDWallet.Core;
using HDWallet.Secp256k1;

namespace HDWallet.Ethereum
{
    public class EthereumSignature : Secp256k1Signature
    {
        byte[] SignatureBytes => Helper.Concat(this.R, this.S);
        string SignatureString => Helper.ToHexString(this.SignatureBytes);
        public string SignatureHex => "0x" + SignatureString + new[] {(byte) (RecId + 27)}.ToHexString();

        public EthereumSignature(Signature signature) : base(signature) {}
    }
}