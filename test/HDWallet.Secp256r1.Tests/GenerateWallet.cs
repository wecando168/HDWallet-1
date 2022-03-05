using System;
using HDWallet.Core;
using HDWallet.Secp256r1.Sample;
using Neo;
using Neo.SmartContract;
using Neo.Wallets;
using NUnit.Framework;

namespace HDWallet.Secp256r1.Tests
{
    public class NeoWallet : Wallet, IWallet
    {
        public NeoWallet() { }

        public NeoWallet(string privateKey) : base(privateKey) { }

        protected override IAddressGenerator GetAddressGenerator()
        {
            return new NullAddressGenerator();
        }

        public string GetAddress()
        {
            throw new NotImplementedException();
        }
    }

    public class GenerateWallet
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ShouldGenerateWalletFromPrivateKey()
        {
             byte[] privateKey = { 0x01,0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01};
            var wallet = new NeoWallet{
                PrivateKeyBytes = privateKey
            };
            var pubKeyStr = wallet.PublicKey.ToString();

             KeyPair keyPair = new KeyPair(privateKey);
             var pubKey = keyPair.PublicKey.ToString();

             Assert.AreEqual(pubKeyStr, pubKey);
        }
    }
}