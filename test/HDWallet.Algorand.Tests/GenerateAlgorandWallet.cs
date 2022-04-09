using NUnit.Framework;
using System;
using HDWallet.Algorand;
using HDWallet.Core;
using HDWallet.BIP32.Ed25519;
using HDWallet.Ed25519;

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
            IHDWallet<AlgorandWallet> hdWallet = new AlgorandHDWallet(mnemonic, "");

            var wallet = hdWallet.GetAccount(0).GetExternalWallet(1);
            Console.WriteLine("Address: {0}", wallet.Address);
            Console.WriteLine("PublicKey: {0}", wallet.PublicKeyBytes.ToHexString());
            Console.WriteLine("PrivateKey: {0}", wallet.PrivateKeyBytes.ToHexString());

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

        [Test]
        public void ShouldGeneratedFromXprv()
        {
            var mnemonic = "segment inhale symptom olive cheese tissue vacuum lazy sketch salt enroll wink oyster hen glory food weasel comic glow legal cute diet fun real";
            IHDWallet<AlgorandWallet> hdWallet = new AlgorandHDWallet(mnemonic, "");
            
            var account0 = hdWallet.GetAccount(0);
            Assert.AreEqual("xprv9s21ZrQH143K3xUTDYV4BjfSvuVscczTMw7LtNWgjYBLLGHPqagsJm9YyQihApsr8UFEP9ydzVzTdVezhaDVFDciCGMLhV5Yp8s2fRT7qh9", account0.GetWif());

            var wallet = account0.GetExternalWallet(1);
            Assert.AreEqual(expected: "5RKLKOVRU4WRWEKKI5I5Z6HTRNYA3XD6HGU34ZDRCDLJJ3DYQEOAOEWODY", wallet.Address);

            string xprv = "xprv9s21ZrQH143K3xUTDYV4BjfSvuVscczTMw7LtNWgjYBLLGHPqagsJm9YyQihApsr8UFEP9ydzVzTdVezhaDVFDciCGMLhV5Yp8s2fRT7qh9";
            ExtKey masterKey = ExtKey.CreateFromWif(xprv);
            IAccount<AlgorandWallet> account0FromXprv = new Account<AlgorandWallet>(masterKey);
            var actualWallet = account0FromXprv.GetExternalWallet(1);

            Assert.AreEqual(wallet.PublicKeyBytes, actualWallet.PublicKeyBytes);
            Assert.AreEqual(expected: "5RKLKOVRU4WRWEKKI5I5Z6HTRNYA3XD6HGU34ZDRCDLJJ3DYQEOAOEWODY", actualWallet.Address);

            account0FromXprv = new Account<AlgorandWallet>(xprv);
            actualWallet = account0FromXprv.GetExternalWallet(1);
            Assert.AreEqual(expected: "5RKLKOVRU4WRWEKKI5I5Z6HTRNYA3XD6HGU34ZDRCDLJJ3DYQEOAOEWODY", actualWallet.Address);
        }
    }
}