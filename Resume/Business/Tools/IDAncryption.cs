using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Business.Tools
{
    public class IDAncryption
    {
        public static string Encrypt(string value)
        {
            int value1 = Convert.ToInt32(value);
            byte[] request = ASCIIEncoding.ASCII.GetBytes("|/-+!@#$%^" + value1.ToString() + "^%$#@!+-/|");
            string respons = Convert.ToBase64String(request);
            return respons;
        }

        public static string Decrypt(string value)
        {
            try
            {
                byte[] request = Convert.FromBase64String(value);
                string respons = ASCIIEncoding.ASCII.GetString(request);
                respons = (respons.Replace("|/-+!@#$%^", "")).Replace("^%$#@!+-/|", "");
                int value1 = Convert.ToInt32(respons);
                respons = value1.ToString();
                return respons;
            }
            catch (Exception)
            {
                return "NotFound";
            }

        }
    }
}
