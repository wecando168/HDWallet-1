namespace HDWallet.Core
{
    // https://github.com/bitcoin/bips/blob/master/bip-0044.mediawiki#purpose
    public partial class Purpose
    {   
        PurposeNumber _purposeNumber;
        Purpose(PurposeNumber purposeNumber)
        {
            _purposeNumber = purposeNumber;
        }

        public static Purpose Create(PurposeNumber purposeNumber)
        {
            return new Purpose(purposeNumber);
        }

        public CoinPath CreateCoinPath(CoinType coinType)
        {
            return new CoinPath(_purposeNumber, coinType);
        }
    }
}