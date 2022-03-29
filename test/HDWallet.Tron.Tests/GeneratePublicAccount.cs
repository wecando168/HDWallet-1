using HDWallet.Core;
using HDWallet.Secp256k1;
using NUnit.Framework;

namespace HDWallet.Tron.Tests
{
    public class GeneratePublicAccount
    {
        [Test]
        public void ShouldCreateAddressFromPublicMasterKey()
        {
            var accountExtendedPrivateKey = "xprv9yB7gYqxZdR4AUGppodn1XL7RpJkRUnDE1fM6oEY4LQrvstH1qCdfFHmW9msdqAsPEPqr9LhYmw2nZQfk8uBbk1KYhzjNVzWdwsugTTgNvc";
            IAccount<TronWallet> account = new Account<TronWallet>(accountExtendedPrivateKey, NBitcoin.Network.Main);
            var depositWallet0 = account.GetExternalWallet(0);
            
            string xpub = "xpub6CAU64NrPzyMNxMHvqAnNfGqyr9EpwW4bEawuBe9cfwqogDRZNWtD3cFMQR1ZgpkMb91zcPrj5qu5Vyqh5yeY4i9aPJymXjxDW7sUvKh8U6";
            IPublicAccount<TronPublicWallet> tronPublicWallet = new PublicAccount<TronPublicWallet>(xpub, NBitcoin.Network.Main);
            IPublicWallet depositPublicWallet0 = tronPublicWallet.GetExternalPublicWallet(0);

            Assert.AreEqual("TMQ3RtdjwCCoeA2RAYiTrFNZTKtzh5t9YQ", depositPublicWallet0.Address);
            Assert.AreEqual(depositWallet0.Address, depositPublicWallet0.Address);
        }

        [Test]
        public void ShouldCreateTronPublicWalletFromPublicMasterKey()
        {
            // xprv9yB7gYqxZdR4AUGppodn1XL7RpJkRUnDE1fM6oEY4LQrvstH1qCdfFHmW9msdqAsPEPqr9LhYmw2nZQfk8uBbk1KYhzjNVzWdwsugTTgNvc
            string xpub = "xpub6CAU64NrPzyMNxMHvqAnNfGqyr9EpwW4bEawuBe9cfwqogDRZNWtD3cFMQR1ZgpkMb91zcPrj5qu5Vyqh5yeY4i9aPJymXjxDW7sUvKh8U6";
            IPublicAccount<TronPublicWallet> tronPublicWallet = new PublicAccount<TronPublicWallet>(xpub, NBitcoin.Network.Main);

            IPublicWallet depositWallet0 = tronPublicWallet.GetExternalPublicWallet(0);
            Assert.AreEqual("TMQ3RtdjwCCoeA2RAYiTrFNZTKtzh5t9YQ", depositWallet0.Address);
        }
    }
}