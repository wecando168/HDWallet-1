using HDWallet.Core;
using NBitcoin;
using NBitcoin.DataEncoders;
using NUnit.Framework;

namespace HDWallet.Stacks.Test
{
    public class GenerateAddress
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("943dde0bd4976c48a8b0d74a25a5002da6cba0837b7f7e319b62a0408063dc6b", "SP3AWK55E4R39A1ADFYG571KV7XGPYRS8Y63CKJYG")]
        [TestCase("9cfdf2272aaa76320a8f9ff191787c36a316633f2ec2347170db87052679e235", "SP188V5EYCFZCA3NAA3WN40DDD6Q9PC4DB1G4X4D4")]
        [TestCase("8c50b770e80239b0102472d93856f1dca49c192d5fa0bca29c1b5478597f552f", "SP28WJNFNE09EZ5R38YQEGN1ZQ0X1A20PPFG2GK8J")]
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