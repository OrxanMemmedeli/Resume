using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class ControllerAction
    {
        public ControllerAction()
        {
            this.ControllerActionUsers = new HashSet<ControllerActionUser>();
        }

        [Key]
        public int ID { get; set; }

        public int ControllerID { get; set; }
        public int ActionID { get; set; }


        public ControllerNames ControllerNames { get; set; }
        public ActiomNames ActiomNames { get; set; }

        public ICollection<ControllerActionUser> ControllerActionUsers { get; set; }
    }
}
