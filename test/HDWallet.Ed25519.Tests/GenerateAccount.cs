using System;
using HDWallet.BIP32.Ed25519;
using HDWallet.Core;
using HDWallet.Ed25519.Sample;
using NUnit.Framework;

namespace HDWallet.Ed25519.Tests
{
    public class GenerateAccount
    {
        [TestCase(
            "000102030405060708090a0b0c0d0e0f", 
            "xprv9s21ZrQH143K3VX7GQTkxonbgca94bts9EdRC1ZKN2Z9BA3JXZxbUwo4kS28ECmXhK1NicjQ7yBwWbZXgjRVktP6Tzi4YqetK5ueSA2CaXP")]
        [TestCase(
            "fffcf9f6f3f0edeae7e4e1dedbd8d5d2cfccc9c6c3c0bdbab7b4b1aeaba8a5a29f9c999693908d8a8784817e7b7875726f6c696663605d5a5754514e4b484542", 
            "xprv9s21ZrQH143K4Sd1z5fLT9D6CsVWg33mA1TDfKPzKavYR47H1cJaX16paqoyUuw3g1Zm6GHruGNpXqdVk8BVoZ8bLE3DYQpudN4C9H391kJ")]
        [TestCase( // elevator myth eyebrow language again room trim link boss lawn spread just acoustic capital hammer
            "0c63dcedf3db8c0ec49d0b8258dc9907afda3199a3dd1caa076f26dc8bf1393ad0b9fe5cbde5c34baca23a5330ac5422a561ba2f5022d91c903668e0efa7a7cc", 
            "xprv9s21ZrQH143K49HceMmdVV4wcdVtX32KGhH1PidSB6FjvQGvYkz9bgNcU2KTudTDVzuaUvwjSC4qTM9NYE9GfUYyHkKnSbPN4D2BWDVFW7o")]
        public void GenerateAccountFromXprv(string seed, string rootKEy)
        {
            IAccount<SampleWallet> accountFromSeed = new Account<SampleWallet>(new ExtKey(seed));
            IAccount<SampleWallet> accountFromWif = new Account<SampleWallet>(accountMasterKey: rootKEy);
            Assert.AreEqual(accountFromSeed.GetInternalWallet(0).PublicKeyBytes, accountFromWif.GetInternalWallet(0).PublicKeyBytes);
        }

        // mno -> m/0/354/0/0/0 -> public1
        // seed -> m/0/354/0 -> xprv
        // xprv -> 0/0 -> public1 
        [Test]
        public void GenerateFromXprvAndMatchWithMnemonic()
        {
            string mnemonic = "elevator myth eyebrow language again room trim link boss lawn spread just acoustic capital hammer";
            string seed = "0c63dcedf3db8c0ec49d0b8258dc9907afda3199a3dd1caa076f26dc8bf1393ad0b9fe5cbde5c34baca23a5330ac5422a561ba2f5022d91c903668e0efa7a7cc";
            string xprv_m_0_354_0 = "xprv9s21ZrQH143K2Ff1bkAY8RUYPsRdA1stC5x2SUhgJqHfvTZKE7kFjJcXY719DYQPPRJAVR4d6FNVBfFUH812jQJ2kKsGvGHoFUbwMD1q9yC"; // m/0/354/0

            // HD Wallet from mnemonic
            IHDWallet<SampleWallet> testHDWalletEd25519 = new TestHDWalletEd25519(mnemonic: mnemonic, "");
            Assert.AreEqual(((TestHDWalletEd25519)testHDWalletEd25519).BIP39Seed, seed);

            // Wallet m/0/354/0/0/0
            var expectedExternal0 = testHDWalletEd25519.GetAccount(0).GetExternalWallet(0); 

            // Derive account m/0'/354'/0' from seed
            ExtKey actualAccount0ExtKey = new ExtKey(seed).DerivePath("m/0'/354'/0'");
            IAccount<SampleWallet> actualAccount0 = new Account<SampleWallet>(actualAccount0ExtKey); // m/0/354/0

            // Wallet m/0/354/0/0/0
            var actualExternal0 = actualAccount0.GetExternalWallet(0); // m/0/354/0/0/0
            Assert.AreEqual(expectedExternal0.PublicKeyBytes, actualExternal0.PublicKeyBytes);

            // xprv for m/0/354/0
            string actualAccount0ExtKeyWif = actualAccount0ExtKey.GetWif();
            Assert.AreEqual(xprv_m_0_354_0, actualAccount0ExtKeyWif);

            // Account m/0/354/0 from xprv
            IAccount<SampleWallet> actualAccount0FromWif = new Account<SampleWallet>(xprv_m_0_354_0);
            
            // Wallet m/0/354/0/0/0 from xprv
            var actualExternal0FromWof = actualAccount0FromWif.GetExternalWallet(0); 
            Assert.AreEqual(expectedExternal0.PublicKeyBytes, actualExternal0FromWof.PublicKeyBytes);
        }
    }
}