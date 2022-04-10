using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HDWallet.Core;
using NBitcoin;

namespace HDWallet.Stacks
{
    public enum NetworkVersion
    {
        /// <summary>
        /// MainnetSingleSig
        /// </summary>
        Mainnet = 22,
        
        /// <summary>
        /// TestnetSingleSig
        /// </summary>
        Testnet = 26
    }

    public class AddressGenerator : IAddressGenerator
    {
        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            return GenerateAddress(new PubKey(pubKeyBytes), NetworkVersion.Mainnet);
        }

        string GenerateAddress(PubKey pubKey, NetworkVersion networkVersion)
        {
            var minLength = -1;
            var publicKey = pubKey.Decompress();
            var pubKeyBytes = publicKey.ToBytes();

            var version = (int)networkVersion;
            var versionHex = Convert.ToString(version, 16);

            var pubKeyHash160BytesSha256 = NBitcoin.Crypto.Hashes.SHA256(pubKeyBytes);
            var pubKeyHash160Bytes = NBitcoin.Crypto.Hashes.RIPEMD160(pubKeyHash160BytesSha256, pubKeyHash160BytesSha256.Length);
            var pubKeyHash160Hex = pubKeyHash160Bytes.ToHexString();

            var hash160Regex = new Regex(@"/^[0-9a-fA-F]{40}$/");
            if (hash160Regex.Match(pubKeyHash160Hex).Length > 0)
            {
                throw new ArgumentException("Invalid argument: not a hash160 hex string");
            }

            var checksumInputHex = $"{versionHex}{pubKeyHash160Hex}";
            var checksumInputBytes = checksumInputHex.FromHexToByteArray();
            
            if (version < 0 || version >= 32)
            {
                throw new ArgumentOutOfRangeException("Invalid version (must be between 0 and 31)");
            }
            
            if (versionHex.Length == 1) {
                versionHex = $"0{versionHex}";
            }

            var checksumRegex = new Regex(@"/^[0-9a-fA-F]*$/");
            if (checksumRegex.Match(checksumInputHex).Length > 0)
            {
                throw new ArgumentException("Invalid data (not a hex string)");
            }
            
            var doubleHashedChecksumInputBytes = NBitcoin.Crypto.Hashes.SHA256(NBitcoin.Crypto.Hashes.SHA256(checksumInputBytes));
            var checksumHex = doubleHashedChecksumInputBytes.Take(4).ToArray().ToHexString();
            
            var inputHex = $"{pubKeyHash160Hex}{checksumHex}";
            var inputBytes = inputHex.FromHexToByteArray();

            inputHex = inputHex.ToLower();

            var inputHexRegex = new Regex(@"/^[0-9a-fA-F]*$/");
            if (inputHexRegex.Match(inputHex).Length > 0)
            {
                throw new ArgumentException("Not a hex-encoded string");
            }
            
            if (inputHex.Length % 2 != 0)
            {
                inputHex = $"0{inputHex}";
            }

            var c32 = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";
            var hex = "0123456789abcdef";
            var res = new List<char>();
            var carry = 0;

            for (int i = inputHex.Length - 1; i >= 0; i--)
            {
                if (carry < 4)
                {
                    var currentCode = hex.IndexOf(inputHex[i]) >> carry;
                    var nextCode = 0;
                    if (i != 0)
                    {
                        nextCode = hex.IndexOf(inputHex[i - 1]);
                    }
                    // carry = 0, nextBits is 1, carry = 1, nextBits is 2
                    var nextBits = 1 + carry;
                    var nextLowBits = (nextCode % (1 << nextBits)) << (5 - nextBits);
                    var curC32Digit = c32[currentCode + nextLowBits];
                    carry = nextBits;
                    res.Insert(0, curC32Digit);
                } else
                {
                    carry = 0;
                }
            }

            var C32leadingZeros = 0;

            for (int i = 0; i < res.Count; i++)
            {
                if (res[i] != '0')
                {
                    break;
                }

                C32leadingZeros++;
            }

            res = res.Skip(C32leadingZeros).ToList();
            var rx = new Regex(@"/^\u0000*/");
            var zeroPrefix = rx.Matches(inputHex);
            var numLeadingZeroBytesInHex = zeroPrefix.Count == 0 ? 0 : zeroPrefix[0].Length;

            for (int i = 0; i < numLeadingZeroBytesInHex; i++)
            {
                res.Insert(0, c32[0]);
            }

            var address = $"S{c32[version]}{string.Join("", res)}".ToString();

            if (minLength > 0)
            {
                var count = minLength - res.Count;
                for (var i = 0; i < count; i++)
                {
                    res.Insert(0, c32[0]);
                }
            }

            return address;
        }

        public string GenerateAddress(byte[] pubKeyBytes, NetworkVersion networkVersion)
        {
            return GenerateAddress(new PubKey(pubKeyBytes), networkVersion);
        }
    }
}