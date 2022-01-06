using HDWallet.Core;
using NBitcoin;
using NBitcoin.DataEncoders;
using NUnit.Framework;

namespace HDWallet.Terra.Test
{
    public class GenerateAddress
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("b1e18ce76ac82ea0e9d752a51964131e1e7aad1856d359bfa596ae94b87ea181", "terra15ztavs8t6rqx032neqcg7mw5kr9w99eh7fufzw")]
        [TestCase("6064b87ce7bf411180de7bd4747880383730be5e0f6c8bb55f70cb7a63073731", "terra1yekxnnt9mr2p4cn7vxz0srvtpge6w3gne7e8wr")]
        [TestCase("1fe137d30cf57d60cef6bfc91c3fd17bff1c7f1dafba74c0cc4dec2129a7daa6", "terra1ppp4k8vwds5jsam6vx3v740vn2kuud29nfgjmk")]
        public void ShouldGenerateAddressesFromPrivateKey(string privateKey, string address)
        {
            var privKeyStr = Encoders.Hex.DecodeData(privateKey); 
            Key key = new Key(privKeyStr);

            // Works for Mainnet by default when no wallet instance is created
            IAddressGenerator addressGenerator =  new AddressGenerator();
            var actualAddress = addressGenerator.GenerateAddress(key.PubKey.ToBytes());

            Assert.AreEqual(address, actualAddress);
        }
    }
}