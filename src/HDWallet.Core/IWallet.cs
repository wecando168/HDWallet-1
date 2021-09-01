namespace HDWallet.Core
{
    public interface IWallet
    {
        string Address { get; }

        Signature Sign(byte[] message);

        public byte[] PrivateKeyBytes { get; set; }

        public uint Index { get; set; }
    }
}