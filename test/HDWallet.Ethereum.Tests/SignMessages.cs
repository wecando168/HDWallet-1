using HDWallet.Core;
using HDWallet.Secp256k1;
using NBitcoin;
using NBitcoin.Crypto;
using NBitcoin.DataEncoders;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using NUnit.Framework;
using System;
using System.Text;

namespace HDWallet.Ethereum.Tests
{
    public class SignMessages
    {
        private const string mnemonic = "rapid apart clip require dragon property hurry ensure coil ship torch include squirrel jewel window";

        [Test]
        public void ShouldSignAMessage()
        {
            string privateKey = "79afbf7147841fca72b45a1978dd7669470ba67abbe5c220062924380c9c364b";
            
            var messageToSign = "Message for ECDSA signing";
            var messageBytes = Encoding.UTF8.GetBytes(messageToSign);
            var sha256 =  NBitcoin.Crypto.Hashes.SHA256(messageBytes);

            // Sign with HdWallet.Ethereum
            EthereumWallet wallet = new EthereumWallet(privateKey);
            EthereumSignature walletSig = wallet.Sign(sha256);
            var signatureWithHdWallet = walletSig.SignatureHex; 
            Assert.AreEqual(signatureWithHdWallet, "0x7de4f9175ba7cebc11db681ff5e1dd43de8f3fbca3744f743223b0681b4ee60e2e9a181cd6456762a178841f2a133e8db073f8b7880ae33fe016b8fd6d9758131c");   
        }

        [Test]
        public void ShouldSignWithHdWallet()
        {
            var messageToSign = "Message for ECDSA signing";
            var messageBytes = Encoding.UTF8.GetBytes(messageToSign);
            var sha256 =  NBitcoin.Crypto.Hashes.SHA256(messageBytes);

            IHDWallet<EthereumWallet> hdWallet = new EthereumHDWallet(mnemonic: mnemonic, passphrase : "");
            EthereumWallet wallet = hdWallet.GetAccount(0).GetExternalWallet(0);

            var signatureWithHdWallet = wallet.Sign(sha256).SignatureHex;
            Assert.AreEqual("0x605f721301fe2e817f639babacfa91855254d1e5ba0ff5d93ba19ae30f599eb735c0d9f4e42824da188718c278e9ccd8fdd40068e6051ebf06a69590a8db9d511b", signatureWithHdWallet);
        }

        [Test]
        public void ShouldVerify()
        {
            var messageToSign = "Message for ECDSA signing";
            var messageBytes = Encoding.UTF8.GetBytes(messageToSign);
            var sha256 =  NBitcoin.Crypto.Hashes.SHA256(messageBytes);

            IHDWallet<EthereumWallet> hdWallet = new EthereumHDWallet(mnemonic: mnemonic, passphrase : "");
            IWallet wallet = hdWallet.GetAccount(0).GetExternalWallet(0);

            // Sign with HdWallet
            var signature = wallet.Sign(sha256);

            // Verify with HdWallet
            var isVerified = wallet.Verify(sha256, signature);
            Assert.IsTrue(isVerified);

            // Verify with Nethereum
            var key = new EthECKey(wallet.PrivateKeyBytes, isPrivate: true);
            var signatureFromComp = EthECDSASignatureFactory.FromComponents(signature.R, signature.S);
            isVerified = key.Verify(sha256, signatureFromComp);
            Assert.True(isVerified);

            // Verify with Pubkey using Nethererum
            var pubKey = new EthECKey(wallet.PublicKeyBytes, isPrivate: false);
            isVerified = pubKey.Verify(sha256, signatureFromComp);
            Assert.True(isVerified);

            // Verify with NBitcoin pubkey
            PubKey.TryCreatePubKey(wallet.PublicKeyBytes, out PubKey nBitcoinPubKey);

            var isParsed = new EthereumSignature(signature).TryGetECDSASignature(out ECDSASignature ecdsaSignature);
            Assert.IsTrue(isParsed);

            isVerified = nBitcoinPubKey.Verify(new uint256(sha256), ecdsaSignature);
            Assert.True(isVerified);
        }

