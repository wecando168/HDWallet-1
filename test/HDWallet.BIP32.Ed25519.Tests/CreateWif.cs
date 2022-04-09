//    Copyright 2017 elucidsoft

//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at

//        http://www.apache.org/licenses/LICENSE-2.0

//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using System;
using System.IO;
using System.Runtime.InteropServices;
using HDWallet.BIP32.Ed25519;
using NUnit.Framework;

namespace HDWallet.BIP32.Ed25519.Tests
{
    public class CreateWif
    {
        [TestCase(
            "000102030405060708090a0b0c0d0e0f", 
            "xprv9s21ZrQH143K3VX7GQTkxonbgca94bts9EdRC1ZKN2Z9BA3JXZxbUwo4kS28ECmXhK1NicjQ7yBwWbZXgjRVktP6Tzi4YqetK5ueSA2CaXP")]
        [TestCase(
            "fffcf9f6f3f0edeae7e4e1dedbd8d5d2cfccc9c6c3c0bdbab7b4b1aeaba8a5a29f9c999693908d8a8784817e7b7875726f6c696663605d5a5754514e4b484542", 
            "xprv9s21ZrQH143K4Sd1z5fLT9D6CsVWg33mA1TDfKPzKavYR47H1cJaX16paqoyUuw3g1Zm6GHruGNpXqdVk8BVoZ8bLE3DYQpudN4C9H391kJ")]
        public void ShouldCreateWif(string seed, string wifExpected)
        {
            ExtKey testMasterKeyFromSeed = new ExtKey(seed);
            string xprv = testMasterKeyFromSeed.GetWif(NBitcoin.Network.Main);
            Assert.AreEqual(expected: wifExpected, actual: xprv);
        }

        [TestCase(
            "xprv9s21ZrQH143K3VX7GQTkxonbgca94bts9EdRC1ZKN2Z9BA3JXZxbUwo4kS28ECmXhK1NicjQ7yBwWbZXgjRVktP6Tzi4YqetK5ueSA2CaXP",
            "2b4be7f19ee27bbf30c667b642d5f4aa69fd169872f8fc3059c08ebae2eb19e7", 
            "90046a93de5380a72b5e45010748567d5ea02bbf6522f979e05c0d8d8ca9fffb")]
        [TestCase(
            "xprv9s21ZrQH143K4Sd1z5fLT9D6CsVWg33mA1TDfKPzKavYR47H1cJaX16paqoyUuw3g1Zm6GHruGNpXqdVk8BVoZ8bLE3DYQpudN4C9H391kJ",
            "171cb88b1b3c1db25add599712e36245d75bc65a1a5c9e18d76f9f2b1eab4012", 
            "ef70a74db9c3a5af931b5fe73ed8e1a53464133654fd55e7a66f8570b8e33c3b")]
        public void ShouldCreateFromWif(string wif, string keyHexExpected, string chainCodeExpected)
        {
            ExtKey testMasterKeyFromWif = ExtKey.CreateFromWif(wif);
            Assert.AreEqual(keyHexExpected, testMasterKeyFromWif.Key.PrivateKey.ToStringHex());
            Assert.AreEqual(chainCodeExpected, testMasterKeyFromWif.ChainCode.ToStringHex());
        }
    }
}