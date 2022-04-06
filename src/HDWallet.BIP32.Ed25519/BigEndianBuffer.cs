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
using System.Collections.Generic;

namespace HDWallet.BIP32.Ed25519
{
    public class BigEndianBuffer
    {
        readonly List<byte> _bytes = new List<byte>();

        public void WriteUInt(uint i)
        {
            _bytes.Add((byte)((i >> 0x18) & 0xff));
            _bytes.Add((byte)((i >> 0x10) & 0xff));
            _bytes.Add((byte)((i >> 8) & 0xff));
            _bytes.Add((byte)(i & 0xff));
        }

        public void Write(byte b)
        {
            _bytes.Add(b);
        }

        public void Write(byte[] bytes)
        {
            Write(bytes, 0, bytes.Length);
        }

        public void Write(byte[] bytes, int offset, int count)
        {
            var newBytes = new byte[count];
            Array.Copy(bytes, offset, newBytes, 0, count);

            _bytes.AddRange(newBytes);
        }

        public byte[] ToArray()
        {
            return _bytes.ToArray();
        }
    }
}
