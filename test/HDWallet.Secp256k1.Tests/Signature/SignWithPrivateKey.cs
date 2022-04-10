using System;
using System.Linq;
using System.Text;
using HDWallet.Secp256k1.Sample;
using NBitcoin;
using NBitcoin.Crypto;
using NBitcoin.DataEncoders;
using NUnit.Framework;

namespace HDWallet.Secp256k1.Tests.Signature
{
    public class SignWithPrivateKey
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("b80553c039bb8bac7414673b7eb5d9f6bb931572afb0618e141bc87b3906000e", "304402205483a10f49d4116b9aa82b72cb70ff975f13e6303d658510c1285be3c68a691202202451e97f55c7b004feccffa61242f1c02b0e65b76f95732aa31a00dd3c0dc2b2")]
        [TestCase("688787d8ff144c502c7f5cffaafe2cc588d86079f9de88304c26b0cb99ce91c6", "304302200d60b9383e1e6ae1801fa450498c9fbbac659deca086e712b661eefe112300ef021f2bcd044e1edd43764b3de2fee167954eb46c4506679a2924b76e78ef0dd6b0")]
        [TestCase("a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", "3044022049913f283c81ecc12800b6ab2c10445715705b0ee0473c7759f808e01de6d4fa022047685678bb287bd8a9e953a2dbafdacb1ca5cdc8915536ade731e86f9c106135")]
        [TestCase("e8f56862d74ef5599af4eeca73924bfa44a6773a497af0c29c48e18729ba6ff0", "304402204451a918d512be8926b875e3c1e63e21fdeb5233158e50009932b3c5af253f610220628550265ef4f18c14b32b5a41060153c9ac53f7b13154dac8a3b2693b8792cd")]
        [TestCase("489cd5dbc708c7e541de4d7cd91ce6d0f1613573b7fc5b40d3942ccb9555cf35", "304402200adfcbdfd436f9acd7ef0b6d363a403aa9f401bf4eede2826d158279cd0d5d6d022062123a6379937dac45e0c6573ae4b6c933cf92fbc1f414d76d9c340e927c3f9d")]
        [TestCase("657f18518eaa2f41307895e18c3ba0d12d97b8a23c6de3966f52c6ba39a07ee4", "304402206fd060f68ddebb2c8e86ca0c1e1b74df8b2f40d6c499b9c207e5f094c6e22cc702200c10f5a3136c24876a932abf4473f3997fed8c5d29eba8c4c75265ab700b8db5")]
        [TestCase("5e81ba0744f5c8537ea42de0e61ec4b676ece53469d0e1cf7495bc5e56126e6a", "304402206d296f902451688c9d0cef80293fb18ce42e13e38f51797d6e41f9ed615ae88f02206087a9ac2db726e4acc6f81170570b08d7a52a9654b66e896efec527c160718e")]
        [TestCase("9943b071e6ff7c75e9f4716fba01ba64e56ee45dc1e8e36c1da744801ef4c21b", "3044022020c25a19fb2398d5f99ba90f692807f17aa29b7a26a3fa0569055d3422e4abbb02201d67ee8cc52198953b88ef524736fc27c8b01a97a039b765fae3813b59a03cbb")]
        public void ShouldSignWithWallet(string message, string signatureDER)
        {
            Wallet wallet = new SampleSecp256k1Wallet("79afbf7147841fca72b45a1978dd7669470ba67abbe5c220062924380c9c364b");
            var compactSignature = wallet.Sign(Encoders.Hex.DecodeData(message)).ToCompact();

            var isParsed = ECDSASignature.TryParseFromCompact(compactSignature, out ECDSASignature ecdsaSignature);
            Assert.IsTrue(isParsed);

            var derSignature = ecdsaSignature.ToDER().ToHexString();
            Assert.AreEqual(expected: signatureDER, actual: derSignature);
        }


        [TestCase("b80553c039bb8bac7414673b7eb5d9f6bb931572afb0618e141bc87b3906000e", "304402205483a10f49d4116b9aa82b72cb70ff975f13e6303d658510c1285be3c68a691202202451e97f55c7b004feccffa61242f1c02b0e65b76f95732aa31a00dd3c0dc2b2")]
        [TestCase("688787d8ff144c502c7f5cffaafe2cc588d86079f9de88304c26b0cb99ce91c6", "304302200d60b9383e1e6ae1801fa450498c9fbbac659deca086e712b661eefe112300ef021f2bcd044e1edd43764b3de2fee167954eb46c4506679a2924b76e78ef0dd6b0")]
        [TestCase("a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", "3044022049913f283c81ecc12800b6ab2c10445715705b0ee0473c7759f808e01de6d4fa022047685678bb287bd8a9e953a2dbafdacb1ca5cdc8915536ade731e86f9c106135")]
        [TestCase("e8f56862d74ef5599af4eeca73924bfa44a6773a497af0c29c48e18729ba6ff0", "304402204451a918d512be8926b875e3c1e63e21fdeb5233158e50009932b3c5af253f610220628550265ef4f18c14b32b5a41060153c9ac53f7b13154dac8a3b2693b8792cd")]
        [TestCase("489cd5dbc708c7e541de4d7cd91ce6d0f1613573b7fc5b40d3942ccb9555cf35", "304402200adfcbdfd436f9acd7ef0b6d363a403aa9f401bf4eede2826d158279cd0d5d6d022062123a6379937dac45e0c6573ae4b6c933cf92fbc1f414d76d9c340e927c3f9d")]
        [TestCase("657f18518eaa2f41307895e18c3ba0d12d97b8a23c6de3966f52c6ba39a07ee4", "304402206fd060f68ddebb2c8e86ca0c1e1b74df8b2f40d6c499b9c207e5f094c6e22cc702200c10f5a3136c24876a932abf4473f3997fed8c5d29eba8c4c75265ab700b8db5")]
        [TestCase("5e81ba0744f5c8537ea42de0e61ec4b676ece53469d0e1cf7495bc5e56126e6a", "304402206d296f902451688c9d0cef80293fb18ce42e13e38f51797d6e41f9ed615ae88f02206087a9ac2db726e4acc6f81170570b08d7a52a9654b66e896efec527c160718e")]
        [TestCase("9943b071e6ff7c75e9f4716fba01ba64e56ee45dc1e8e36c1da744801ef4c21b", "3044022020c25a19fb2398d5f99ba90f692807f17aa29b7a26a3fa0569055d3422e4abbb02201d67ee8cc52198953b88ef524736fc27c8b01a97a039b765fae3813b59a03cbb")]
        public void ShouldSignWithTypedWallet(string message, string signatureDER)
        {
            SampleSecp256k1Wallet wallet = new SampleSecp256k1Wallet("79afbf7147841fca72b45a1978dd7669470ba67abbe5c220062924380c9c364b");
            Secp256k1Signature signature = wallet.Sign(Encoders.Hex.DecodeData(message));

            var isParsed = signature.TryGetECDSASignature(out ECDSASignature ecdsaSignature); Assert.IsTrue(isParsed);

            var derSignature = ecdsaSignature.ToDER().ToHexString();
            Assert.AreEqual(expected: signatureDER, actual: derSignature);
        }

        // [TestCase("b80553c039bb8bac7414673b7eb5d9f6bb931572afb0618e141bc87b3906000e")]
        // [TestCase("688787d8ff144c502c7f5cffaafe2cc588d86079f9de88304c26b0cb99ce91c6")]
        // [TestCase("a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3")]
        // [TestCase("e8f56862d74ef5599af4eeca73924bfa44a6773a497af0c29c48e18729ba6ff0")]
        // [TestCase("489cd5dbc708c7e541de4d7cd91ce6d0f1613573b7fc5b40d3942ccb9555cf35")]
        // [TestCase("657f18518eaa2f41307895e18c3ba0d12d97b8a23c6de3966f52c6ba39a07ee4")]
        // [TestCase("5e81ba0744f5c8537ea42de0e61ec4b676ece53469d0e1cf7495bc5e56126e6a")]
        // [TestCase("9943b071e6ff7c75e9f4716fba01ba64e56ee45dc1e8e36c1da744801ef4c21b")]
        // public void ShouldSignWithWalletUsingLegacy(string message)
        // {
        //     string privateKeyHex = "79afbf7147841fca72b45a1978dd7669470ba67abbe5c220062924380c9c364b";
        //     string wif = "L1JFfFEMQ7AZLpV7D5D3pE9MB8BqrR6fZvexzZ262kQjrod4yR8s";
            
        //     SampleSecp256k1Wallet wallet = new SampleSecp256k1Wallet(privateKeyHex);
        //     var messageHash = Encoders.Hex.DecodeData(message);
        //     var signature = wallet.SignLegacy(messageHash);
        //     var compactSignature = signature.ToCompact();

        //     var isParsed = ECDSASignature.TryParseFromCompact(compactSignature, out ECDSASignature ecdsaSignature);
        //     Assert.IsTrue(isParsed);

        //     Key PrivateKey = Key.Parse(wif, Network.Main); 
        //     var isVerified = PrivateKey.PubKey.Verify(new uint256(messageHash), ecdsaSignature);
        //     Assert.IsTrue(isVerified);
        // }

        // [Test]
        // public void SignWithECPrivKey()
        // {
        //     var privKeyStr = Encoders.Hex.DecodeData("cdce32b32436ff20c2c32ee55cd245a82fff4c2dc944da855a9e0f00c5d889e4"); 
        //     Key key = new Key(privKeyStr);
        //     NBitcoin.Secp256k1.ECPrivKey privKey = Context.Instance.CreateECPrivKey(new Scalar(key.ToBytes()));

        //     var messageToSign = "159817a085f113d099d3d93c051410e9bfe043cc5c20e43aa9a083bf73660145";
        //     var messageBytes = Encoders.Hex.DecodeData(messageToSign);

        //     privKey.TrySignRecoverable(messageBytes, out SecpRecoverableECDSASignature sigRec);
        //     var (r, s, v) = sigRec;

        //     var R = r.ToBytes().ToHexString();
        //     var S = s.ToBytes().ToHexString();

        //     Assert.AreEqual("84de8230e66c6507dea6de6d925c76ac0db85d99ddd3c069659d0970ade8876a", R);
        //     Assert.AreEqual("0dcd4adb2e40fcf257da419b88c1e7dd4d92750b63381d8379b96f3b7b8a4498", S);
        //     Assert.AreEqual(1, v);
        // }

        // [Test]
        // public void MultipleSignatureWays()
        // {
            // var privKeyStr = Encoders.Hex.DecodeData("8e812436a0e3323166e1f0e8ba79e19e217b2c4a53c970d4cca0cfb1078979df"); 
            // Key key = new Key(privKeyStr);
            // Assert.AreEqual("04a5bb3b28466f578e6e93fbfd5f75cee1ae86033aa4bbea690e3312c087181eb366f9a1d1d6a437a9bf9fc65ec853b9fd60fa322be3997c47144eb20da658b3d1", key.PubKey.Decompress().ToHex());

            // var messageToSign = "159817a085f113d099d3d93c051410e9bfe043cc5c20e43aa9a083bf73660145";
            // var messageBytes = Encoders.Hex.DecodeData(messageToSign);

            // ECDSASignature signature = key.Sign(new uint256(messageBytes), true);
            // SecpECDSASignature.TryCreateFromDer(signature.ToDER(), out SecpECDSASignature sig);
            // var (r,s) = sig;

            // var R = r.ToBytes();
            // var S = s.ToBytes();

            // Assert.AreEqual("38b7dac5ee932ac1bf2bc62c05b792cd93c3b4af61dc02dbb4b93dacb758123f", R.ToHexString());
            // Assert.AreEqual("08bf123eabe77480787d664ca280dc1f20d9205725320658c39c6c143fd5642d", S.ToHexString());

            // Compact signature
            // CompactSignature signatureCompact = key.SignCompact(new uint256(messageBytes), true);
            // int recid = signatureCompact.RecoveryId;
            // if ( ! (
            //     SecpRecoverableECDSASignature.TryCreateFromCompact(signatureCompact.Signature, recid, out SecpRecoverableECDSASignature sigR) && sigR is SecpRecoverableECDSASignature
            //     ) 
            // )
			// {
            //     throw new InvalidOperationException("Impossible to recover the public key");
			// }

            // // V from comapct signature
            // var (r1, s1, v1) = sigR;

            // Assert.AreEqual(v1, 0);

            // // Recoverable signature with Secp256k1 lib
            // NBitcoin.Secp256k1.ECPrivKey privKey = Context.Instance.CreateECPrivKey(new Scalar(key.ToBytes()));
            // Assert.AreEqual(key.PubKey.ToBytes(), privKey.CreatePubKey().ToBytes());

            // privKey.TrySignRecoverable(messageBytes, out SecpRecoverableECDSASignature sigRec);

            // var (r2, s2, v2) = sigRec;

            // Assert.AreEqual(r2, r);
            // Assert.AreEqual(s2, s);
            // Assert.AreEqual(v2, v1);
        // }
    }

    public static class Helper
    {
        // From NBitcoin
        public static byte[] Concat(byte[] arr, params byte[][] arrs)
		{
			var len = arr.Length + arrs.Sum(a => a.Length);
			var ret = new byte[len];
			Buffer.BlockCopy(arr, 0, ret, 0, arr.Length);
			var pos = arr.Length;
			foreach (var a in arrs)
			{
				Buffer.BlockCopy(a, 0, ret, pos, a.Length);
				pos += a.Length;
			}
			return ret;
		}

        public static string ToHexString(this byte[] bytes)
        {
            var hex = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }


        public static byte[] FromHexToByteArray(this string input)
        {
            var numberChars = input.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(input.Substring(i, 2), 16);
            }
            return bytes;
        }
        
    }
}