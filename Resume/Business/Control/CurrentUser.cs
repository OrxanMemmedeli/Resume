using Resume.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Resume.Business.Control
{
    public class CurrentUser
    {
        public int FindUser(ResumeContext contex, string userIdentity)
        {
            var user = contex.Users.FirstOrDefault(x => x.Email == userIdentity);
            return user.ID;
        }
    }
}
