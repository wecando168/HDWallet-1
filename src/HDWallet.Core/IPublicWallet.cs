namespace HDWallet.Core
{
    public interface IPublicWallet 
    {
        string Address { get; }

        byte[] PublicKeyBytes { get; set; }

        // TODO: Add unit tests
        bool Verify(byte[] message, Signature sig);
    }
}