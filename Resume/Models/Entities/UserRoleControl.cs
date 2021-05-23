using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class UserRoleControl
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }

        public User User { get; set; }
        public RoleControl RoleControl { get; set; }
    }
}
