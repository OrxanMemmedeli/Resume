using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class ControllerActionUser
    {
        public int ControllerID { get; set; }
        //public int ActionNamesID { get; set; }
        public int UserID { get; set; }


        public ControllerNames ControllerNames { get; set; }
        //public ActionNames ActionNames { get; set; }
        public User User { get; set; }
    }
}
