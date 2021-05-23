using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class RoleControl
    {
        public RoleControl()
        {
            this.UserRoleControls = new HashSet<UserRoleControl>();
        }

        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string ControllerName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string ActionName { get; set; }

        public virtual ICollection<UserRoleControl> UserRoleControls { get; set; }
    }
}
