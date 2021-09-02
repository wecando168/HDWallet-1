using Neo;
using Neo.Cryptography;
using System;
using System.IO;
using System.Text;

namespace HDWallet.Neo
{
    public static class NeoHelper
    {
        /// <summary>
        /// Converts an <see cref="ISerializable"/> object to a byte array.
        /// </summary>
        /// <param name="value">The <see cref="ISerializable"/> object to be converted.</param>
        /// <returns>The converted byte array.</returns>
        public static byte[] ToArray(this ISerializable value)
        {
            using MemoryStream ms = new();
            using BinaryWriter writer = new(ms, Utility.StrictUTF8, true);
            value.Serialize(writer);
            writer.Flush();
            return ms.ToArray();
        }

        /// <summary>
        /// Converts a byte array to hex <see cref="string"/>.
        /// </summary>
        /// <param name="value">The byte array to convert.</param>
        /// <param name="reverse">Indicates whether it should be converted in the reversed byte order.</param>
        /// <returns>The converted hex <see cref="string"/>.</returns>
        public static string ToHexString(this byte[] value, bool reverse = false)
        {
            StringBuilder sb = new();
            for (int i = 0; i < value.Length; i++)
                sb.AppendFormat("{0:x2}", value[reverse ? value.Length - i - 1 : i]);
            return sb.ToString();
        }

        /// <summary>
        /// Computes the hash of the specified script.
        /// </summary>
        /// <param name="script">The specified script.</param>
        /// <returns>The hash of the script.</returns>
        public static UInt160 ToScriptHash(this byte[] script)
        {
            return new UInt160(Crypto.Hash160(script));
        }

        /// <summary>
        /// Computes the hash of the specified script.
        /// </summary>
        /// <param name="script">The specified script.</param>
        /// <returns>The hash of the script.</returns>
        public static UInt160 ToScriptHash(this ReadOnlySpan<byte> script)
        {
            return new UInt160(Crypto.Hash160(script));
        }
    }
}
