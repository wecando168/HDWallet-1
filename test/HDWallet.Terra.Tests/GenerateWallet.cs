using HDWallet.Core;
using NUnit.Framework;

namespace HDWallet.Terra.Test
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
            IHDWallet<TerraWallet> terraHDWallet = new TerraHDWallet("trap shoulder quantum fun health forward banana identify prosper steak cheese prison little eye dentist artwork term supply cradle mobile enemy angry switch labor", "");
            var account0 = terraHDWallet.GetAccount(0);
            TerraWallet wallet0 = account0.GetExternalWallet(0);
            Assert.AreEqual("b1e18ce76ac82ea0e9d752a51964131e1e7aad1856d359bfa596ae94b87ea181", wallet0.PrivateKey.ToHex());

            var terraWallet = new TerraWallet(wallet0.PrivateKey.ToHex());
            Assert.AreEqual(wallet0.PrivateKey.ToHex(), terraWallet.PrivateKey.ToHex());
            Assert.AreEqual(wallet0.Address, terraWallet.Address);
        }
    }
}