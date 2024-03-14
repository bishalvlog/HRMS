using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Comman
{
    public static class DgCryptoHelper
    {
        public static (byte[] key , byte[] iv, byte[] data) GetKeyIvDataBytesFromBase64Encoded (string keyIvData, char delimeter  = ',')
        {
            if(keyIvData is null)
            
                throw new ArgumentNullException (nameof (keyIvData));
                var keyIvDataAv = keyIvData.Split(delimeter);
                if(keyIvDataAv.Length != 3)
                
                    throw new InvalidDataException($"Invalide Payload data");
                    var keyBytes = Convert.FromBase64String(keyIvDataAv[0]);
                    var ivBytes = Convert.FromBase64String(keyIvDataAv[1]);
                   var dataBytes = Convert.FromBase64String(keyIvDataAv[2]);
            return (keyBytes, ivBytes, dataBytes);           
            
        }
    };
}
