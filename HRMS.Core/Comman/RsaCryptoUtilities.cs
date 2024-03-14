using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace HRMS.Core.Comman
{
    public static class RsaCryptoUtilities
    {
        public const string AlgorithmRsaNoneOaepWithSha256AndMgf1Padding = "RSA/NONE/OAEPWITHSHA256ANDMGF1PADDING";

        public const string AlgorithmSHA256withRSA = "SHA-256withRSA";

        public static AsymmetricCipherKeyPair GenerateRSAKeyPair( int KeyLenghtInBits = 2048)
        {
            var rsaKeyPairGeneration = new RsaKeyPairGenerator();
            rsaKeyPairGeneration.Init(new KeyGenerationParameters(new SecureRandom(), KeyLenghtInBits));

            return rsaKeyPairGeneration.GenerateKeyPair();


        }
        public static RsaPrivateCrtKeyParameters ImportPrivateKeyDer(string base64Der)
        {
            var privateKeyBytes = Convert.FromBase64String(base64Der);
            var privateKeyStructure = (Asn1Sequence)Asn1Object.FromByteArray(privateKeyBytes);
            var privateKeyInfo = PrivateKeyInfo.GetInstance(privateKeyStructure);

            var rsaPrivateKey = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(privateKeyInfo);
            return rsaPrivateKey;
        }
        public static RsaKeyParameters ImportPublicKeyDer(string base64Der)
        {
            var publicKeyBytes = Convert.FromBase64String(base64Der);
            var publicKeyStructure = (Asn1Sequence)Asn1Object.FromByteArray(publicKeyBytes);
            var publicKeyInfo = SubjectPublicKeyInfo.GetInstance(publicKeyStructure);

            var rsaPublicKey = (RsaKeyParameters)PublicKeyFactory.CreateKey(publicKeyInfo);

            return rsaPublicKey;


        }
        public static  RsaPrivateCrtKeyParameters ImportPrivateKeyPem (string pem)
        {
            var privateKeyReader = new PemReader(new StringReader(pem));
            var privatKeyObject = privateKeyReader.ReadObject();
            var rsaPrivateKey = (RsaPrivateCrtKeyParameters)privatKeyObject;

            return rsaPrivateKey;   


        }
        public static byte[] DecryptData(
            byte[] cipheredData,
            RsaPrivateCrtKeyParameters privateKey, 
            string decyptionAlorithm  = AlgorithmRsaNoneOaepWithSha256AndMgf1Padding)
        {
            IBufferedCipher cipherd = CipherUtilities.GetCipher(decyptionAlorithm);
            cipherd.Init(false, privateKey);

            return ApplyCipher(cipheredData, cipherd);
        }
        private static byte[] ApplyCipher (byte[] data, IBufferedCipher cipher)
        {
            using var inputStream = new MemoryStream();
            var outputBytes = new List<byte>(); 
            var blockSize = cipher.GetBlockSize();

            var buffer = new byte[blockSize];
            int index;
            while ((index = inputStream.Read(buffer, 0, blockSize)) >0)
            {
                var chiperBlock = cipher.DoFinal(buffer, 0, index);
                outputBytes.AddRange(chiperBlock);
            }
            return outputBytes.ToArray();
        }
        
    }
}
