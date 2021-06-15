using Resume.Models.Context;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Business.Control
{
    public static class RoleChecker
    {
        public static bool AuthorizeRoles(ResumeContext context, int userID, string controllerName, string actionName)
        {
            bool result = false;
            var role = context.ControllerActionUsers.FirstOrDefault(x => x.UserID == userID && x.ControllerNames.Name == controllerName);
            if (role != null)
            {
                if (role.ControllerNames.ActionNames.Select(x => x.Name).Contains(actionName))
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
