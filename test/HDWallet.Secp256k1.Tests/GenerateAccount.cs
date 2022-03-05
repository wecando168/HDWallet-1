using HDWallet.Core;
using HDWallet.Secp256k1.Sample;
using NBitcoin;
using NUnit.Framework;

namespace HDWallet.Secp256k1.Tests
{
    public class GenerateAccount
    {
        [Test]
        public void ShouldCreateAccount()
        {
            IHDWallet<SampleSecp256k1Wallet> bitcoinHDWallet = new SampleSecp256k1HDWallet("conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch", "");

            var account0 = bitcoinHDWallet.GetAccount(0);

            var depositWallet0 = account0.GetExternalWallet(0);
            Assert.AreEqual("0374c393e8f757fa4b6af5aba4545fd984eae28ab84bda09df93d32562123b7a1c", depositWallet0.PublicKey.ToHex());

            var depositWallet1 = account0.GetExternalWallet(1);
            Assert.AreEqual("025166e4e70b4ae6fd0deab416ab1c3704f2aa5dbf451be7639ca48fe6d273773c", depositWallet1.PublicKey.ToHex());

            var changeWallet1 = account0.GetInternalWallet(0);
            Assert.AreEqual("02c1100b710dc70152106bf936cf57cfdafff5505b03790f5dcad45bcdef722921", changeWallet1.PublicKey.ToHex());


            var account1 = bitcoinHDWallet.GetAccount(1);

            var depositWallet10 = account1.GetExternalWallet(0);
            Assert.AreEqual("02b9a6e20677667522d6d710d182950dd5e67f60cee9e704e30f65395fd2cb866e", depositWallet10.PublicKey.ToHex());
            var depositWallet11 = account1.GetExternalWallet(1);
            Assert.AreEqual("02cb36da84d699131d9d9c680d94d5f8aa24a2029752b0b3a706dc4e25282493a0", depositWallet11.PublicKey.ToHex());

            var changeWallet10 = account1.GetInternalWallet(0);
            Assert.AreEqual("020edfe3b196fa8f853887221fd72edc343b168487437dba01b0047fccf96818c8", changeWallet10.PublicKey.ToHex());
            var changeWallet11 = account1.GetInternalWallet(1);
            Assert.AreEqual("02786e8b010eaeb52f22ce7a59c98741299847f1fa9d2bd3ba0bf10a48d613e35e", changeWallet11.PublicKey.ToHex());
        }

        [Test]
        public void ShouldCreateAccountWallet()
        {
            // Test vector created at https://iancoleman.io/bip39/  (BIP32 -> m/44'/0' -> 'Use hardened addresses' )
            IHDWallet<SampleSecp256k1Wallet> bitcoinHDWallet = new SampleSecp256k1HDWallet("conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch", "");
            Assert.AreEqual("02d8220f7a1528c1ff3684249b2c80fd3dcadf10cad466b05f66ec52a851ab6067", bitcoinHDWallet.GetAccountWallet(0).PublicKey.ToHex());
            Assert.AreEqual("0380450c855822c697fcee88347ff7281f70cc3de1d1f8cc5be9fe07db18019187", bitcoinHDWallet.GetAccountWallet(1).PublicKey.ToHex());
            Assert.AreEqual("032ee82769d33bb77eb6b0099f704bad94d35da5484e95410a7ba9893fa223128e", bitcoinHDWallet.GetAccountWallet(2).PublicKey.ToHex());
            Assert.AreEqual("03ad412ea25bd8192b0d738dbfcd8b43242f17cf5a4ffeb7b8e9a50072b99b4a7a", bitcoinHDWallet.GetAccountWallet(19).PublicKey.ToHex());
        }

        [Test]
        public void ShouldCreateAccountFromMasterKey()
        {
            // Account Extended Private Key for m/44'/0'/0' of mnemonic;
            // conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch
            // Checked from https://iancoleman.io/bip39
            var accountExtendedPrivateKey = "xprv9xyvwx1jBEBKwjZtXYogBwDyfXTyTa3Af6urV2dU843CyBxLu9J5GLQL4vMWvaW4q3skqAtarUvdGmBoWQZnU2RBLnmJdCM4FnbMa72xWNy";

            IAccount<SampleSecp256k1Wallet> accountHDWallet = new Account<SampleSecp256k1Wallet>(accountExtendedPrivateKey, Network.Main);
            
            // m/44'/0'/0'/0/0
            var depositWallet0 = accountHDWallet.GetExternalWallet(0);
            Assert.AreEqual("0374c393e8f757fa4b6af5aba4545fd984eae28ab84bda09df93d32562123b7a1c", depositWallet0.PublicKey.ToHex());

            // m/44'/0'/0'/0/1
            var depositWallet1 = accountHDWallet.GetExternalWallet(1);
            Assert.AreEqual("025166e4e70b4ae6fd0deab416ab1c3704f2aa5dbf451be7639ca48fe6d273773c", depositWallet1.PublicKey.ToHex());
        }
    }
}