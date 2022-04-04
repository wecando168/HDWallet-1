using NUnit.Framework;
using System;
using HDWallet.Algorand;
using HDWallet.Core;


namespace HDWallet.Algorand.Tests
{

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void ShouldGeneratedFromMnemonic()
        {
            var mnemonic = "segment inhale symptom olive cheese tissue vacuum lazy sketch salt enroll wink oyster hen glory food weasel comic glow legal cute diet fun real";
            var hdWallet = new AlgorandHDWallet(mnemonic, "");

            // var wallet = hdWallet.GetWalletFromPath<AlgorandWallet>("m/44'/283'/0'");
            var wallet = hdWallet.GetAccount(0).GetExternalWallet(1);
            Console.WriteLine("Path: {0}", wallet.Path);
            Console.WriteLine("Address: {0}", wallet.Address);
            Console.WriteLine("PublicKey: {0}", wallet.PublicKeyBytes.ToHexString());
            Console.WriteLine("PrivateKey: {0}", wallet.PrivateKeyBytes.ToHexString());

            Assert.AreEqual(expected: "m/44'/283'/0'/0'/1'", wallet.Path);
            Assert.AreEqual(expected: "5RKLKOVRU4WRWEKKI5I5Z6HTRNYA3XD6HGU34ZDRCDLJJ3DYQEOAOEWODY", wallet.Address);
            Assert.AreEqual(expected: "ec54b53ab1a72d1b114a4751dcf8f38b700ddc7e39a9be647110d694ec78811c", wallet.PublicKeyBytes.ToHexString());
            Assert.AreEqual(expected: "12c1a9991a216b2c8c86110e3bb6813920562e21090126b7813c525906f32f5f", wallet.PrivateKeyBytes.ToHexString());


            // Check Signature agains the one produced by go library.
            var helloBytes = System.Text.Encoding.ASCII.GetBytes("Hello Dear World!");
            var sig = wallet.Sign(helloBytes);
            var sighex = sig.R.ToHexString() + sig.S.ToHexString();
            Console.WriteLine("Signature: {0}", sighex);
            Assert.AreEqual(expected: "9db2f34d61c94d5c5cf84e3cc8f63203319ca827dd7d26071f298f93c8465e4665c83effdccc2db72839a02d812b4ab1c38fbdd662d42e7d4a15ff307abb1303", sighex);
        }
    }
}