using HDWallet.Core;
using NUnit.Framework;

namespace HDWallet.Stacks.Test
{
    public class GenerateWallet
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ShouldGenerateWalletFromPrivateKey()
        {
            IHDWallet<StacksWallet> stacksHDWallet = new StacksHDWallet("labor slow cloud ecology teach price cousin mountain cushion digital refuse priority dawn balcony step luxury obvious bracket lion insect brother code cave excess", "");
            var account0 = stacksHDWallet.GetAccount(0);
            StacksWallet wallet0 = account0.GetExternalWallet(0);
            Assert.AreEqual("c216ddb515f247a7463be98569ba0c526d9da33ddf345e73ef78f0221c3427f3", wallet0.PrivateKey.ToHex());
            Assert.AreEqual("STMQVWWA1JA2HSGB4KX02VN185DA3W8B5V7WAXK",wallet0.GetAddress(NetworkVersion.Testnet) );

            var stacksWallet = new StacksWallet("c216ddb515f247a7463be98569ba0c526d9da33ddf345e73ef78f0221c3427f3");
            Assert.AreEqual(wallet0.PrivateKey.ToHex(), stacksWallet.PrivateKey.ToHex());
            Assert.AreEqual(wallet0.Address, stacksWallet.Address);
        }
    }
}