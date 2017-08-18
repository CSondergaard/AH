using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Client_ah
{
    public class login
    {


        public string HashString(string text, string salt)
        {
            // Create an instance of the SHA256 provider
            var sha = new SHA256Managed();

            // Compute the hash 
            var combinedPassword = String.Concat(text, salt);
            var bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public string GetRandomSalt(Int32 size = 12)
        {
            var random = new RNGCryptoServiceProvider();
            var salt = new Byte[size];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

    }
}