        [Test]
        public void ShouldVerifyWithNethereum()
        {
            var messageToSign = "Message for ECDSA signing";
            var messageBytes = Encoding.UTF8.GetBytes(messageToSign);
            var sha256 =  NBitcoin.Crypto.Hashes.SHA256(messageBytes);

            IHDWallet<EthereumWallet> hdWallet = new EthereumHDWallet(mnemonic: mnemonic, passphrase : "");
            EthereumWallet wallet = hdWallet.GetAccount(0).GetExternalWallet(0);

            // Sign with HdWallet
            var signature = wallet.Sign(sha256);

            // Verify with Nethereum
            var privKey = new EthECKey(wallet.PrivateKeyBytes, isPrivate: true);
            var isVerified = privKey.Verify(sha256, EthECDSASignatureFactory.FromComponents(signature.R, signature.S));
            Assert.True(isVerified);
        }

        [Test]
        public void ShouldVerifyWithPubkeyUsingNethereum()
        {
            var messageToSign = "Message for ECDSA signing";
            var messageBytes = Encoding.UTF8.GetBytes(messageToSign);
            var sha256 =  NBitcoin.Crypto.Hashes.SHA256(messageBytes);

            IHDWallet<EthereumWallet> hdWallet = new EthereumHDWallet(mnemonic: mnemonic, passphrase : "");
            EthereumWallet wallet = hdWallet.GetAccount(0).GetExternalWallet(0);

            // Sign with HdWallet
            var signature = wallet.Sign(sha256);
            var signatureFromComp = EthECDSASignatureFactory.FromComponents(signature.R, signature.S);

            // Verify with Pubkey using Nethererum
            var pubKey = new EthECKey(wallet.PublicKey.ToBytes(), isPrivate: false);
            var isVerified = pubKey.Verify(sha256, signatureFromComp);
            Assert.True(isVerified);
        }

        [Test]
        public void ShouldVerifyWithPubkeyUsingNBitcoin()
        {
            var messageToSign = "Message for ECDSA signing";
            var messageBytes = Encoding.UTF8.GetBytes(messageToSign);
            var sha256 =  NBitcoin.Crypto.Hashes.SHA256(messageBytes);

            IHDWallet<EthereumWallet> hdWallet = new EthereumHDWallet(mnemonic: mnemonic, passphrase : "");
            EthereumWallet wallet = hdWallet.GetAccount(0).GetExternalWallet(0);

            // Sign with HdWallet
            var signature = wallet.Sign(sha256);

            // Verify with pubkey using NBitcoin 
            var nBitcoinPubKey = wallet.PublicKey;

            var isParsed = signature.TryGetECDSASignature(out ECDSASignature ecdsaSignature);
            Assert.IsTrue(isParsed);

            var isVerified = nBitcoinPubKey.Verify(new uint256(sha256), ecdsaSignature);
            Assert.True(isVerified);
        }

        [Test]
        public void ShouldSignSameWithNethereum()
        {
            var messageToSign = "Message for ECDSA signing";
            var messageBytes = Encoding.UTF8.GetBytes(messageToSign);
            var sha256 =  NBitcoin.Crypto.Hashes.SHA256(messageBytes);

            // Generate private key with HdWallet
            IHDWallet<EthereumWallet> hdWallet = new EthereumHDWallet(mnemonic: mnemonic, passphrase : "");
            string privateKey = hdWallet.GetAccount(0).GetExternalWallet(0).PrivateKeyBytes.ToHexString();

            // Reference value
            EthECDSASignature ethCompactSignature = new MessageSigner().SignAndCalculateV(sha256, privateKey);
            string signatureWithNeth = EthECDSASignature.CreateStringSignature(ethCompactSignature); 
            
            // Sign with HdWallet.Ethereum
            EthereumWallet wallet = new EthereumWallet(privateKey);
            var signatureWithHdWallet = wallet.Sign(sha256).SignatureHex;
            Assert.AreEqual(signatureWithNeth, signatureWithHdWallet);
        }

