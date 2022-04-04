using NBitcoin;
using NUnit.Framework;

namespace HDWallet.Core.Tests
{
    class SampleWallet : HdWalletBase
    {
        public SampleWallet(string seed) : base(seed) {}

        public SampleWallet(string mnemonic, string passphrase) : base(mnemonic, passphrase) {}
    }

    public class HdWalletBaseTests
    {
        [Test]
        public void ShouldCreateFromMnemonic()
        {
            var wallet = new SampleWallet("push wrong tribe amazing again cousin hill belt silent found sketch monitor", "");
            Assert.AreEqual("3d977063d3e2ee074f8d6806d1fb73d1b3884d29ab032aa1c7121cfddb0467a99330647652bbe6a244074bccaed63dc08a67286dc1fbf1b8aa36e8aa7bfce909", wallet.BIP39Seed);
        }

        // English test vectors : https://github.com/trezor/python-mnemonic/blob/master/vectors.json
        // Japanese test vectors: https://github.com/bip32JP/bip32JP.github.io/blob/master/test_JP_BIP39.json 
        [TestCase("abandon abandon abandon abandon abandon abandon abandon abandon abandon abandon abandon about", "c55257c360c07c72029aebc1b53c05ed0362ada38ead3e3e9efa3708e53495531f09a6987599d18264c1e1c92f2cf141630c7a3c4ab7c81b2f001698e7463b04")]
        [TestCase("legal winner thank year wave sausage worth useful legal winner thank yellow", "2e8905819b8723fe2c1d161860e5ee1830318dbf49a83bd451cfb8440c28bd6fa457fe1296106559a3c80937a1c1069be3a3a5bd381ee6260e8d9739fce1f607")]
        [TestCase("letter advice cage absurd amount doctor acoustic avoid letter advice cage above", "d71de856f81a8acc65e6fc851a38d4d7ec216fd0796d0a6827a3ad6ed5511a30fa280f12eb2e47ed2ac03b5c462a0358d18d69fe4f985ec81778c1b370b652a8")]
        [TestCase("zoo zoo zoo zoo zoo zoo zoo zoo zoo zoo zoo wrong", "ac27495480225222079d7be181583751e86f571027b0497b5b5d11218e0a8a13332572917f0f8e5a589620c6f15b11c61dee327651a14c34e18231052e48c069")]
        [TestCase("abandon abandon abandon abandon abandon abandon abandon abandon abandon abandon abandon abandon abandon abandon abandon abandon abandon agent", "035895f2f481b1b0f01fcf8c289c794660b289981a78f8106447707fdd9666ca06da5a9a565181599b79f53b844d8a71dd9f439c52a3d7b3e8a79c906ac845fa")]
        [TestCase("あいこくしん　あいこくしん　あいこくしん　あいこくしん　あいこくしん　あいこくしん　あいこくしん　あいこくしん　あいこくしん　あいこくしん　あいこくしん　あおぞら", "a262d6fb6122ecf45be09c50492b31f92e9beb7d9a845987a02cefda57a15f9c467a17872029a9e92299b5cbdf306e3a0ee620245cbd508959b6cb7ca637bd55", "㍍ガバヴァぱばぐゞちぢ十人十色")]
        [TestCase("うちゅう　ふそく　ひしょ　がちょう　うけもつ　めいそう　みかん　そざい　いばる　うけとる　さんま　さこつ　おうさま　ぱんつ　しひょう　めした　たはつ　いちぶ　つうじょう　てさぎょう　きつね　みすえる　いりぐち　かめれおん", "346b7321d8c04f6f37b49fdf062a2fddc8e1bf8f1d33171b65074531ec546d1d3469974beccb1a09263440fc92e1042580a557fdce314e27ee4eabb25fa5e5fe", "㍍ガバヴァぱばぐゞちぢ十人十色")]
        public void ShouldCreateFromMnemonic(string mneumonic, string seed, string passphrase = "TREZOR")
        {
            var wallet = new SampleWallet(mneumonic, passphrase);
            Assert.AreEqual(seed, wallet.BIP39Seed);
        }

        [Test]
        public void ShouldCreateFromMnemonicAndPassword()
        {
            var wallet = new SampleWallet("push wrong tribe amazing again cousin hill belt silent found sketch monitor", "passphrase");
            Assert.AreEqual("256a23851bd21dbf38c1d38d69ec386eaf402c01fc76ce72de69df093c8e8fbf1caf3f77e09bd76e725020309d57f0ea4c71ecee69b548fe34249305172ded82", wallet.BIP39Seed);
        }

