using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Helpers
{
    public class CryptoHelper : ICryptoHelper
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public CryptoHelper(string key, string iv)
        {
            if (key.Length != 16 || iv.Length != 16)
                throw new ArgumentException("Key와 IV는 16바이트 문자열이여야 합니다.");

            _key = Encoding.UTF8.GetBytes(key);
            _iv = Encoding.UTF8.GetBytes(iv);
        }

        public string Encrypt(string plain, string salt)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;
                aes.Mode = CipherMode.CBC;

                var _plainBytes = Encoding.UTF8.GetBytes(salt + plain);
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(_plainBytes, 0, _plainBytes.Length);
                    cs.FlushFinalBlock();

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string Decrypt(string encrypted, string salt)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;
                aes.Mode = CipherMode.CBC;

                var _encBytes = Convert.FromBase64String(encrypted);
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(_encBytes, 0, _encBytes.Length);
                    cs.FlushFinalBlock();

                    var res = Encoding.UTF8.GetString(ms.ToArray());
                    return res.StartsWith(salt) ? res.Substring(salt.Length) : string.Empty;
                }
            }
        }
    }
}
