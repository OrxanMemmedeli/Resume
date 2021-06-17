using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class User
    {
        public User()
        {
            this.ControllerActionUsers = new HashSet<ControllerActionUser>();
            this.ActionUsers = new HashSet<ActionUser>();
        }

        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password")]
        [Column(TypeName = "nvarchar(max)")]
        public string ConfirmPassword { get; set; }
        public bool Status { get; set; }

        public ICollection<ControllerActionUser> ControllerActionUsers { get; set; }
        public ICollection<ActionUser> ActionUsers { get; set; }
    }
}
