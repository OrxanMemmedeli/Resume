using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Business.Tools
{
    public class SQLInjection
    {
        internal static string Protect(string value)
        {
            string result = null;
            if (!string.IsNullOrWhiteSpace(value))
            {
                result = value.Replace("'", "");
                result = result.Replace("<", "");
                result = result.Replace(">", "");
            }
            return result;
        }
    }
}
