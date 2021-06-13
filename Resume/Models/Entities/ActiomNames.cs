using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class ActiomNames
    {
        public ActiomNames()
        {
            this.ControllerActionUsers = new HashSet<ControllerActionUser>();
        }

        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public ICollection<ControllerActionUser> ControllerActionUsers { get; set; }
    }
}
