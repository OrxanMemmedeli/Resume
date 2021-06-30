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
            var controllers = context.ControllerActionUsers.FirstOrDefault(x => x.UserID == userID && x.ControllerNames.Name == controllerName);
            if (controllers != null)
            {
                var action = context.ActionNames.FirstOrDefault(x => x.ControllerNamesID == controllers.ControllerID && x.Name == actionName);
                if (action != null)
                {
                    if (context.ActionUsers.FirstOrDefault(x => x.UserID == userID && x.ActionID == action.ID) != null)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
