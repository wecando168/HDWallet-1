using HDWallet.Core;
using HDWallet.Secp256k1.Sample;
using NUnit.Framework;

namespace HDWallet.Secp256k1.Tests
{
    public class GeneratePublicWallet
    {
        [Test]
        public void ShouldGeneratePublicWallet()
        {
            IHDWallet<SampleSecp256k1Wallet> tronHDWallet = new SampleSecp256k1HDWallet("conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch", "");
            SampleSecp256k1Wallet wallet0 = tronHDWallet.GetAccount(0).GetExternalWallet(0);

            IPublicWallet pubWallet = new SampleSecp256k1PublicWallet(wallet0.PublicKeyBytes);
            Assert.AreEqual(wallet0.PublicKeyBytes, pubWallet.PublicKeyBytes);
        }
    }
}