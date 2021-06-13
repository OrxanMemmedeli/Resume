﻿using System;
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
            this.UserRoleControls = new HashSet<UserRoleControl>();
            this.ControllerActionUsers = new HashSet<ControllerActionUser>();
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
        public virtual ICollection<UserRoleControl> UserRoleControls { get; set; }
        public ICollection<ControllerActionUser> ControllerActionUsers { get; set; }
    }
}
