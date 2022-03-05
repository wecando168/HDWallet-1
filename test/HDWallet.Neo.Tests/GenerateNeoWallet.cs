using HDWallet.Core;
using Neo.Cryptography.ECC;
using NUnit.Framework;
using System;

namespace HDWallet.Neo.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldGenerateFromPrivateKey()
        {
            var privateKeyString = "5c609164a666ee35489e643d210436890fc4990fb2b1544c956fc4603b0d1c29";
            NeoWallet wallet = new NeoWallet(privateKeyString);
            var address = wallet.GetAddress();
            var defaultAddress = wallet.Address;

            Assert.AreEqual("NLToPSKMAMqPBrz38zXAkVrwrB5FFZXyue", address);
            Assert.AreEqual("NLToPSKMAMqPBrz38zXAkVrwrB5FFZXyue", defaultAddress);
            Assert.AreEqual("0201e390b04e24bac115c2172ad9f3a7e9abfc1d87e99f3aa74bd5da0839ec080a", wallet.PublicKey.ToString());
        }

        [Test]
        public void ShouldGeneratedFromMnemonic()
        {
            var mnemonic = "dust dry odor unique impose craft adapt fatigue home bag kit primary advice rose stable error core shop entry vacuum pitch skill enhance pretty";
            IHDWallet<NeoWallet> hdWallet = new NeoHdWallet(mnemonic, "123456");

            var wallet = hdWallet.GetAccount(0).GetExternalWallet(0);

            Assert.AreEqual("0bff59a04f762ede6ca9a02b9e0d550574e57e979ee3d7c9d44d432ab77eaa6c", wallet.PrivateKeyBytes.ToHexString());
            Assert.AreEqual("035ae04176940923a8dee307f8d5f65139095b6d5d30c3063363a044a399e1a226", wallet.PublicKey.ToString());
            Assert.AreEqual("NdpXh2dae7KfZNFqYjrALCyhDshJtThmNN", wallet.Address);
        }

        [Test]
        public void ShouldCreateAddressFromMasterKey()
        {
            var accountExtendedPrivateKey = "xprv9ygCPYxKvwkSoQvKtcsfc4AYx7YBMWqkSZ8u7yAD1Ydz9muWdjNgZN6vdg1QBPZ9rYZdKbhPnmseYmHbJCSqkuxPJUzPHc5i6PQto4gvz6M";
            IAccount<NeoWallet> accountHDWallet = NeoHdWallet.GetAccountFromMasterKey(accountExtendedPrivateKey);
            var depositWallet0 = accountHDWallet.GetExternalWallet(0);
            Assert.AreEqual("NeWuyvtyLL2WUSZpW9zHrAe6AwUCPxmS1R", depositWallet0.Address);
        }
    }
}