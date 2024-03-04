using System.Security.Cryptography;
using System.Text;

namespace Cryptid
{
    public static class AES
    {
        public static void Encrypt(string file, string key)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] content = Utility.GetStreamBytes(fs);
                Utility.EmptyStream(fs);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    byte[] initVector = aes.IV;

                    fs.Write(initVector, 0, initVector.Length);

                    using (CryptoStream cryptoStream = new CryptoStream(fs,
                        aes.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(content);
                    }
                }
            }
        }

        public static void Decrypt(string file, string key)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.ReadWrite))
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] content;
                    byte[] initVectorBuffer = new byte[aes.IV.Length];

                    fs.Read(initVectorBuffer, 0, 16);

                    using (CryptoStream cryptoStream = new CryptoStream(fs,
                        aes.CreateDecryptor(Encoding.UTF8.GetBytes(key), initVectorBuffer),
                        CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cryptoStream))
                        {
                            content = Encoding.UTF8.GetBytes(sr.ReadToEnd());

                            Utility.EmptyStream(fs);
                            fs.Write(content, 0, content.Length);
                        }
                    }
                }
            }
        }
    }
}
