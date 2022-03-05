using HDWallet.Core;
using NBitcoin;
using NUnit.Framework;

namespace HDWallet.Secp256.Tests
{
    public class GenerateAccount
    {

        [Test]
        public void ShouldCreateAccountWallet()
        {
            // Test vector created at https://iancoleman.io/bip39/  (BIP32 -> m/44'/0' -> 'Use hardened addresses' )
            IHDWallet<SampleSecp256Wallet> bitcoinHDWallet = new SampleSecp256HDWallet("conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch", "");

            // m/44'/0'/0'
            Assert.AreEqual("L5KBLaoFvm8opFP3zU9dazEtCxGzzUnxey5hMStMnaTFMb5ey7M2", bitcoinHDWallet.GetAccountWallet(0).PrivateKey.GetWif(network: Network.Main).ToString());
            
            // m/44'/0'/1'
            Assert.AreEqual("L1TeJowWu5k66vb4gvUc29G8e12ojGzt62ZKvzEhsxn8wjL4e6BU", bitcoinHDWallet.GetAccountWallet(1).PrivateKey.GetWif(network: Network.Main).ToString());

            // m/44'/0'/2'
            Assert.AreEqual("KwXcXpPigHpMsuiavMpgGHqycokjHrHvCrGbk9jAPPsUd23au2Di", bitcoinHDWallet.GetAccountWallet(2).PrivateKey.GetWif(network: Network.Main).ToString());

            // m/44'/0'/19'
            Assert.AreEqual("KzsMxiea73UcQ6uMtEdKG9egv3TXGyYEkhcgBfELoaA2GR9RjX1x", bitcoinHDWallet.GetAccountWallet(19).PrivateKey.GetWif(network: Network.Main).ToString());
        }

        [Test]
        public void ShouldCreateAccount()
        {
            // Test vector created at https://iancoleman.io/bip39/  (BIP44 -> m/44'/0'/0')
            IHDWallet<SampleSecp256Wallet> bitcoinHDWallet = new SampleSecp256HDWallet("conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch", "");

            // m/44'/0'/0'
            var account0 = bitcoinHDWallet.GetAccount(0);

            // m/44'/0'/0'/0/0
            var depositWallet0 = account0.GetExternalWallet(0);
            Assert.AreEqual("L47mbshDe8n14kxQFoTavV8fQgRAWtthcS1ZBTd22VDJj8kriU2z", depositWallet0.PrivateKey.GetWif(network: Network.Main).ToString());

            // m/44'/0'/0'/0/1
            var depositWallet1 = account0.GetExternalWallet(1);
            Assert.AreEqual("L5KiCVyAMd5fRFtkaHXx1dxaX8jHHYzepzy5eUe8jUA23PwLnKKs", depositWallet1.PrivateKey.GetWif(network: Network.Main).ToString());

            // m/44'/0'/0'/1/0
            var changeWallet1 = account0.GetInternalWallet(0);
            Assert.AreEqual("L3a3LT9BARQoCsJo1Xdd2EqPfj3b79RVCd8UoonNY8V5UvVV2CjQ", changeWallet1.PrivateKey.GetWif(network: Network.Main).ToString());


            // m/44'/0'/1'
            var account1 = bitcoinHDWallet.GetAccount(1);

            // m/44'/0'/1'/0/0
            var depositWallet10 = account1.GetExternalWallet(0);
            Assert.AreEqual("L1NNWjzAfR9AH3KJNJbemvgXXAbLBY78tWoAryN2jUXyFgDuvhr5", depositWallet10.PrivateKey.GetWif(network: Network.Main).ToString());
            // m/44'/0'/1'/0/1
            var depositWallet11 = account1.GetExternalWallet(1);
            Assert.AreEqual("Kz8mH5hRc2V5nCs6aZeYCgeo97qHmkVWV8zUvjJ1vz917jG8bULD", depositWallet11.PrivateKey.GetWif(network: Network.Main).ToString());

            // m/44'/0'/1'/1/0
            var changeWallet10 = account1.GetInternalWallet(0);
            Assert.AreEqual("KzW2yBn7orD6LnKhPJQoeniU6Xnak6pEjHFXMVmeNpXhso1YcWZa", changeWallet10.PrivateKey.GetWif(network: Network.Main).ToString());
            // m/44'/0'/1'/1/1
            var changeWallet11 = account1.GetInternalWallet(1);
            Assert.AreEqual("KwjjWExn8j1gZgCN9MCu5yfzK2T5gsrKfQ6JCSManpywLxY5Wkbz", changeWallet11.PrivateKey.GetWif(network: Network.Main).ToString());
        }

