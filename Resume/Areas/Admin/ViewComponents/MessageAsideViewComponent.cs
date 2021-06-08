using Microsoft.AspNetCore.Mvc;
using Resume.Areas.Admin.Models.ViewModels;
using Resume.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Areas.Admin.ViewComponents
{
    public class MessageAsideViewComponent : ViewComponent
    {
        private readonly MessageCount _count;
        private readonly ResumeContext db;

        public MessageAsideViewComponent(MessageCount count, ResumeContext context)
        {
            _count = count;
            db = context;
        }

        public IViewComponentResult Invoke()
        {
            var messages = db.Contacts;
            _count.Inbox = messages.Where(s => s.Respons == false && s.Status == true).Count();
            _count.Answered = messages.Where(s => s.Respons == true && s.Status == false).Count();
            _count.Read = messages.Where(s => s.Respons == false && s.Status == false).Count();
            return View(_count);
        }
    }
}
