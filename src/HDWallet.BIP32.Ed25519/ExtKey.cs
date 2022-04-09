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
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using NBitcoin;

namespace HDWallet.BIP32.Ed25519
{
    public class ExtKey
    {
        public byte[] ChainCode { get; private set; }
        public Key Key { get; private set; }

        public ExtKey(string seed)
        {
            var masterKeyFromSeed = GetMasterKeyFromSeed(seed);
            
            Key = new Key(masterKeyFromSeed.Key);
            ChainCode = masterKeyFromSeed.ChainCode;
        }

        public ExtKey(byte[] key, byte[] chainCode)
        {
            Key = new Key(key);
            ChainCode = chainCode;
        }

        readonly string curve = "ed25519 seed";
        readonly uint hardenedOffset = 0x80000000;

        private (byte[] Key, byte[] ChainCode) GetMasterKeyFromSeed(string seed)
        {
            using (HMACSHA512 hmacSha512 = new HMACSHA512(Encoding.UTF8.GetBytes(curve)))
            {
                var i = hmacSha512.ComputeHash(seed.HexToByteArray());

                var il = i.Slice(0, 32);
                var ir = i.Slice(32);

                return (Key: il, ChainCode: ir);
            }
        }

        private bool IsValidPath(string path)
        {
            var regex = new Regex("^m(\\/[0-9]+')+$");

            if (!regex.IsMatch(path))
                return false;

            var valid = !(path.Split('/')
                .Slice(1)
                .Select(a => a.Replace("'", ""))
                .Any(a => !Int32.TryParse(a, out _)));

            return valid;
        }


        public ExtKey DerivePath(string path)
        {
            if (!IsValidPath(path))
                throw new FormatException("Invalid derivation path");

            var segments = path
                .Split('/')
                .Slice(1)
                .Select(a => a.Replace("'", ""))
                .Select(a => Convert.ToUInt32(a, 10));

            var results = segments
                .Aggregate(this, (mks, next) => mks.Derive(next));

            return results;
        }

        public ExtKey Derive(uint index)
		{
			(var childkey, var childcc)  = this.Key.Derivate(this.ChainCode, index + hardenedOffset);
			return new ExtKey(childkey, childcc);
		}


        /// <summary>
		/// Converts the extended key to the base58 representation for Mainnet.
		/// </summary>
		public string GetWif()
        {
            return GetWif(Network.Main);
        }

        /// <summary>
		/// Converts the extended key to the base58 representation, within the specified network.
		/// </summary>
		public string GetWif(Network network)
		{
			BitcoinExtKey bitcoinExtKey = new BitcoinExtKey(
                new NBitcoin.ExtKey(
                    new NBitcoin.Key(
                        this.Key.PrivateKey
                    ), 
                    this.ChainCode), 
                network);

            return bitcoinExtKey.ToWif();
		}

        public static ExtKey CreateFromWif(string base58)
        {
            return CreateFromWif(base58, Network.Main);
        }
        
        public static ExtKey CreateFromWif(string base58, Network expectedNetwork)
        {
            var parsed = new BitcoinExtKey(base58, expectedNetwork).ExtKey;
            ExtKey extKey = new ExtKey(parsed.PrivateKey.ToBytes(), parsed.ChainCode);

            return extKey;
        }
    }
}