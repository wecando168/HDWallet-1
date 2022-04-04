using System.Text;
using HDWallet.Core;
using HDWallet.Ed25519;

namespace HDWallet.Algorand
{
    
    public class AlgorandWallet : Wallet, IWallet
    {
        public AlgorandWallet(){}

        public AlgorandWallet(string privateKey) : base(privateKey) {}

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new AddressGenerator();
        }

        public new AlgorandSignature Sign(byte[] message)
        {
            return new AlgorandSignature(base.Sign(message));
        }
    }
}