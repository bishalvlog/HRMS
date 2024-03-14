using HRMS.Core.Comman;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Threading.Tasks;

namespace HRMS.Services.SecureApi
{
    public class SecureApiCrytoService : ISecureApiCrytoService
    {
        private readonly IConfiguration _config;
        public SecureApiCrytoService( IConfiguration configuration)
        {
            _config = configuration;
        }
        public Task<byte[]> DecryptHybrideAsync(byte[] cipheredDate, byte[] cipheredKey, byte[] iv)
        {

            var rsaPrivateKey = ImportPrivateKey();
            
            var key = RsaCryptoUtilities.DecryptData(cipheredKey,rsaPrivateKey);

            var decipheredData = AesCryptoUtilites.DecryPt(cipheredDate, key, iv);

           return Task.FromResult(decipheredData);

           
        }
        private RsaPrivateCrtKeyParameters ImportPrivateKey()
        {
            var keyFormate = _config["SecureApi:E2EE:Request:RSAKeyFormate"];
            var key = _config["SecureApi:E2EE:Request:PrivateKey"];

            if (keyFormate.Equals("DER", StringComparison.InvariantCultureIgnoreCase))
                return RsaCryptoUtilities.ImportPrivateKeyDer(key);

            return RsaCryptoUtilities.ImportPrivateKeyPem(key);

        }
        private RsaKeyParameters ImportPublicKey()
        {
            var keyFormate = _config["SecureApi:E2EE:Request:RSAKeyFormate"];
            var key = _config["SecureApi:E2EE:Request:PrivateKey"];

            if(keyFormate.Equals("DER",StringComparison.InvariantCultureIgnoreCase))
                return RsaCryptoUtilities.ImportPublicKeyDer(key);
            return RsaCryptoUtilities.ImportPublicKeyDer(key);

      
        }

    }
}
