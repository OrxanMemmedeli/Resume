using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class ActionNames
    {
        public ActionNames()
        {
            //this.ControllerActionUsers = new HashSet<ControllerActionUser>();
            this.ActionUsers = new HashSet<ActionUser>();
        }

        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public int ControllerNamesID { get; set; }

        public ControllerNames ControllerNames { get; set; }
        //public ICollection<ControllerActionUser> ControllerActionUsers { get; set; }
        public ICollection<ActionUser> ActionUsers { get; set; }
    }
}
