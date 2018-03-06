using System;
using System.Security.Cryptography;
using System.Text;

namespace ICTLab_backend.DTO
{
    
    //Password hasher
    public class PasswordHasher
    {
        //Expects a string a input
        public string GenerateSHA256(string input)
        {
            var sha256 = SHA256Managed.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            //Console.WriteLine(GetStringFromHash(hash));
            return GetStringFromHash(hash);
        }
        
        public string GetStringFromHash(byte[] hashValue)
        {
            var result = new StringBuilder();
            for (var i = 0; i < hashValue.Length; i++)
            {
                result.Append(hashValue[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}