        [Test]
        public void ShouldCreateAccountFromMasterKey()
        {
            // Test vector created at https://iancoleman.io/bip39/  (BIP44 -> m/44'/0'/0')
            // conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch

            // m/44'/0'/0'
            var account0 = SampleSecp256HDWallet.GetAccountFromMasterKey("xprv9xyvwx1jBEBKwjZtXYogBwDyfXTyTa3Af6urV2dU843CyBxLu9J5GLQL4vMWvaW4q3skqAtarUvdGmBoWQZnU2RBLnmJdCM4FnbMa72xWNy");

            // m/44'/0'/0'/0/0
            var depositWallet0 = account0.GetExternalWallet(0);
            Assert.AreEqual("L47mbshDe8n14kxQFoTavV8fQgRAWtthcS1ZBTd22VDJj8kriU2z", depositWallet0.PrivateKey.GetWif(network: Network.Main).ToString());

            // m/44'/0'/0'/0/1
            var depositWallet1 = account0.GetExternalWallet(1);
            Assert.AreEqual("L5KiCVyAMd5fRFtkaHXx1dxaX8jHHYzepzy5eUe8jUA23PwLnKKs", depositWallet1.PrivateKey.GetWif(network: Network.Main).ToString());

            // m/44'/0'/0'/1/0
            var changeWallet1 = account0.GetInternalWallet(0);
            Assert.AreEqual("L3a3LT9BARQoCsJo1Xdd2EqPfj3b79RVCd8UoonNY8V5UvVV2CjQ", changeWallet1.PrivateKey.GetWif(network: Network.Main).ToString());


            // m/44'/0'/1'
            var account1 = SampleSecp256HDWallet.GetAccountFromMasterKey("xprv9xyvwx1jBEBKzzkKDDb2JrAKYhFfDeyKcHFxdkrdEBh4donr7VqmAV2WWk7mFRjRgBAap4akcdejnt22dK17eEgrFzpgw5TFGcGbPpT1vJu");

            // m/44'/0'/1'/0/0
            var depositWallet10 = account1.GetExternalWallet(0);
            Assert.AreEqual("L1NNWjzAfR9AH3KJNJbemvgXXAbLBY78tWoAryN2jUXyFgDuvhr5", depositWallet10.PrivateKey.GetWif(network: Network.Main).ToString());
            // m/44'/0'/1'/0/1
            var depositWallet11 = account1.GetExternalWallet(1);
            Assert.AreEqual("Kz8mH5hRc2V5nCs6aZeYCgeo97qHmkVWV8zUvjJ1vz917jG8bULD", depositWallet11.PrivateKey.GetWif(network: Network.Main).ToString());

            // m/44'/0'/1'/1/0
            var changeWallet10 = account1.GetInternalWallet(0);
            Assert.AreEqual("KzW2yBn7orD6LnKhPJQoeniU6Xnak6pEjHFXMVmeNpXhso1YcWZa", changeWallet10.PrivateKey.GetWif(network: Network.Main).ToString());
            // m/44'/0'/1'/1/1
            var changeWallet11 = account1.GetInternalWallet(1);
            Assert.AreEqual("KwjjWExn8j1gZgCN9MCu5yfzK2T5gsrKfQ6JCSManpywLxY5Wkbz", changeWallet11.PrivateKey.GetWif(network: Network.Main).ToString());
        }
    }
}