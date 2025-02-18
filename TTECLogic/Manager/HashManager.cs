using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TTECLogic.Manager
{
    internal class HashManager
    {
        public static string HashPassword(string pass)
        {
            SHA512Managed sha512 = new SHA512Managed();
            byte[] hashedValue = sha512.ComputeHash(Encoding.UTF8.GetBytes(pass));
            string hashpass = BitConverter.ToString(hashedValue);
            return hashpass;
        }
    }
}
