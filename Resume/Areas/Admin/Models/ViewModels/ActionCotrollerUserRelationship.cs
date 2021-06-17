using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Areas.Admin.Models.ViewModels
{
    public class ActionCotrollerUserRelationship
    {
        public IEnumerable<ControllerActionUser> controllerActionUsers{ get; set; }
        public IEnumerable<ActionUser> actionUsers{ get; set; }
    }
}
