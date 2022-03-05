namespace HDWallet.Core
{
    public interface IWallet
    {
        string Address { get; }

        Signature Sign(byte[] message);

        byte[] PrivateKeyBytes { get; set; }

        uint Index { get; set; }
    }
}