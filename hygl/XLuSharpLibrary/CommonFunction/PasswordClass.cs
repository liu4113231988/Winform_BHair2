using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XLuSharpLibrary.CommonFunction
{
   public class PasswordClass
    {
        public static string EncryptMD5(string strtext)
        {
            MD5 md = new MD5CryptoServiceProvider();
            byte[] value = md.ComputeHash(Encoding.UTF8.GetBytes(strtext));
            return BitConverter.ToString(value).Replace("-", "");
        }
    }
}
