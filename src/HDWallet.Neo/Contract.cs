using Neo;
using Neo.Cryptography.ECC;
using Neo.SmartContract;
using Neo.VM;
using Helper = HDWallet.Core.Helper;

namespace HDWallet.Neo
{
    public class Contract
    {
        /// <summary>
        /// The script of the contract.
        /// </summary>
        public byte[] Script;

        /// <summary>
        /// The parameters of the contract.
        /// </summary>
        public ContractParameterType[] ParameterList;

        private UInt160 _scriptHash;
        /// <summary>
        /// The hash of the contract.
        /// </summary>
        public virtual UInt160 ScriptHash
        {
            get
            {
                if (_scriptHash == null)
                {
                    _scriptHash = Script.ToScriptHash();
                }
                return _scriptHash;
            }
        }

        /// <summary>
        /// Creates a signature contract.
        /// </summary>
        /// <param name="publicKey">The public key of the contract.</param>
        /// <returns>The created contract.</returns>
        public static Contract CreateSignatureContract(ECPoint publicKey)
        {
            return new Contract
            {
                Script = CreateSignatureRedeemScript(publicKey),
                ParameterList = new[] { ContractParameterType.Signature }
            };
        }

        /// <summary>
        /// Creates the script of signature contract.
        /// </summary>
        /// <param name="publicKey">The public key of the contract.</param>
        /// <returns>The created script.</returns>
        public static byte[] CreateSignatureRedeemScript(ECPoint publicKey)
        {
            using ScriptBuilder sb = new();
            sb.EmitPush(publicKey.EncodePoint(true));
            sb.EmitSysCall(ApplicationEngine.System_Crypto_CheckSig);
            return sb.ToArray();
        }
    }
}
