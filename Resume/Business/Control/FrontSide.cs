using Resume.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Business.Control
{
    public class FrontSide
    {
        public string Control(ResumeContext db)
        {
            string result = string.Empty;
            var sections = db.HideTables.Where(x => x.Status == true).ToList();
            foreach (var item in sections)
            {
                result += '"' + item.TableName + '"';
            }
            return result;
        }  
    }
}