        [Test]
        public void ShouldCreateFromSeed()
        {
            var wallet = new SampleWallet("3d977063d3e2ee074f8d6806d1fb73d1b3884d29ab032aa1c7121cfddb0467a99330647652bbe6a244074bccaed63dc08a67286dc1fbf1b8aa36e8aa7bfce909");
            Assert.AreEqual("3d977063d3e2ee074f8d6806d1fb73d1b3884d29ab032aa1c7121cfddb0467a99330647652bbe6a244074bccaed63dc08a67286dc1fbf1b8aa36e8aa7bfce909", wallet.BIP39Seed);
        }

        [Test]
        public void Notes()
        {
            // Mnemonic
            Mnemonic mnemonic = new Mnemonic("push wrong tribe amazing again cousin hill belt silent found sketch monitor");

            // mnemonic -> seed (hex)
            string seed = mnemonic.DeriveSeed().ToHexString();
            Assert.AreEqual("3d977063d3e2ee074f8d6806d1fb73d1b3884d29ab032aa1c7121cfddb0467a99330647652bbe6a244074bccaed63dc08a67286dc1fbf1b8aa36e8aa7bfce909", seed);

            // Seed (hex) -> Xprv
            ExtKey extKey = ExtKey.CreateFromSeed(seed.FromHexToByteArray());
            BitcoinExtKey bitcoinExtKey = extKey.GetWif(Network.Main);
            string wif = bitcoinExtKey.ToWif();
            Assert.AreEqual("xprv9s21ZrQH143K36zpCxiwo4sgScCQm3fef6K8ajZiJ7wseezmwAi6sKhEuWexTKt7CNp1z2KKsMTJhZXrhEKVYbpEHrB9DnsTp31YcWtA1k3", wif);

            // string (xprv...) -> BitcoinExtKey Xprv
            BitcoinExtKey bitcoinExtKeyParsed = new BitcoinExtKey(wif, Network.Main);
            var parsedWif = bitcoinExtKeyParsed.ToWif();
            Assert.AreEqual("xprv9s21ZrQH143K36zpCxiwo4sgScCQm3fef6K8ajZiJ7wseezmwAi6sKhEuWexTKt7CNp1z2KKsMTJhZXrhEKVYbpEHrB9DnsTp31YcWtA1k3", parsedWif);

            // BitcoinExtKey -> string (xprv...)
            parsedWif = bitcoinExtKeyParsed.ToString();
            Assert.AreEqual("xprv9s21ZrQH143K36zpCxiwo4sgScCQm3fef6K8ajZiJ7wseezmwAi6sKhEuWexTKt7CNp1z2KKsMTJhZXrhEKVYbpEHrB9DnsTp31YcWtA1k3", parsedWif);

            // string (xprv...) -> ExtKey Xprv
            ExtKey extKeyParsed = ExtKey.Parse(wif, Network.Main);
            wif = extKeyParsed.GetWif(Network.Main).ToWif();
            Assert.AreEqual("xprv9s21ZrQH143K36zpCxiwo4sgScCQm3fef6K8ajZiJ7wseezmwAi6sKhEuWexTKt7CNp1z2KKsMTJhZXrhEKVYbpEHrB9DnsTp31YcWtA1k3", wif);

            // ExtKey -> string (xprv...)
            var stringHex = extKeyParsed.ToString(Network.Main);
            Assert.AreEqual("xprv9s21ZrQH143K36zpCxiwo4sgScCQm3fef6K8ajZiJ7wseezmwAi6sKhEuWexTKt7CNp1z2KKsMTJhZXrhEKVYbpEHrB9DnsTp31YcWtA1k3", stringHex);

            ExtKey extKey1 = ExtKey.CreateFromSeed("3d977063d3e2ee074f8d6806d1fb73d1b3884d29ab032aa1c7121cfddb0467a99330647652bbe6a244074bccaed63dc08a67286dc1fbf1b8aa36e8aa7bfce909".FromHexToByteArray());
            ExtKey extKey2 = ExtKey.Parse("xprv9s21ZrQH143K36zpCxiwo4sgScCQm3fef6K8ajZiJ7wseezmwAi6sKhEuWexTKt7CNp1z2KKsMTJhZXrhEKVYbpEHrB9DnsTp31YcWtA1k3", Network.Main);
            Assert.AreEqual(extKey1.PrivateKey, extKey2.PrivateKey);
        }
    }
}