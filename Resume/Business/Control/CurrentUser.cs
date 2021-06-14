using Resume.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Resume.Business.Control
{
    public static class CurrentUser
    {
        public static int FindUser(ResumeContext contex, IHttpContextAccessor httpContextAccessor)
        {
            var user = contex.Users.FirstOrDefault(x => x.Email == httpContextAccessor.HttpContext.User.Identity.Name);
            return user.ID;
        }
    }
}
