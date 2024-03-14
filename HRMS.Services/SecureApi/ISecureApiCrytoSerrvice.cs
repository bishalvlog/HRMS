using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.SecureApi
{
    public interface  ISecureApiCrytoService
    {
        Task<byte[]> DecryptHybrideAsync(byte[] cipheredDate, byte[] cipheredKey, byte[] iv);
    }
}
