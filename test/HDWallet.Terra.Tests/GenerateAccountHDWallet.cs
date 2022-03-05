using HDWallet.Core;
using HDWallet.Secp256k1;
using NUnit.Framework;

namespace HDWallet.Terra.Test
{
    public class GenerateAccount
    {
        [Test]
        public void ShouldCreateAccount()
        {
            string words = "trap shoulder quantum fun health forward banana identify prosper steak cheese prison little eye dentist artwork term supply cradle mobile enemy angry switch labor";
            IHDWallet<TerraWallet> wallet = new TerraHDWallet(words);
            var account0wallet0 = wallet.GetAccount(0).GetExternalWallet(0); // m/44'/330'/0'/0/0
            var t = account0wallet0.PublicKey.ToHex();
            Assert.AreEqual("03ad19db6505046a49ff45e09b261794e34fb1ca384b668c29ef479d8c5bd56be7", account0wallet0.PublicKey.ToHex());

            // Account Extended Private Key for m/44'/330'/0';
            var accountExtendedPrivateKey = "xprv9zBJRUoZ6njdJGQsJYdm8WqfVahRchQ9aBWRvX2Xgdpf5cwpHN2PS3Zm3WxveWbuhGifm4gW7Cb5uTYFjCdcDduBJTanb2hAK2xjrvh73AX";
            IAccount<TerraWallet> account = new Account<TerraWallet>(accountExtendedPrivateKey, NBitcoin.Network.Main);

            // m/44'/330'/0'/0/0
            var depositWallet0 = account.GetExternalWallet(0);
            Assert.AreEqual("03ad19db6505046a49ff45e09b261794e34fb1ca384b668c29ef479d8c5bd56be7", depositWallet0.PublicKey.ToHex());

            Assert.AreEqual(account0wallet0.PublicKey, depositWallet0.PublicKey);
        }

        [Test]
        public void ShouldCreateAddressFromMasterKey()
        {
            var accountExtendedPrivateKey = "xprv9zBJRUoZ6njdJGQsJYdm8WqfVahRchQ9aBWRvX2Xgdpf5cwpHN2PS3Zm3WxveWbuhGifm4gW7Cb5uTYFjCdcDduBJTanb2hAK2xjrvh73AX";
            IAccount<TerraWallet> account = new Account<TerraWallet>(accountExtendedPrivateKey, NBitcoin.Network.Main);
            var depositWallet0 = account.GetExternalWallet(0);
            Assert.AreEqual("03ad19db6505046a49ff45e09b261794e34fb1ca384b668c29ef479d8c5bd56be7", depositWallet0.PublicKey.ToHex());
        }

        [Test]
        public void ShouldCreateTerraWalletFromMasterKey()
        {
            var accountExtendedPrivateKey = "xprv9zBCnh1bQSSWZhN1mkbZW33MGX1hiWLRtnXfpXqtZWnj5oEmR5wsaan2TZzk5h3AiDUqM3DQUddqeg2gHxKgpxwEEPA1pqncjjc1kFighEn";
            var account = TerraHDWallet.GetAccountFromMasterKey(accountExtendedPrivateKey);
            var depositWallet0 = account.GetExternalWallet(0);
        }
    }
}