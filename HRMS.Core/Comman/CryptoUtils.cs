﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Comman
{
    public class CryptoUtils
    {
        public static string GenerateKeySalt(int saltLenght = 128)
        {
            byte[] bytesBuffer = new byte[saltLenght * 2];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytesBuffer);
            }
            return Convert.ToBase64String(bytesBuffer)[..saltLenght];

        }
        public static string HashHmacsha512Base64(string text, string secretkey)
        {
            if(string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException(nameof(text));
            if(string.IsNullOrWhiteSpace(secretkey)) throw new ArgumentNullException(nameof(secretkey));

            var hashvalue = HashmacSha512(text, secretkey);
            return Convert.ToBase64String(hashvalue);
        }
        public static byte[] HashmacSha512(string text, string secretkey)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secretkey);
            var inputBytes = Encoding.UTF8.GetBytes(text);

            using var hmacSha512 = new HMACSHA256(secretBytes); 
            return hmacSha512.ComputeHash(inputBytes);
        }
    }
}
