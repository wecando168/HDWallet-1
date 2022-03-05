using HDWallet.Core;
using HDWallet.Secp256k1;
using NUnit.Framework;

namespace HDWallet.Stacks.Test
{
    public class GenerateAccount
    {
        [Test]
        public void ShouldCreateAccount()
        {
            string words = "labor slow cloud ecology teach price cousin mountain cushion digital refuse priority dawn balcony step luxury obvious bracket lion insect brother code cave excess";
            IHDWallet<StacksWallet> wallet = new StacksHDWallet(words);
            var account0wallet0 = wallet.GetAccount(0).GetExternalWallet(0); // m/44'/195'/0'/0/0
            var t = account0wallet0.PublicKey.ToHex();
            Assert.AreEqual("037e83b6453f77b523a7f48d980e8aa294ae46b1dbc7e966799b151c151c49e2ac", account0wallet0.PublicKey.ToHex());

            // Account Extended Private Key for m/44'/195'/0';
            var accountExtendedPrivateKey = "xprv9zBCnh1bQSSWZhN1mkbZW33MGX1hiWLRtnXfpXqtZWnj5oEmR5wsaan2TZzk5h3AiDUqM3DQUddqeg2gHxKgpxwEEPA1pqncjjc1kFighEn";
            IAccount<StacksWallet> account = new Account<StacksWallet>(accountExtendedPrivateKey, NBitcoin.Network.Main);
            
            // m/44'/195'/0'/0/0
            var depositWallet0 = account.GetExternalWallet(0);
            Assert.AreEqual("037e83b6453f77b523a7f48d980e8aa294ae46b1dbc7e966799b151c151c49e2ac", depositWallet0.PublicKey.ToHex());

            Assert.AreEqual(account0wallet0.PublicKey, depositWallet0.PublicKey);
        }

        [Test]
        public void ShouldCreateAddressFromMasterKey()
        {
            var accountExtendedPrivateKey = "xprv9zBCnh1bQSSWZhN1mkbZW33MGX1hiWLRtnXfpXqtZWnj5oEmR5wsaan2TZzk5h3AiDUqM3DQUddqeg2gHxKgpxwEEPA1pqncjjc1kFighEn";
            IAccount<StacksWallet> account = new Account<StacksWallet>(accountExtendedPrivateKey, NBitcoin.Network.Main);
            var depositWallet0 = account.GetExternalWallet(0);
            Assert.AreEqual("037e83b6453f77b523a7f48d980e8aa294ae46b1dbc7e966799b151c151c49e2ac", depositWallet0.PublicKey.ToHex());
            Assert.AreEqual("STMQVWWA1JA2HSGB4KX02VN185DA3W8B5V7WAXK", depositWallet0.GetAddress(NetworkVersion.Testnet));
        }

        [Test]
        public void ShouldCreateStacksWalletFromMasterKey()
        {
            var accountExtendedPrivateKey = "xprv9zBCnh1bQSSWZhN1mkbZW33MGX1hiWLRtnXfpXqtZWnj5oEmR5wsaan2TZzk5h3AiDUqM3DQUddqeg2gHxKgpxwEEPA1pqncjjc1kFighEn";
            var account = StacksHDWallet.GetAccountFromMasterKey(accountExtendedPrivateKey);
            var depositWallet0 = account.GetExternalWallet(0);
            Assert.AreEqual("STMQVWWA1JA2HSGB4KX02VN185DA3W8B5V7WAXK", depositWallet0.GetAddress(NetworkVersion.Testnet));
        }
    }
}