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

using System.Security.Cryptography;

namespace HDWallet.BIP32.Ed25519
{
    public class Key 
    {
        readonly byte[] _privateKey;
        public byte[] PrivateKey => _privateKey;

        public byte[] PublicKey => GetPublicKey();

        public Key(byte[] key)
        {
            _privateKey = key;
        }

        public byte[] GetPublicKey(bool withZeroByte = true)
        {
            Chaos.NaCl.Ed25519.KeyPairFromSeed(out var publicKey, out _, _privateKey);

            var zero = new byte[] { 0 };

            var buffer = new BigEndianBuffer();
            if (withZeroByte)
                buffer.Write(zero);

            buffer.Write(publicKey);

            return buffer.ToArray();
        }

        public byte[] GetExpandedPrivateKey()
        {
            Chaos.NaCl.Ed25519.KeyPairFromSeed(out _, out var expandedPrivateKey, _privateKey);

            var buffer = new BigEndianBuffer();

            buffer.Write(expandedPrivateKey);
            return buffer.ToArray();
        }

        public (byte[] key, byte[] ccChild) Derivate(byte[] cc, uint nChild)
        {
            BigEndianBuffer buffer = new BigEndianBuffer();

            buffer.Write(new byte[] { 0 });
            buffer.Write(this.PrivateKey);
            buffer.WriteUInt(nChild);

            using (HMACSHA512 hmacSha512 = new HMACSHA512(cc))
            {
                var i = hmacSha512.ComputeHash(buffer.ToArray());

                var il = i.Slice(0, 32);
                var ir = i.Slice(32);

                return (key: il, ccChild: ir);
            }
        }
    }
}