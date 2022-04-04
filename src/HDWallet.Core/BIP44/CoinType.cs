namespace HDWallet.Core
{
    /// <summary>
    /// https://github.com/bitcoin/bips/blob/master/bip-0044.mediawiki#coin-type
    /// SLIP-0044 : Registered coin types for BIP-0044
    /// https://github.com/satoshilabs/slips/blob/master/slip-0044.md
    /// </summary>
    public enum CoinType : uint 
    {
        Bitcoin = 0,
        BitcoinTestnet = 1,
        CoinType1 = 1,
        Ethereum = 60,
        Tron = 195,
        Polkadot = 354,
        Kusama = 434,
        Cardano = 1815,
        Tezos = 1729,
        Avalanche = 9000,
        FileCoin = 461,
        Solana = 501,
        Blockstack = 5757,
        Neo = 888,
        Terra = 330,
        Algorand = 283
    }
}