using Resume.Business.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.ViewModels
{
    public class LoginDatasViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string captcha { get; set; }

        public LoginDatasViewModel ProtectForSQLInjection(LoginDatasViewModel model)
        {
            model.Email = SQLInjection.Protect(model.Email);
            model.Password = SQLInjection.Protect(model.Password);
            return model;
        }

    }
}
