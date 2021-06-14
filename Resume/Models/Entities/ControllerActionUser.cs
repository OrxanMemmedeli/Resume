using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class ControllerActionUser
    {
        [ForeignKey("ControllerAction")]
        public int ControllerActionID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }

        public ControllerAction ControllerAction { get; set; }
        public User User { get; set; }
    }
}
