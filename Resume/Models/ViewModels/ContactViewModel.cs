using Resume.Business.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.ViewModels
{
    public class ContactViewModel
    {
        public string NameSurname { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public string captcha { get; set; }

        public ContactViewModel ProtectForSQLInjection(ContactViewModel model)
        {
            model.NameSurname = SQLInjection.Protect(model.NameSurname);
            model.Subject = SQLInjection.Protect(model.Subject);
            model.Email = SQLInjection.Protect(model.Email);
            model.Message = SQLInjection.Protect(model.Message);
            return model;
        }
    }
}