        [Test]
        public void Compare()
        {   
            string privateKey = "79afbf7147841fca72b45a1978dd7669470ba67abbe5c220062924380c9c364b";
            
            var messageToSign = "Message for ECDSA signing";
            var messageBytes = Encoding.UTF8.GetBytes(messageToSign);
            var sha256 =  NBitcoin.Crypto.Hashes.SHA256(messageBytes);
            // var sha256 = "9943b071e6ff7c75e9f4716fba01ba64e56ee45dc1e8e36c1da744801ef4c21b".FromHexToByteArray(); // TODO: Test this

            // Sign with Nethereum
            var ethereumSigner = new MessageSigner();
            EthECDSASignature ethCompactSignature = ethereumSigner.SignAndCalculateV(sha256, privateKey);
            var r = ethCompactSignature.R.ToHex().PadLeft(64, '0'); Assert.AreEqual(r, "7de4f9175ba7cebc11db681ff5e1dd43de8f3fbca3744f743223b0681b4ee60e");
            var s = ethCompactSignature.S.ToHex().PadLeft(64, '0'); Assert.AreEqual(s, "2e9a181cd6456762a178841f2a133e8db073f8b7880ae33fe016b8fd6d975813");
            var v = ethCompactSignature.V.ToHex(); Assert.AreEqual(v, "1c");

            string signatureWithNeth = EthECDSASignature.CreateStringSignature(ethCompactSignature); 
            Assert.AreEqual(signatureWithNeth, "0x7de4f9175ba7cebc11db681ff5e1dd43de8f3fbca3744f743223b0681b4ee60e2e9a181cd6456762a178841f2a133e8db073f8b7880ae33fe016b8fd6d9758131c");

            // Sign with NBitcoin
            Key key = PrivateKeyParse(privateKey);
            var nbitcoinSignature = key.Sign(new uint256(sha256));
            Assert.AreEqual(nbitcoinSignature.ToCompact().ToHexString(), "7de4f9175ba7cebc11db681ff5e1dd43de8f3fbca3744f743223b0681b4ee60e2e9a181cd6456762a178841f2a133e8db073f8b7880ae33fe016b8fd6d975813");

            CompactSignature nbitcoinCompactSignature = key.SignCompact(new uint256(sha256));
            var compactSignature = nbitcoinCompactSignature.Signature.ToHex(); 
            Assert.AreEqual(compactSignature, "7de4f9175ba7cebc11db681ff5e1dd43de8f3fbca3744f743223b0681b4ee60e2e9a181cd6456762a178841f2a133e8db073f8b7880ae33fe016b8fd6d975813");
            var recId = nbitcoinCompactSignature.RecoveryId;
            Assert.AreEqual(recId, 1);

            var signatureWithNBitcoin = "0x" + compactSignature + new[] {(byte) (recId + 27)}.ToHex();
            Assert.AreEqual(signatureWithNBitcoin, "0x7de4f9175ba7cebc11db681ff5e1dd43de8f3fbca3744f743223b0681b4ee60e2e9a181cd6456762a178841f2a133e8db073f8b7880ae33fe016b8fd6d9758131c");

            // Sign with HdWallet.Ethereum
            EthereumWallet wallet = new EthereumWallet(privateKey);
            EthereumSignature walletSig = wallet.Sign(sha256);
            var signatureWithHdWallet = walletSig.SignatureHex; 
            Assert.AreEqual(signatureWithHdWallet, "0x7de4f9175ba7cebc11db681ff5e1dd43de8f3fbca3744f743223b0681b4ee60e2e9a181cd6456762a178841f2a133e8db073f8b7880ae33fe016b8fd6d9758131c");

            Assert.AreEqual(signatureWithHdWallet, signatureWithNBitcoin);
            Assert.AreEqual(signatureWithHdWallet, signatureWithNeth);
        }

        private Key PrivateKeyParse(string privateKey)
        {
            byte[] privKeyPrefix = new byte[] { (128) };
            byte[] prefixedPrivKey = Helper.Concat(privKeyPrefix, Encoders.Hex.DecodeData(privateKey));

            byte[] privKeySuffix = new byte[] { (1) };
            byte[] suffixedPrivKey = Helper.Concat(prefixedPrivKey, privKeySuffix);

            Base58CheckEncoder base58Check = new Base58CheckEncoder();
            string privKeyEncoded = base58Check.EncodeData(suffixedPrivKey);
            return Key.Parse(privKeyEncoded, Network.Main);
        }
    }
}