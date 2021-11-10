using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ProjectHotel.BLL.Helpers
{
    static public class HashPasword
    {
        static public string CreateHashPassword(string Password,string Salt)
        {
            byte[] BPswd = Encoding.UTF8.GetBytes(Password);
            byte[] BSalt = Encoding.UTF8.GetBytes(Salt);

            byte[] PreHashed = new byte[BSalt.Length + BPswd.Length];
            Buffer.BlockCopy(BPswd, 0, PreHashed, 0, BPswd.Length);
            Buffer.BlockCopy(BSalt, 0, PreHashed, BPswd.Length,BSalt.Length);

            SHA256 sHA256 = SHA256.Create();

            byte[] HashedBytes = sHA256.ComputeHash(PreHashed);
            return BitConverter.ToString(HashedBytes);
        }
    }
}
