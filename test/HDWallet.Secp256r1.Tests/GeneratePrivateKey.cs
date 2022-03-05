using HDWallet.Core;
using HDWallet.Secp256r1.Sample;
using NUnit.Framework;

namespace HDWallet.Secp256r1.Tests
{
    public class GeneratePrivateKey
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldGenerateMasterPrivateKeys()
        {
            //string words = "push wrong tribe amazing again cousin hill belt silent found sketch monitor";
            
            //IHDWallet<Secp256r1Wallet> wallet = new Secp256r1HDWallet(mnemonic, passphrase: string.Empty);
            //var account = wallet.GetMasterWallet();

            //var privateKeyHex = account.PrivateKeyBytes.ToHexString();
            //var publicKeyHex = account.PublicKey.ToString();
            
            //Assert.AreEqual("945ee333591e6a709ed574a7ceba0bc09f650a7822ba0c2b7f5c8a5ead295374", privateKeyHex);
        }
    }
}