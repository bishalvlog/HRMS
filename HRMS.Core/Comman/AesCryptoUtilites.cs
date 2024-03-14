using System.Buffers;
using System.Security.Cryptography;

namespace HRMS.Core.Comman
{
    public static class AesCryptoUtilites
    {
        public  static byte[] DecryPt(byte[] cipherTextBytes, byte[] KeyBytes , byte[] ivBytes)
        {
            using var aes = Aes.Create();

            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            aes.Key = KeyBytes; 
            aes.IV = ivBytes;   

            var decryptor = aes.CreateDecryptor();

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write);
            cs.Write(cipherTextBytes, 0, cipherTextBytes.Length);
            cs.FlushFinalBlock();

            return ms.ToArray();


        }
    }
}
