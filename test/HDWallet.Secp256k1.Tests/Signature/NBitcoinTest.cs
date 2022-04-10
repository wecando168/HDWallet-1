using System;
using NBitcoin;
using NBitcoin.Crypto;
using NBitcoin.DataEncoders;
using NUnit.Framework;

namespace Test
{
    public class NBitcoinTest
    {
        readonly Key PrivateKey = Key.Parse("L1JFfFEMQ7AZLpV7D5D3pE9MB8BqrR6fZvexzZ262kQjrod4yR8s", Network.Main); // 79afbf7147841fca72b45a1978dd7669470ba67abbe5c220062924380c9c364b

        public byte[] Sign(byte[] message)
        {
            // ECPrivKey privKey = Context.Instance.CreateECPrivKey(PrivateKey.ToBytes());
            // privKey.TrySignECDSA(message, out SecpECDSASignature sigRec);
            ECDSASignature sigRec = PrivateKey.Sign(new uint256(message));
            return sigRec.ToDER();
        }

        public byte[] Sign1(byte[] message)
        {
            var compactSignature = PrivateKey.Sign(new uint256(message));
            return compactSignature.ToDER();
        }

        [TestCase("b80553c039bb8bac7414673b7eb5d9f6bb931572afb0618e141bc87b3906000e")] // Passes
        [TestCase("688787d8ff144c502c7f5cffaafe2cc588d86079f9de88304c26b0cb99ce91c6")] // Passes
        [TestCase("a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3")] // Passes
        [TestCase("e8f56862d74ef5599af4eeca73924bfa44a6773a497af0c29c48e18729ba6ff0")] // Passes
        // [TestCase("489cd5dbc708c7e541de4d7cd91ce6d0f1613573b7fc5b40d3942ccb9555cf35")] // Fails
        // [TestCase("657f18518eaa2f41307895e18c3ba0d12d97b8a23c6de3966f52c6ba39a07ee4")] // Fails
        // [TestCase("5e81ba0744f5c8537ea42de0e61ec4b676ece53469d0e1cf7495bc5e56126e6a")] // Fails
        // [TestCase("9943b071e6ff7c75e9f4716fba01ba64e56ee45dc1e8e36c1da744801ef4c21b")] // Fails
        public void CompareSigningMethods(string message)
        {
            var messageHash = Encoders.Hex.DecodeData(message);

            var signatureWithHdWallet = Encoders.Hex.EncodeData(Sign(messageHash)); 
            var signatureWithHdWallet1 = Encoders.Hex.EncodeData(Sign1(messageHash));

            Assert.AreEqual(signatureWithHdWallet, signatureWithHdWallet1);
        }     

        [TestCase("b80553c039bb8bac7414673b7eb5d9f6bb931572afb0618e141bc87b3906000e", "304402205483a10f49d4116b9aa82b72cb70ff975f13e6303d658510c1285be3c68a691202202451e97f55c7b004feccffa61242f1c02b0e65b76f95732aa31a00dd3c0dc2b2")]
        [TestCase("688787d8ff144c502c7f5cffaafe2cc588d86079f9de88304c26b0cb99ce91c6", "304302200d60b9383e1e6ae1801fa450498c9fbbac659deca086e712b661eefe112300ef021f2bcd044e1edd43764b3de2fee167954eb46c4506679a2924b76e78ef0dd6b0")]
        [TestCase("a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", "3044022049913f283c81ecc12800b6ab2c10445715705b0ee0473c7759f808e01de6d4fa022047685678bb287bd8a9e953a2dbafdacb1ca5cdc8915536ade731e86f9c106135")]
        [TestCase("e8f56862d74ef5599af4eeca73924bfa44a6773a497af0c29c48e18729ba6ff0", "304402204451a918d512be8926b875e3c1e63e21fdeb5233158e50009932b3c5af253f610220628550265ef4f18c14b32b5a41060153c9ac53f7b13154dac8a3b2693b8792cd")]
        [TestCase("489cd5dbc708c7e541de4d7cd91ce6d0f1613573b7fc5b40d3942ccb9555cf35", "304402200adfcbdfd436f9acd7ef0b6d363a403aa9f401bf4eede2826d158279cd0d5d6d022062123a6379937dac45e0c6573ae4b6c933cf92fbc1f414d76d9c340e927c3f9d")]
        [TestCase("657f18518eaa2f41307895e18c3ba0d12d97b8a23c6de3966f52c6ba39a07ee4", "304402206fd060f68ddebb2c8e86ca0c1e1b74df8b2f40d6c499b9c207e5f094c6e22cc702200c10f5a3136c24876a932abf4473f3997fed8c5d29eba8c4c75265ab700b8db5")]
        [TestCase("5e81ba0744f5c8537ea42de0e61ec4b676ece53469d0e1cf7495bc5e56126e6a", "304402206d296f902451688c9d0cef80293fb18ce42e13e38f51797d6e41f9ed615ae88f02206087a9ac2db726e4acc6f81170570b08d7a52a9654b66e896efec527c160718e")]
        [TestCase("9943b071e6ff7c75e9f4716fba01ba64e56ee45dc1e8e36c1da744801ef4c21b", "3044022020c25a19fb2398d5f99ba90f692807f17aa29b7a26a3fa0569055d3422e4abbb02201d67ee8cc52198953b88ef524736fc27c8b01a97a039b765fae3813b59a03cbb")]
        public void ShouldSignWithKey(string message, string signature)
        {
            var messageHash = Encoders.Hex.DecodeData(message);
            var signatureWithHdWallet = Encoders.Hex.EncodeData(Sign1(messageHash));
            Assert.AreEqual(signature, signatureWithHdWallet);
        }
    }
}