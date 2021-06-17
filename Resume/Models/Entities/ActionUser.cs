using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class ActionUser
    {
        public int ActionID { get; set; }
        public int UserID { get; set; }

        public ActionNames ActionNames { get; set; }
        public User User { get; set; }
    }
}
