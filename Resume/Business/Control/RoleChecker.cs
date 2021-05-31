using Resume.Models.Context;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Business.Control
{
    public class RoleChecker
    {
        private readonly ResumeContext db;

        public RoleChecker(ResumeContext context)
        {
            db = context;
        }

        public bool AuthorizeRoles(int userID, string controllerName, string actionName)
        {
            bool result = false;
            var role = db.UserRoleControls.FirstOrDefault(x => x.UserID == userID && x.RoleControl.ControllerName == controllerName && x.RoleControl.ActionName == actionName);
            if (role != null)
            {
                result = true;
            }
            return result;
        }
    }
}
