using HDWallet.Core;
using HDWallet.Secp256;
using HDWallet.Secp256k1.Sample;
using NBitcoin;
using NUnit.Framework;

namespace HDWallet.Secp256k1.Tests
{
    public class GeneratePublicAccount
    {
        [Test]
        public void ShouldCreatePublicAccountFromMasterKey()
        {
            // conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch
            // m/44'/0'/0'
            // xprv9xyvwx1jBEBKwjZtXYogBwDyfXTyTa3Af6urV2dU843CyBxLu9J5GLQL4vMWvaW4q3skqAtarUvdGmBoWQZnU2RBLnmJdCM4FnbMa72xWNy
            var accountExtendedPublicKey = "xpub6ByHMTYd1bjdADeMdaLgZ5AiDZJTs2m22KqTHR35gPaBqzHVSgcKp8iovBC76BJGiNEDM5cDNUsY7iv1htS3mWK7ue6LqNfZaHA1VbxPQHA";
            
            IPublicAccount<SampleSecp256k1PublicWallet> publicAccount = new PublicAccount<SampleSecp256k1PublicWallet>(accountExtendedPublicKey, Network.Main);
            
            // m/44'/0'/0'/0/0
            var depositWallet0 = publicAccount.GetExternalPublicWallet(0);
            Assert.AreEqual("0374c393e8f757fa4b6af5aba4545fd984eae28ab84bda09df93d32562123b7a1c", depositWallet0.PublicKeyBytes.ToHexString());

            // m/44'/0'/0'/0/1
            var depositWallet1 = publicAccount.GetExternalPublicWallet(1);
            Assert.AreEqual("025166e4e70b4ae6fd0deab416ab1c3704f2aa5dbf451be7639ca48fe6d273773c", depositWallet1.PublicKeyBytes.ToHexString());
        }

        [Test]
        public void ShouldMatchAccountsFromXprixOrXpub()
        {
            var accountExtendedPrivateKey = "xprv9xyvwx1jBEBKwjZtXYogBwDyfXTyTa3Af6urV2dU843CyBxLu9J5GLQL4vMWvaW4q3skqAtarUvdGmBoWQZnU2RBLnmJdCM4FnbMa72xWNy";
            IAccount<SampleSecp256k1Wallet> accountHDWallet = new Account<SampleSecp256k1Wallet>(accountExtendedPrivateKey, Network.Main);

            var accountExtendedPublicKey = "xpub6ByHMTYd1bjdADeMdaLgZ5AiDZJTs2m22KqTHR35gPaBqzHVSgcKp8iovBC76BJGiNEDM5cDNUsY7iv1htS3mWK7ue6LqNfZaHA1VbxPQHA";
            IPublicAccount<SampleSecp256k1PublicWallet> publicAccount = new PublicAccount<SampleSecp256k1PublicWallet>(accountExtendedPublicKey, Network.Main);
            
            Assert.AreEqual(publicAccount.GetExternalPublicWallet(0).PublicKeyBytes, accountHDWallet.GetExternalWallet(0).PublicKeyBytes);
            Assert.AreEqual(publicAccount.GetExternalPublicWallet(1).PublicKeyBytes, accountHDWallet.GetExternalWallet(1).PublicKeyBytes);

            Assert.AreEqual(publicAccount.GetInternalPublicWallet(0).PublicKeyBytes, accountHDWallet.GetInternalWallet(0).PublicKeyBytes);
            Assert.AreEqual(publicAccount.GetInternalPublicWallet(1).PublicKeyBytes, accountHDWallet.GetInternalWallet(1).PublicKeyBytes);
        }
    }
}