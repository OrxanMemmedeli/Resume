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
        private readonly IHttpContextAccessor _httpContextAccessor;  // use user.identiyi.name in class
        private readonly ResumeContext db;
        public CurrentUser(DbContextOptions options, ResumeContext context, IHttpContextAccessor httpContextAccessor)
        {
            db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public int FindUser()
        {
            var user = db.Users.FirstOrDefault(x => x.Email == _httpContextAccessor.HttpContext.User.Identity.Name);
            return user.ID;
        }
    }
}
