namespace HDWallet.Core
{
    public interface IPublicAccount<out TWallet> where TWallet : IPublicWallet, new()
    {
        TWallet GetInternalPublicWallet(uint addressIndex);
        TWallet GetExternalPublicWallet(uint addressIndex);
    }
}