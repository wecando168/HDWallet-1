using HDWallet.Core;
using Neo;
using Neo.Cryptography.ECC;
using Neo.Wallets;

namespace HDWallet.Neo
{
    public class NeoAddressGenerator : IAddressGenerator
    {
        public string GenerateAddress(ECPoint pubKey, byte version)
        {
            UInt160 scriptHash = Contract.CreateSignatureContract(pubKey).ScriptHash;
            string address = scriptHash.ToAddress(version);

            return address;
        }

        string IAddressGenerator.GenerateAddress(byte[] pubKeyBytes)
        {
            var pubKey = ECPoint.FromBytes(pubKeyBytes, ECCurve.Secp256r1);
            UInt160 scriptHash = Contract.CreateSignatureContract(pubKey).ScriptHash;
            string address = scriptHash.ToAddress(ProtocolSettings.Default.AddressVersion);

            return address;
        }
    }
}