using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Business.Tools
{
    public class AncryptionAndDecryption
    {

        public static string encodedata(string str)
        {
            string returndata = "";
            byte[] data = UTF8Encoding.UTF8.GetBytes(str);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("KhanCrypt"));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] resuts = transform.TransformFinalBlock(data, 0, data.Length);
                    returndata = Convert.ToBase64String(resuts, 0, resuts.Length);
                }
            }
            return returndata;
        }


        public static string decodedata(string str)
        {
            string returndata = "";
            byte[] data = Convert.FromBase64String(str);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("KhanCrypt"));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] resuts = transform.TransformFinalBlock(data, 0, data.Length);
                    returndata = UTF8Encoding.UTF8.GetString(resuts);
                }
            }
            return returndata;
        }
    }
}
