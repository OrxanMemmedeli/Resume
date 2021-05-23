using Resume.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Business.Control
{
    public class CurrentUser
    {
        private readonly ResumeContext db;
        public CurrentUser(ResumeContext context)
        {
            db = context;
        }


        //public int FindUser()
        //{
        //    var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
        //    return
        //}
    }
}
