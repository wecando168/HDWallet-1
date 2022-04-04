using System;
using HDWallet.Core;

namespace HDWallet.Algorand
{
    public class AddressGenerator : IAddressGenerator
    {
        public string GenerateAddress(byte[] pubKeyBytes)
        {
            if(pubKeyBytes.Length != 32) 
            {
                throw new ArgumentException("PublicKey should be 32 bytes");
            }

            // Note: Official Go-SDK uses SHA512_256 checksum while building address string. 
            var hashFn = SharpHash.Base.HashFactory.Crypto.CreateSHA2_512_256();
            var sha256 = hashFn.ComputeBytes(pubKeyBytes).GetBytes();

            byte[] rv = new byte[32 + 4];
            System.Buffer.BlockCopy(pubKeyBytes, 0, rv, 0, 32);
            System.Buffer.BlockCopy(sha256, 28, rv, 32, 4);

            return SimpleBase.Base32.Rfc4648.Encode(rv, false);
        }
    }
}