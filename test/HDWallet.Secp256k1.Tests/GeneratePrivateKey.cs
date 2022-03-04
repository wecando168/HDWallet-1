using HDWallet.Core;
using HDWallet.Secp256k1.Sample;
using NUnit.Framework;

namespace HDWallet.Secp256k1.Tests
{
    public class GeneratePrivateKey
    {
        [Test]
        public void ShouldGenerateMasterPrivateKeys()
        {
            string words = "push wrong tribe amazing again cousin hill belt silent found sketch monitor";
            
            IHDWallet<SampleSecp256k1Wallet> wallet = new SampleSecp256k1HDWallet(words, passphrase: string.Empty);
            var account = wallet.GetMasterWallet();

            var privateKeyHex = account.PrivateKey.ToHex();
            var publicKeyHex = account.PublicKey.Decompress().ToHex();
            
            Assert.AreEqual("945ee333591e6a709ed574a7ceba0bc09f650a7822ba0c2b7f5c8a5ead295374", privateKeyHex);
        }
    }
}