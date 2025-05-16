namespace Helpers
{
    public interface ICryptoHelper
    {
        public string Encrypt(string plain, string salt);
        public string Decrypt(string encrypted, string salt);
    }
}
